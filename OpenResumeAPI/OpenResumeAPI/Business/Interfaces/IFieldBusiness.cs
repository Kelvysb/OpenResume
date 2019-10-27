using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IFieldBusiness : ICRUDBusiness<Field>
    {
        List<Field> FindByBlock(int userId, int resumeId, int blockId);
        List<Field> UpdateFields(int userId, int resumeId, int blockId, List<Field> fields);
    }
}