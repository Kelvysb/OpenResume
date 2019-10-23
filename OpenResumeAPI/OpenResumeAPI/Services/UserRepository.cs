using BDataBaseStandard;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OpenResumeAPI.Services
{
    public class UserRepository : CRUDRepository<User>, IUserRepository
    {
        public UserRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public User FindByEmail(string email)
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
