using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Business
{
    public class BlockBusiness : CRUDBusiness<Block, IBlockRepository>, IBlockBusiness
    {
        public BlockBusiness(IBlockRepository repository) : base(repository) { }
    }
}
