using System.Collections.Generic;
using System.Net;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IResumeBusiness : ICRUDBusiness<Resume>
    {
        List<Resume> List(int userId);
        Resume Find(string user, string resume);
        (HttpStatusCode, Resume) Create(Resume resume);
        HttpStatusCode UpdateResume(Resume resume);
    }
}