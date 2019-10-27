using BDataBaseStandard;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Services
{
    public class BlockRepository : CRUDRepository<Block>, IBlockRepository
    {
        public BlockRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public List<Block> FindByUserAndResume(int userId, int resumeId)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("USERID", userId);
                par.Add("RESUMEID", resumeId);

                return dataBase.fnExecute<Block>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID
                                                    and resumeId = @RESUMEID", par);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Block>();
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
