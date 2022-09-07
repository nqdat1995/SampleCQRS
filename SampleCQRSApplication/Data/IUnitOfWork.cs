using SampleCQRSApplication.Data.Repository;

namespace SampleCQRSApplication.Data
{
    public interface IUnitOfWork
    {
        public ITeamRepository TeamsRepository { get; }
        public Task Save();
    }
}