using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Exceptions;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Business
{
    public class ResumeBusiness : CRUDBusiness<Resume, IResumeRepository>, IResumeBusiness
    {
        IBlockBusiness blockBusiness;
        IFieldBusiness fieldBusiness;

        public ResumeBusiness(IResumeRepository repository,
                              IBlockBusiness blockBusiness,
                              IFieldBusiness fieldBusiness) : base(repository)
        {
            this.blockBusiness = blockBusiness;
            this.fieldBusiness = fieldBusiness;
        }

        public Resume Create(Resume resume)
        {
            if (!CheckResume(resume.UserId, resume.Name))
            {
                resume.Id = repository.Insert(resume);
                CreateBlocks(resume);
            }
            else
            {
                throw new DuplicatedException<Resume>(resume);
            }
            return resume;
        }

        private void CreateBlocks(Resume resume)
        {
            resume.Blocks.ForEach(block =>
            {
                block.UserId = resume.UserId;
                block.ResumeId = resume.Id;
                blockBusiness.Insert(block);
                CreateFields(block);
            });
        }

        private void CreateFields(Block block)
        {
            block.Fields.ForEach(field =>
            {
                field.UserId = block.UserId;
                field.ResumeId = block.ResumeId;
                field.BlockId = block.Id;
                fieldBusiness.Insert(field);
            });
        }

        public Resume Find(string user, string resume)
        {
            Resume result = repository.Find(user, resume);
            if (result != null)
                result.Blocks = FindBlocks(result);
            return result;
        }

        private Resume FindById(int resumeId)
        {
            Resume result = repository.ByID(resumeId);
            if (result != null)
                result.Blocks = FindBlocks(result);
            return result;
        }

        private List<Block> FindBlocks(Resume resume)
        {
            List<Block> result = blockBusiness.FindByResume(resume.UserId, resume.Id);
            result.ForEach(block =>
                block.Fields = fieldBusiness.FindByBlock(block.UserId, block.ResumeId, block.Id));
            return result;
        }

        public List<Resume> List(int userId)
        {
            return repository.FindByUserId(userId);
        }

        public void UpdateResume(Resume resume)
        {
            Resume current = FindById(resume.Id);
            if (CheckResumeConflict(resume.UserId, resume.Name, current.Id))
            {
                repository.Update(resume);
                resume.Blocks = blockBusiness.UpdateBlocks(current.UserId, current.Id, resume.Blocks);
            }
            else
            {
                throw new DuplicatedException<Resume>();
            }
        }

        private bool CheckResume(int userId, string name)
        {
            try
            {
                repository.FindByUserIdAndName(userId, name);
                return true;
            }
            catch (NotFoundException<Resume>)
            {
                return false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private bool CheckResumeConflict(int userId, string name, int resumeId)
        {
            try
            {
                Resume conflictCheck = repository.FindByUserIdAndName(userId, name);
                return conflictCheck.Id == resumeId;
            }
            catch (NotFoundException<Resume>)
            {
                return false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
