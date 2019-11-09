using OpenResumeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OpenResumeAPI.Exceptions
{
    public class ResumeException : Exception
    {
        public ResumeException(){}
        public ResumeException(string message) : base(message){}
        public ResumeException(string message, Exception innerException) : base(message, innerException){}
        public ResumeException(Resume resume)
        {
            this.Subject = resume;
        }
        public ResumeException(string message, Resume resume) : base(message)
        {
            this.Subject = resume;
        }
        public ResumeException(string message, Exception innerException, Resume resume) : base(message, innerException)
        {
            this.Subject = resume;
        }
        public Resume Subject { get; set; }
    }
    public class ResumeLimitException : ResumeException
    {
        public ResumeLimitException()
        {
        }

        public ResumeLimitException(string message) : base(message)
        {
        }

        public ResumeLimitException(Resume resume) : base(resume)
        {
        }

        public ResumeLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResumeLimitException(string message, Resume resume) : base(message, resume)
        {
        }

        public ResumeLimitException(string message, Exception innerException, Resume resume) : base(message, innerException, resume)
        {
        }
    }
}
