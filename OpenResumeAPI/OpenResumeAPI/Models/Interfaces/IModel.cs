using OpenResumeAPI.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Models.Interfaces
{
    interface IModel
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
    }
}
