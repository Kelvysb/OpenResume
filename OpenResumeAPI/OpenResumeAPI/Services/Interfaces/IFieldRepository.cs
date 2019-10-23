using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface IFieldRepository: ICRUDRepository<Field>
    {
        List<Field> FindByUserAndResumeAndBlock(int userId, int resumeId, int blockId);
    }
}