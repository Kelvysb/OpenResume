using OpenResumeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Exceptions
{
    public class CRUDException<Model> : Exception where Model : ModelBase
    {
        public CRUDException() : base() { }
        public CRUDException(string message) : base(message) { }
        public CRUDException(string message, Exception innerException) : base(message, innerException) { }

        public CRUDException(Model element) : base()
        {
            Element = element;
        }
        public CRUDException(string message, Model element) : base(message)
        {
            Element = element;
        }
        public CRUDException(string message, Exception innerException, Model element) : base(message, innerException)
        {
            Element = element;
        }

        public Model Element { get; set; }
    }

    public class NotFoundException<Model> : CRUDException<Model> where Model : ModelBase
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Model element) : base(element)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string message, Model element) : base(message, element)
        {
        }

        public NotFoundException(string message, Exception innerException, Model element) : base(message, innerException, element)
        {
        }
    }

    public class DuplicatedException<Model> : CRUDException<Model> where Model : ModelBase
    {
        public DuplicatedException()
        {
        }

        public DuplicatedException(string message) : base(message)
        {
        }

        public DuplicatedException(Model element) : base(element)
        {
        }

        public DuplicatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicatedException(string message, Model element) : base(message, element)
        {
        }

        public DuplicatedException(string message, Exception innerException, Model element) : base(message, innerException, element)
        {
        }
    }
}
