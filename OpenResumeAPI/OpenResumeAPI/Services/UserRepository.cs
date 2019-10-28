using BDataBaseStandard;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static BDataBaseStandard.DataBaseException;

namespace OpenResumeAPI.Services
{
    public class UserRepository : CRUDRepository<User>, IUserRepository
    {
        public UserRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public User FindByConfirmation(string token)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("CONFIRMATION", token);

                return dataBase.fnExecute<User>($@"Select
                                                        {Columns()}
                                                    from 
                                                        {tableName}
                                                    where
                                                        confirmationToken = @CONFIRMATION", par)
                                .FirstOrDefault();

            }
            catch (DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    return null;
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User FindByReset(string token)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("RESET", token);

                return dataBase.fnExecute<User>($@"Select
                                                        {Columns()}
                                                    from 
                                                        {tableName}
                                                    where
                                                        resetToken = @RESET", par)
                                .FirstOrDefault();

            }
            catch (DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    return null;
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User FindByEmail(string email)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("EMAIL", email);

                return dataBase.fnExecute<User>($@"Select
                                                        {Columns()}
                                                    from 
                                                        {tableName}
                                                    where
                                                        email = @EMAIL", par)
                                .FirstOrDefault();

            }
            catch(DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    return null;
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User FindByLogin(string login)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("LOGIN", login);
                
                return dataBase.fnExecute<User>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    login = @LOGIN", par)
                                .FirstOrDefault();
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return null;
                else
                    throw new System.Exception($"Database Errror: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
