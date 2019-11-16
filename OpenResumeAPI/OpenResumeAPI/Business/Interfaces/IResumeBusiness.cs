using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IResumeBusiness : ICRUDBusiness<Resume>
    {
        List<Resume> List(int userId);
        Resume Find(string user, string resume);
        Resume Create(Resume resume);
        void UpdateResume(Resume resume);
    }
}