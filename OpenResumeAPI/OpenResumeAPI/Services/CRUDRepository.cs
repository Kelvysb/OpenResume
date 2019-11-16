using BDataBaseStandard;
using OpenResumeAPI.Exceptions;
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
                return ExecuteAll();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>();
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Model> ExecuteAll()
        {
            return dataBase.fnExecute<Model>($@"Select 
                                                    {Columns()}
                                                from 
                                                    {tableName}");
        }

        public virtual List<Model> Limit(int limit)
        {
            try
            {
                return ExecuteLimit(limit);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>();
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Model> ExecuteLimit(int limit)
        {
            return dataBase.fnExecute<Model>($@"Select top {limit}
                                                    {Columns()}
                                                from 
                                                    {tableName}");
        }

        public virtual Model ByID(int Id)
        {         
            try
            {
                return ExecuteById(Id);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>();
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Model ExecuteById(int Id)
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

        public virtual List<Model> ByName(string name)
        {          
            try
            {
                return ExecuteByName(name);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>();
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Model> ExecuteByName(string name)
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

        public virtual List<Model> ByDescription(string description)
        {         
            try
            {
                return ExecuteByDescription(description);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>();
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Model> ExecuteByDescription(string description)
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

        public virtual int Insert(Model model)
        {
            try
            {
                return ExecuteInsert(model);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.Duplicated)
                    throw new DuplicatedException<Model>(model);
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private int ExecuteInsert(Model model)
        {
            return dataBase.fnExecute<int>($@"insert into {tableName} (
                                                    {ColumnsForInsert()}
                                                ) output inserted.id 
                                                values (
                                                     {ColumnsParametersForInsert()}
                                                )", Parameters(model))
                                        .FirstOrDefault();
        }

        public virtual void Update(Model model)
        {
            try
            {
                ExecuteUpdate(model);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>(model);
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }

          
        }

        private void ExecuteUpdate(Model model)
        {
            dataBase.sbExecute($@"update {tableName} SET
                                                    {ColumnsParametersForUpdate()}
                                                where
                                                    id = @ID
                                                ", Parameters(model));
        }

        public virtual void Delete(Model model)
        {
            try
            {
                ExecuteDelete(model);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<Model>(model);
                else
                    throw new Exception($"Database Error: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (Exception)
            {
                throw;
            }          
        }

        private void ExecuteDelete(Model model)
        {
            Dictionary<string, object> par = new Dictionary<string, object>();
            par.Add("ID", model.Id);
            dataBase.sbExecute($@"delete from {tableName}                                                    
                                        where
                                            id = @ID
                                        ", par);
        }

        protected virtual string Columns()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null)
                            .Select(prop => ((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name)
                            .ToList();

            return $"{String.Join(",\r\n", result)} \r\n";
        }

        protected virtual string ColumnsForInsert()
        {
            List<string> result = typeof(Model).GetProperties()
                            .Where(prop => prop.GetCustomAttribute(typeof(ColumnName), true) != null
                                    && !((ColumnName)prop.GetCustomAttribute(typeof(ColumnName), true)).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
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
