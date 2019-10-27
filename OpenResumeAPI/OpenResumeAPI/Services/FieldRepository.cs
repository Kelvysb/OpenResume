using BDataBaseStandard;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Services
{
    public class FieldRepository : CRUDRepository<Field>, IFieldRepository
    {
        public FieldRepository(IDataBaseFactory dataBaseFactory) : base(dataBaseFactory) { }

        public List<Field> FindByUserAndResumeAndBlock(int userId, int resumeId, int blockId)
        {
            try
            {
                Dictionary<string, object> par = new Dictionary<string, object>();
                par.Add("USERID", userId);
                par.Add("RESUMEID", resumeId);
                par.Add("BLOCKID", blockId);

                return dataBase.fnExecute<Field>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID
                                                    and resumeId = @RESUMEID
                                                    and blockId = @BLOCKID", par);
            }
            catch (DataBaseException dbEx)
            {
                if (dbEx.Code == DataBaseException.enmDataBaseExeptionCode.NotExists)
                    return new List<Field>();
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
