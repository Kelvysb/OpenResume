using BDataBaseStandard;
using OpenResumeAPI.Helpers.Attributes;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OpenResumeAPI.Services
{
    public class ResumeRepository : CRUDRepository<Resume>, IResumeRepository
    {
        public ResumeRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public Resume Find(string user, string resume)
        {
            try
            {
                string userTable = ((TableName)typeof(User).GetCustomAttributes(typeof(TableName), true).First()).Name;
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("USER", user);
                par.Add("RESUME", resume);
                return dataBase.fnExecute<Resume>($@"Select
                                                        {Columns()}
                                                    from 
                                                        {tableName} R
                                                        Inner join { userTable } U on
                                                            U.id = R.userId
                                                    where
                                                        R.name = @RESUME
                                                        and U.name = @USER", par)
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

        public List<Resume> FindByUserId(int userId)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("USERID", userId);

                return dataBase.fnExecute<Resume>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID", par);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Resume>();
                else
                    throw new System.Exception($"Database Errror: {dbEx.Code} - {dbEx.Message}", dbEx);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Resume FindByUserIdAndName(int userId, string name)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("USERID", userId);
                par.Add("NAME", name);

                return dataBase.fnExecute<Resume>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID
                                                    and name = @NAME", par)
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
