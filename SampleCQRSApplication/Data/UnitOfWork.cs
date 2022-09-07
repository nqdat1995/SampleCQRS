using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Data.Repository;

namespace SampleCQRSApplication.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext context;
        private ITeamRepository teamRepository;

        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
        }

        public ITeamRepository TeamsRepository
        {
            get
            {

                if (this.teamRepository == null)
                {
                    this.teamRepository = new TeamRepository(context);
                }
                return teamRepository;
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
