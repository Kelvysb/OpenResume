using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OpenResumeAPI.Business
{
    public class BlockBusiness : CRUDBusiness<Block, IBlockRepository>, IBlockBusiness
    {

        IFieldBusiness fieldBusiness;

        public BlockBusiness(IBlockRepository repository,
                             IFieldBusiness fieldBusiness) : base(repository)
        {
            this.fieldBusiness = fieldBusiness;
        }

        public List<Block> FindByResume(int userId, int resumeId)
        {
            return repository.FindByUserAndResume(userId, resumeId);
        }

        public List<Block> UpdateBlocks(int userId, int resumeId, List<Block> blocks)
        {
            List<Block> result = new List<Block>();
            List<Block> current = repository.FindByUserAndResume(userId, resumeId);

            List<Block> delete = current.Where(curr => !blocks.Any(block => block.Id == curr.Id)).ToList();
            List<Block> update = blocks.Where(block => current.Any(curr => curr.Id == block.Id)).ToList();
            List<Block> insert = blocks.Where(block => !current.Any(curr => curr.Id == block.Id)).ToList();

            delete.ForEach(block =>
            {
                block.Fields.ForEach(field => fieldBusiness.Delete(field));
                Delete(block);
            });

            update.ForEach(block =>
            {
                repository.Update(block);
                block.Fields = fieldBusiness.UpdateFields(block.UserId, block.ResumeId, block.Id, block.Fields);
                result.Add(block);
            });

            insert.ForEach(block =>
            {
                block.Id = repository.Insert(block);
                block.Fields = fieldBusiness.UpdateFields(block.UserId, block.ResumeId, block.Id, block.Fields);
                result.Add(block);
            });

            return result;

        }
    }
}
