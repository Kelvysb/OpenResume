using System;
using Xunit;
using OpenResumeAPI.Models;
using System.Collections.Generic;
using OpenResumeAPI.Controllers;
using OpenResumeAPI.Services;
using OpenResumeAPI.Business;
using OpenResumeAPI.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OpenResumeAPI.Services.Interfaces;

namespace OpenResumeAPI.Tests
{
    public class ResumeTests
    {

        ResumeController resumeController;
        List<Resume> expectedResumes;
        List<Block> expectedBlocks;
        List<Field> expectedFields;
        List<Resume> returnResumes;
        List<Block> returnBlocks;
        List<Field> returnFields;     

        public ResumeTests()
        {

            expectedFields = new List<Field>()
            {
                new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                new Field(2, 1, 1, 1, "Field 1 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false),
                new Field(3, 1, 1, 2, "Field 2 - 1","Writer", 1, "TOPIC", "Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-10), DateTime.Now.AddYears(-5), false),
                new Field(4, 1, 1, 2, "Field 2 - 2","Freelancer", 2, "TOPIC", "Freelancer Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-5), DateTime.Now, true)

            };

            expectedBlocks = new List<Block>()
            {
                new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", expectedFields.Where(field => field.BlockId == 1).ToList()),
                new Block(2, 1, 1,"Block 2","Professional information in English", 2, "PROFESSIONAL", "Professional Information", "", expectedFields.Where(field => field.BlockId == 2).ToList())
            };

            expectedResumes = new List<Resume>()
            {
                new Resume(1, 1,"Resume1","Resume 1 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1", 
                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, expectedBlocks)
            };

            returnResumes = new List<Resume>()
            {
                new Resume(1, 1,"Resume1","Resume 1 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1",
                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, null)
            };

            returnBlocks = new List<Block>()
            {
                new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null),
                new Block(2, 1, 1,"Block 2","Professional information in English", 2, "PROFESSIONAL", "Professional Information", "", null)
            };

            returnFields = new List<Field>()
            {
                new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                new Field(2, 1, 1, 1, "Field 1 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false),
                new Field(3, 1, 1, 2, "Field 2 - 1","Writer", 1, "TOPIC", "Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-10), DateTime.Now.AddYears(-5), false),
                new Field(4, 1, 1, 2, "Field 2 - 2","Freelancer", 2, "TOPIC", "Freelancer Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-5), DateTime.Now, true)

            };

            IDataBaseFactory dataBaseFactory = new DataBaseHelper()
                                                   .Add<Resume>(returnResumes, 3)
                                                   .Add<Block>(returnBlocks, 3)                                                   
                                                   .Add<Field>(returnFields, 3)
                                                   .AddByValue<Field>(returnFields.Where(item => item.BlockId == 1).ToList(), "BLOCKID", 1)
                                                   .AddByValue<Field>(returnFields.Where(item => item.BlockId == 2).ToList(), "BLOCKID", 2)
                                                   .AddByValue<Resume>(new List<Resume>(), "NAME", "Resume2")
                                                   .Build();

            resumeController = new ResumeController(
                                    new ResumeBusiness(
                                        new ResumeRepository(dataBaseFactory),
                                        new BlockBusiness(new BlockRepository(dataBaseFactory), 
                                                         new FieldBusiness(new FieldRepository(dataBaseFactory))),
                                        new FieldBusiness(new FieldRepository(dataBaseFactory))),
                                    LoggerHelper.Create<ResumeController>(),
                                    ValidatorHelper.Create(1, "FAKE_TOKEN"));

            resumeController.ControllerContext = HttpContextHelper.Create("FAKE_TOKEN");
        }

        [Fact]
        public void Find()
        {
            string inputUser = "Lovecraft";
            string inputResume = "Resume_1";

            var result = resumeController.Find(inputUser, inputResume);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<Resume>>(result);
            Assert.Equal(200, (result.Result as OkObjectResult).StatusCode);
            Assert.Equal(expectedResumes.First().Id, ((result.Result as OkObjectResult).Value as Resume).Id);
            Assert.Equal(expectedResumes.First().Blocks.Count, ((result.Result as OkObjectResult).Value as Resume).Blocks.Count);
            Assert.Equal(expectedResumes.First().Blocks.First().Fields.Count, ((result.Result as OkObjectResult).Value as Resume).Blocks.First().Fields.Count);
            Assert.Equal(expectedResumes.First().Blocks.Last().Fields.Count, ((result.Result as OkObjectResult).Value as Resume).Blocks.Last().Fields.Count);

        }

        [Fact]
        public void List()
        {
            int inputUser = 1;

            var result = resumeController.List(inputUser);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<List<Resume>>>(result);
            Assert.Equal(200, (result.Result as OkObjectResult).StatusCode);
            Assert.Equal(expectedResumes.Count, ((result.Result as OkObjectResult).Value as List<Resume>).Count);
            Assert.Equal(expectedResumes.First().Id, ((result.Result as OkObjectResult).Value as List<Resume>).First().Id);
            
        }

        [Fact]
        public void CreateDuplicated()
        {
            Resume inputResume = new Resume(0, 1, "Resume1", "Resume 1 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1",
                                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, null)
            {
                Blocks = new List<Block>()
                {
                    new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                            new Field(2, 1, 1, 1, "Field 1 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false)
                        }
                    },
                    new Block(2, 1, 1,"Block 2","Professional information in English", 2, "PROFESSIONAL", "Professional Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(3, 1, 1, 2, "Field 2 - 1","Writer", 1, "TOPIC", "Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-10), DateTime.Now.AddYears(-5), false),
                            new Field(4, 1, 1, 2, "Field 2 - 2","Freelancer", 2, "TOPIC", "Freelancer Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-5), DateTime.Now, true)
                        }
                    }
                }
            };

            var result = resumeController.Create(inputResume);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<Resume>>(result);
            Assert.Equal(409, (result.Result as ObjectResult).StatusCode);
        }

        [Fact]
        public void Create()
        {
            Resume inputResume = new Resume(0, 1, "Resume2", "Resume 2 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1",
                                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, null)
            {
                Blocks = new List<Block>()
                {
                    new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                            new Field(2, 1, 1, 1, "Field 1 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false)
                        }
                    },
                    new Block(2, 1, 1,"Block 2","Professional information in English", 2, "PROFESSIONAL", "Professional Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(3, 1, 1, 2, "Field 2 - 1","Writer", 1, "TOPIC", "Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-10), DateTime.Now.AddYears(-5), false),
                            new Field(4, 1, 1, 2, "Field 2 - 2","Freelancer", 2, "TOPIC", "Freelancer Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-5), DateTime.Now, true)
                        }
                    }
                }
            };

            var result = resumeController.Create(inputResume);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<Resume>>(result);
            Assert.Equal(200, (result.Result as ObjectResult).StatusCode);
            Assert.Equal(3, ((result.Result as ObjectResult).Value as Resume).Id);
        }

        [Fact]
        public void Update()
        {
            Resume inputResume = new Resume(1, 1, "Resume 1", "Resume 1 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1",
                                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, null)
            {
                Blocks = new List<Block>()
                {
                    new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                            new Field(2, 1, 1, 1, "Field 1 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false)
                        }
                    },
                    new Block(2, 1, 1,"Block 2","Professional information in English", 2, "PROFESSIONAL", "Professional Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(3, 1, 1, 2, "Field 2 - 1","Writer", 1, "TOPIC", "Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-10), DateTime.Now.AddYears(-5), false),
                            new Field(4, 1, 1, 2, "Field 2 - 2","Freelancer", 2, "TOPIC", "Freelancer Writer of weird fiction and horror fiction.", DateTime.Now.AddYears(-5), DateTime.Now, true)
                        }
                    }
                }
            };

            var result = resumeController.Update(inputResume);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);
        }

        [Fact]
        public void UpdateBlockChanges()
        {
            Resume inputResume = new Resume(1, 1, "Resume 1", "Resume 1 in English", 1, "www.open-resume.com\\Lovecraft\\Resume_1",
                                            "FAKE_TOKEN", "us-en", "euro", 0, DateTime.Now, DateTime.Now, null)
            {
                Blocks = new List<Block>()
                {
                    new Block(1, 1, 1,"Block 1","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(1, 1, 1, 1, "Field 1 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                            new Field(0, 1, 1, 1, "Field 1 - 3 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false)
                        }
                    },
                    new Block(0, 1, 1,"Block 3","Personal information in English", 1, "PERSONAL", "Persoanl Information", "", null)
                    {
                        Fields = new List<Field>()
                        {
                            new Field(0, 1, 1, 3, "Field 3 - 1","Personal information Field 1 in English", 1, "NAME", "H.P Lovecraft", DateTime.Now, DateTime.Now, false),
                            new Field(0, 1, 1, 3, "Field 3 - 2 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false),
                            new Field(0, 1, 1, 3, "Field 3 - 3 ","Personal information Field 2 in English", 2, "ADDRESS", "Providence, Rhode Island, U.S.", DateTime.Now, DateTime.Now, false)
                        }
                    }
                }
            };

            var result = resumeController.Update(inputResume);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, (result as OkResult).StatusCode);
        }


    }
}
