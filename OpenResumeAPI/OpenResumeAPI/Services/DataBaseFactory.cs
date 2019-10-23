using BDataBaseStandard;
using Microsoft.Extensions.Options;
using OpenResumeAPI.Helpers;
using OpenResumeAPI.Services.Interfaces;

namespace OpenResumeAPI.Services
{
    /// <summary>
    /// Data base factory class
    /// </summary>
    public class DataBaseFactory : IDataBaseFactory
    {
        IOptions<AppSettings> options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings"></param>
        public DataBaseFactory(IOptions<AppSettings> appSettings)
        {
            options = appSettings;
        }

        /// <summary>
        /// Build the database connection.
        /// </summary>
        /// <returns></returns>
        public IDataBase Build()
        {
            return DataBase.fnOpenConnection(options.Value.ConnectionString, DataBase.enmDataBaseType.MsSql);
        }
    }
}
