using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Business
{
    public class ResumeBusiness : CRUDBusiness<Resume, IResumeRepository>, IResumeBusiness
    {
        public ResumeBusiness(IResumeRepository repository) : base(repository) { }
    }
}
