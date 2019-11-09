using OpenResumeAPI.Models;
using System;

namespace OpenResumeAPI.Exceptions
{
    public class UserException : Exception
    {

        public UserException() : base() { }

        public UserException(string message) : base(message) { }

        public UserException(string message, Exception innerException) : base(message, innerException) { }

        public UserException(User user) : base()
        {
            this.Subject = user;
        }

        public UserException(string message, User user) : base(message)
        {
            this.Subject = user;
        }

        public UserException(string message, Exception innerException, User user) : base(message, innerException)
        {
            this.Subject = user;
        }
       
        public User Subject { get; set; }
    }
    public class InvalidLoginException : UserException
    {
        public InvalidLoginException()
        {
        }

        public InvalidLoginException(string message) : base(message)
        {
        }

        public InvalidLoginException(User user) : base(user)
        {
        }

        public InvalidLoginException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidLoginException(string message, User user) : base(message, user)
        {
        }

        public InvalidLoginException(string message, Exception innerException, User user) : base(message, innerException, user)
        {
        }
    }
    public class InvalidTokenException : UserException
    {
        public InvalidTokenException()
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }

        public InvalidTokenException(User user) : base(user)
        {
        }

        public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidTokenException(string message, User user) : base(message, user)
        {
        }

        public InvalidTokenException(string message, Exception innerException, User user) : base(message, innerException, user)
        {
        }
    }
    public class InvalidEmailException : UserException
    {
        public InvalidEmailException()
        {
        }

        public InvalidEmailException(string message) : base(message)
        {
        }

        public InvalidEmailException(User user) : base(user)
        {
        }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidEmailException(string message, User user) : base(message, user)
        {
        }

        public InvalidEmailException(string message, Exception innerException, User user) : base(message, innerException, user)
        {
        }
    }
    public class DuplicateLoginException : UserException
    {
        public DuplicateLoginException()
        {
        }

        public DuplicateLoginException(string message) : base(message)
        {
        }

        public DuplicateLoginException(User user) : base(user)
        {
        }

        public DuplicateLoginException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateLoginException(string message, User user) : base(message, user)
        {
        }

        public DuplicateLoginException(string message, Exception innerException, User user) : base(message, innerException, user)
        {
        }
    }
    public class DuplicateEmailException : UserException
    {
        public DuplicateEmailException()
        {
        }

        public DuplicateEmailException(string message) : base(message)
        {
        }

        public DuplicateEmailException(User user) : base(user)
        {
        }

        public DuplicateEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateEmailException(string message, User user) : base(message, user)
        {
        }

        public DuplicateEmailException(string message, Exception innerException, User user) : base(message, innerException, user)
        {
        }
    }
}
