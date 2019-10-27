using BDataBaseStandard;
using Moq;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Tests.Helpers
{

    class DataBaseHelper
    {
        private Mock<IDataBaseFactory> dataBaseFactory;
        private Mock<IDataBase> dataBase;

        public DataBaseHelper()
        {
            dataBaseFactory = new Mock<IDataBaseFactory>();
            dataBase = new Mock<IDataBase>();
        }

        public DataBaseHelper Add<Model>(List<Model> expectedList, int expectedNumber) where Model : ModelBase
        {            
            dataBase.Setup(repository => repository.fnExecute<Model>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>())).Returns(expectedList);
            dataBase.Setup(repository => repository.fnExecute<int>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>())).Returns(new List<int>() { expectedNumber });
            return this;
        }

        public DataBaseHelper AddByValue<Model>(List<Model> expectedList, string key, object value) where Model : ModelBase
        {
            dataBase.Setup(repository => repository.fnExecute<Model>(It.IsAny<string>(), It.Is<Dictionary<string, object>>(item => item.ContainsKey(key) && item[key].Equals(value)))).Returns(expectedList);
            return this;
        }

        public IDataBaseFactory Build()
        {
            dataBaseFactory.Setup(factory => factory.Build()).Returns(dataBase.Object);
            return dataBaseFactory.Object;
        }
    }
}
