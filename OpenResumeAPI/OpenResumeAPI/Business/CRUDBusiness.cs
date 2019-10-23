using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Business
{
    public class CRUDBusiness<Model, Repository> : ICRUDBusiness<Model> where Model : ModelBase
                                                                        where Repository : ICRUDRepository<Model>
    {
        protected readonly Repository repository;

        public CRUDBusiness(Repository repository)
        {
            this.repository = repository;
        }

        public List<Model> All()
        {
            return repository.All();
        }

        public List<Model> ByDescription(string description)
        {
            return repository.ByDescription(description);
        }

        public Model ByID(int id)
        {
            return repository.ByID(id);
        }

        public List<Model> ByName(string name)
        {
            return repository.ByName(name);
        }

        public int Insert(Model model)
        {
            return repository.Insert(model);
        }

        public List<Model> Limit(int limit)
        {
            return repository.Limit(limit);
        }

        public int Update(Model model)
        {
            return repository.Update(model);
        }
    }
}
