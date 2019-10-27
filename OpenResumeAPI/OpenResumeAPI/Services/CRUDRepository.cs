using BDataBaseStandard;
using OpenResumeAPI.Helpers.Attributes;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public virtual List<Model> All()
        {
            try
            {

                return dataBase.fnExecute<Model>($@"Select 
                                                    {Columns()}
                                                from 
                                                    {tableName}");
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Model>();
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual List<Model> Limit(int limit)
        {
            try
            {
                return dataBase.fnExecute<Model>($@"Select top {limit}
                                                    {Columns()}
                                                from 
                                                    {tableName}");
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Model>();
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual Model ByID(int Id)
        {         
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("ID", Id);

                return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    id = @ID", par)
                                .FirstOrDefault();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return null;
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual List<Model> ByName(string name)
        {          
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("NAME", name);

                return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    name = @NAME", par);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Model>();
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual List<Model> ByDescription(string description)
        {         
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("DESCRIPTION", description);

                return dataBase.fnExecute<Model>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    description like @DESCRIPTION", par);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Model>();
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual int Insert(Model model)
        {
            try
            {
                return dataBase.fnExecute<int>($@"insert into {tableName} (
                                                    {Columns()}
                                                ) output inserted.id 
                                                values (
                                                     {ColumnsParametersForInsert()}
                                                )", Parameters(model))
                            .FirstOrDefault();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.Duplicated)
                    return 0;
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }           
        }

        public virtual bool Update(Model model)
        {
            try
            {
                return dataBase.fnExecute<int>($@"update {tableName} SET
                                                    {ColumnsParametersForUpdate()}
                                                    output inserted.id 
                                                where
                                                    id = @ID
                                                ", Parameters(model))
                          .Any();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return false;
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }

          
        }

        public virtual bool Delete(Model model)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("ID", model.Id);
                return dataBase.fnExecute<int>($@"delete from {tableName}                                                    
                                                    output deleted.id 
                                                where
                                                    id = @ID
                                                ", par)
                               .Any();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return false;
                else
                    throw new System.Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }          
        }

        protected virtual string Columns()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null)
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .ToList();

            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected virtual string ColumnsParametersForUpdate()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                                    && !((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .Select(name => $"{name} = @{name.Trim().ToUpper()}")
                            .ToList();
            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected virtual string ColumnsParametersForInsert()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                                    && !((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .Select(name => $"@{name.Trim().ToUpper()}")
                            .ToList();
            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected virtual Dictionary<string, object> Parameters(Model values)
        {
            return new Dictionary<string, object>(
                                typeof(Model).GetProperties()
                                .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null)
                                .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                                .Select(name => new KeyValuePair<string, object>(name.ToUpper(), PropertyValue(values, name))));
        }

        private object PropertyValue(Model values, string columnName)
        {
            return values.GetType().GetProperties()
                    .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                        && ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    .Select(prop => prop.GetValue(values))
                    .FirstOrDefault();
        }

    }
}
