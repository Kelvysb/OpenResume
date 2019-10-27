using System.Collections.Generic;
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface ICRUDRepository<Model> where Model : ModelBase
    {
        List<Model> All();
        List<Model> ByDescription(string description);
        Model ByID(int Id);
        List<Model> ByName(string name);
        int Insert(Model model);
        List<Model> Limit(int limit);
        bool Update(Model model);
        bool Delete(Model model);
    }
}