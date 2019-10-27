using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface IResumeRepository: ICRUDRepository<Resume>
    {
        List<Resume> FindByUserId(int userId);
        Resume Find(string user, string resume);
        Resume FindByUserIdAndName(int userId, string name);
    }
}