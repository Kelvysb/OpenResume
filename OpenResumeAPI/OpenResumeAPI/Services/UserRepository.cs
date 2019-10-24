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

        public User FindByEmail(string email)
        {
            try
            {
                List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
                {
                    new clsDataBaseParametes("EMAIL", email)
                };

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
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("LOGIN", login)
            };

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
