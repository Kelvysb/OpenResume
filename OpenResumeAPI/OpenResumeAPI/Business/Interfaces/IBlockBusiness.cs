using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IBlockBusiness : ICRUDBusiness<Block>
    {
        List<Block> FindByResume(int userId, int resumeId);
        List<Block> UpdateBlocks(int userId, int resumeId, List<Block> blocks);
    }
}