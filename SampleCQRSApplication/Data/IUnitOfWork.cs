using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Data.Repository;

namespace SampleCQRSApplication.Data
{
    public interface IUnitOfWork
    {
        public AppDBContext Context();
        IBetRepository BetRepository { get; }
        IMatchRepository MatchRepository { get; }
        IRoundRepository RoundRepository { get; }
        ISeasonRepository SeasonRepository { get; }
        ITeamRepository TeamRepository { get; }
        ITournamentRepository TournamentRepository { get; }
        ITournamentRoundRepository TournamentRoundRepository { get; }
        ITournamentSeasonRepository TournamentSeasonRepository { get; }
        ITournamentTeamRepository TournamentTeamRepository { get; }
        IUserRepository UserRepository { get; }
        ISendMailRepository SendMailRepository { get; }
        public Task Save();
    }
}