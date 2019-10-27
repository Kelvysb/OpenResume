using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OpenResumeAPI.Business
{
    public class FieldBusiness : CRUDBusiness<Field, IFieldRepository>, IFieldBusiness
    {
        public FieldBusiness(IFieldRepository repository) : base(repository) { }

        public List<Field> FindByBlock(int userId, int resumeId, int BlockId)
        {
            return repository.FindByUserAndResumeAndBlock(userId, resumeId, BlockId);
        }

        public List<Field> UpdateFields(int userId, int resumeId, int blockId, List<Field> fields)
        {
            List<Field> result = new List<Field>();
            List<Field> current = repository.FindByUserAndResumeAndBlock(userId, resumeId, blockId);

            List<Field> delete = current.Where(curr => !fields.Any(field => field.Id == curr.Id)).ToList();
            List<Field> update = fields.Where(field => current.Any(curr => curr.Id == field.Id)).ToList();
            List<Field> insert = fields.Where(field => !current.Any(curr => curr.Id == field.Id)).ToList();

            delete.ForEach(field => Delete(field));

            update.ForEach(field =>
            {
                repository.Update(field);
                result.Add(field);                
            });

            insert.ForEach(field =>
            {
                field.Id = repository.Insert(field);
                result.Add(field);
            });

            return result;
        }
    }
}
