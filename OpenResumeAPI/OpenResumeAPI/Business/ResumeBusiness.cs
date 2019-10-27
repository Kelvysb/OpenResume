using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Net;

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

        public (HttpStatusCode, Resume) Create(Resume resume)
        {
            (HttpStatusCode, Resume) result = (HttpStatusCode.Conflict, null); 
            if(repository.FindByUserIdAndName(resume.UserId, resume.Name) == null)
            {
                resume.Id = repository.Insert(resume);
                if (resume.Id != 0)
                {
                    resume.Blocks.ForEach(block =>
                    {
                        block.UserId = resume.UserId;
                        block.ResumeId = resume.Id;
                        blockBusiness.Insert(block);
                        block.Fields.ForEach(field =>
                        {
                            field.UserId = block.UserId;
                            field.ResumeId = block.ResumeId;
                            field.BlockId = block.Id;
                            fieldBusiness.Insert(field);
                        });
                    });
                    result = (HttpStatusCode.OK, resume);
                }
            }
            return result;
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

        public HttpStatusCode UpdateResume(Resume resume)
        {
            HttpStatusCode result = HttpStatusCode.NotFound;
            Resume current = FindById(resume.Id);
            if(current !=  null)
            {
                Resume conflictCheck = repository.FindByUserIdAndName(resume.UserId, resume.Name);
                if (conflictCheck == null || conflictCheck.Id == current.Id)
                {
                    if (repository.Update(resume))                    
                        resume.Blocks = blockBusiness.UpdateBlocks(current.UserId, current.Id, resume.Blocks);
                    result = HttpStatusCode.OK;
                }
                else
                {
                    result = HttpStatusCode.Conflict;
                }
            }
            return result;
        }        
    }
}
