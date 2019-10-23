using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface IBlockRepository: ICRUDRepository<Block>
    {
        List<Block> FindByUserAndResume(int userId, int resumeId);
    }
}