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

        public virtual List<Model> All()
        {
            return repository.All();
        }

        public virtual List<Model> ByDescription(string description)
        {
            return repository.ByDescription(description);
        }

        public virtual Model ByID(int id)
        {
            return repository.ByID(id);
        }

        public virtual List<Model> ByName(string name)
        {
            return repository.ByName(name);
        }

        public virtual int Insert(Model model)
        {
            return repository.Insert(model);
        }

        public virtual List<Model> Limit(int limit)
        {
            return repository.Limit(limit);
        }

        public virtual void Update(Model model)
        {
            repository.Update(model);
        }

        public virtual void Delete(Model model)
        {
            repository.Delete(model);
        }
    }
}
