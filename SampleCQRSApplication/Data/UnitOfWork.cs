using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Data.Repository;

namespace SampleCQRSApplication.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext context;
        private ITeamRepository teamRepository;
        private IBetRepository betRepository;
        private IMatchRepository matchRepository;
        private IRoundRepository roundRepository;
        private ISeasonRepository seasonRepository;
        private ITournamentRepository tournamentRepository;
        private ITournamentRoundRepository tournamentRoundRepository;
        private ITournamentSeasonRepository tournamentSeasonRepository;
        private ITournamentTeamRepository tournamentTeamRepository;
        private IUserRepository userRepository;
        private ISendMailRepository sendMailRepository;

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
        public IBetRepository BetRepository
        {
            get
            {

                if (this.betRepository == null)
                {
                    this.betRepository = new BetRepository(context);
                }
                return betRepository;
            }
        }
        public IMatchRepository MatchRepository
        {
            get
            {

                if (this.matchRepository == null)
                {
                    this.matchRepository = new MatchRepository(context);
                }
                return matchRepository;
            }
        }
        public IRoundRepository RoundRepository
        {
            get
            {

                if (this.roundRepository == null)
                {
                    this.roundRepository = new RoundRepository(context);
                }
                return roundRepository;
            }
        }
        public ISeasonRepository SeasonRepository
        {
            get
            {

                if (this.seasonRepository == null)
                {
                    this.seasonRepository = new SeasonRepository(context);
                }
                return seasonRepository;
            }
        }
        public ITournamentRepository TournamentRepository
        {
            get
            {

                if (this.tournamentRepository == null)
                {
                    this.tournamentRepository = new TournamentRepository(context);
                }
                return tournamentRepository;
            }
        }
        public ITournamentRoundRepository TournamentRoundRepository
        {
            get
            {

                if (this.tournamentRoundRepository == null)
                {
                    this.tournamentRoundRepository = new TournamentRoundRepository(context);
                }
                return tournamentRoundRepository;
            }
        }
        public ITournamentSeasonRepository TournamentSeasonRepository
        {
            get
            {

                if (this.tournamentSeasonRepository == null)
                {
                    this.tournamentSeasonRepository = new TournamentSeasonRepository(context);
                }
                return tournamentSeasonRepository;
            }
        }
        public ITournamentTeamRepository TournamentTeamRepository
        {
            get
            {

                if (this.tournamentTeamRepository == null)
                {
                    this.tournamentTeamRepository = new TournamentTeamRepository(context);
                }
                return tournamentTeamRepository;
            }
        }
        public ISendMailRepository SendMailRepository
        {
            get
            {

                if (this.sendMailRepository == null)
                {
                    this.sendMailRepository = new SendMailRepository(context);
                }
                return sendMailRepository;
            }
        }

        //Authentication
        public IUserRepository UserRepository {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
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
