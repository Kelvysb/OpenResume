using System.Collections.Generic;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface ICRUDBusiness<Model>
    {
        List<Model> All();
        List<Model> ByDescription(string description);
        Model ByID(int Id);
        List<Model> ByName(string name);
        int Insert(Model model);
        List<Model> Limit(int limit);
        int Update(Model model);
    }
}
