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
            List<clsDataBaseParametes> par = new List<clsDataBaseParametes>()
            {
                new clsDataBaseParametes("USERID", userId.ToString()),
                new clsDataBaseParametes("RESUMEID", resumeId.ToString())
            };

            return dataBase.fnExecute<Block>($@"Select
                                                    {Columns()}
                                                from 
                                                    {tableName}
                                                where
                                                    userId = @USERID
                                                    and resumeId = @RESUMEID", par);
        }
    }
}
