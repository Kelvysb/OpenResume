using BDataBaseStandard;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface IDataBaseFactory
    {
        IDataBase Build();
    }
}