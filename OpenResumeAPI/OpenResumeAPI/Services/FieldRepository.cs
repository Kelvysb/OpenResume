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
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("USERID", userId.ToString()),
                new clsDataBaseParametes("RESUMEID", resumeId.ToString()),
                new clsDataBaseParametes("BLOCKID", blockId.ToString())
            };

            return dataBase.fnExecute<Field>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID
                                                    and resumeId = @RESUMEID
                                                    and blockId = @BLOCKID", par);
        }
    }
}
