using BDataBaseStandard;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Services
{
    public class ResumeRepository : CRUDRepository<Resume>, IResumeRepository
    {
        public ResumeRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public List<Resume> FindByUserId(int userId)
        {
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("USERID", userId.ToString())
            };

            return dataBase.fnExecute<Resume>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID", par);
        }
    }
}
