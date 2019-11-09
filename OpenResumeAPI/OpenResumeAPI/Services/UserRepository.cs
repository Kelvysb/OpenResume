using BDataBaseStandard;
using OpenResumeAPI.Exceptions;
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
                return ExecuteFindByConfirmation(token);
            }
            catch (DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<User>();
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private User ExecuteFindByConfirmation(string token)
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

        public User FindByReset(string token)
        {
            try
            {
                return ExecuteFindByReset(token);
            }
            catch (DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<User>();
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private User ExecuteFindByReset(string token)
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

        public User FindByEmail(string email)
        {
            try
            {
                return ExecuteFindByEmail(email);
            }
            catch (DataBaseException dbex)
            {
                if (dbex.Code == enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<User>();
                else
                    throw new Exception($"{dbex.Code} - {dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private User ExecuteFindByEmail(string email)
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

        public User FindByLogin(string login)
        {
            try
            {
                return ExecuteFindByLogin(login);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    throw new NotFoundException<User>();
                else
                    throw new System.Exception($"Database Errror: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        private User ExecuteFindByLogin(string login)
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
    }
}
