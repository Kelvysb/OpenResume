using BDataBaseStandard;
using OpenResumeAPI.Helpers.Attributes;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenResumeAPI.Services
{
    public class CRUDRepository<Model> : ICRUDRepository<Model> where Model : ModelBase
    {

        protected readonly IDataBase dataBase;
        protected string tableName;

        public CRUDRepository(IDataBaseFactory dataBaseFactory)
        {
            dataBase = dataBaseFactory.Build();
            if (typeof(Model).GetCustomAttributes(typeof(TableName), true).Any())
                tableName = ((TableName)typeof(Model).GetCustomAttributes(typeof(TableName), true).First()).Name;
            else
                throw new Exception($"Invalid Model {typeof(Model).Name}, must have TableName attribute.");
        }

        public List<Model> All()
        {
            return dataBase.fnExecute<Model>($@"Select 
                                                    {Columns()}
                                                from 
                                                    {tableName}");
        }

        public List<Model> Limit(int limit)
        {
            return dataBase.fnExecute<Model>($@"Select top {limit}
                                                    {Columns()}
                                                from 
                                                    {tableName}");
        }

        public Model ByID(int Id)
        {
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("ID", Id.ToString())
            };

            return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    id = @ID", par)
                            .FirstOrDefault();
        }

        public List<Model> ByName(string name)
        {
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("NAME", name)
            };

            return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    name = @NAME", par);
        }

        public List<Model> ByDescription(string description)
        {
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("DESCRIPTION", description)
            };

            return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    description like @DESCRIPTION", par);
        }

        public int Insert(Model model)
        {
            return dataBase.fnExecute<int>($@"insert into {tableName} (
                                                    {Columns()}
                                                ) output inserted.id 
                                                values (
                                                     {ColumnsParametersForInsert()}
                                                )", Parameters(model))
                            .FirstOrDefault();
        }

        public int Update(Model model)
        {
            return dataBase.fnExecute<int>($@"update {tableName} SET
                                                    {ColumnsParametersForUpdate()}
                                                    output inserted.id 
                                                where
                                                    id = @ID
                                                )", Parameters(model))
                            .FirstOrDefault();
        }

        protected string Columns()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null)
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .ToList();

            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected string ColumnsParametersForUpdate()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                                    && !((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .Select(name => $"{name} = @{name.Trim().ToUpper()}")
                            .ToList();
            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected string ColumnsParametersForInsert()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                                    && !((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .Select(name => $"@{name.Trim().ToUpper()}")
                            .ToList();
            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected List<clsDataBaseParametes> Parameters(Model values)
        {
            return typeof(Model).GetProperties()
                                .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null)
                                .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                                .Select(name => new clsDataBaseParametes(name.ToUpper(), PropertyValue(values, name)))
                                .ToList();
        }

        private string PropertyValue(Model values, string columnName)
        {
            return values.GetType().GetProperties()
                    .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                        && ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    .Select(prop => prop.GetValue(values).ToString())
                    .FirstOrDefault();
        }
    }
}
