using SampleCQRSApplication.Data.Repository;

namespace SampleCQRSApplication.Data
{
    public interface IUnitOfWork
    {
        IBetRepository BetRepository { get; }
        IMatchRepository MatchRepository { get; }
        IRoundRepository RoundRepository { get; }
        ISeasonRepository SeasonRepository { get; }
        ITeamRepository TeamsRepository { get; }
        ITournamentRepository TournamentRepository { get; }
        ITournamentRoundRepository TournamentRoundRepository { get; }
        ITournamentSeasonRepository TournamentSeasonRepository { get; }
        ITournamentTeamRepository TournamentTeamRepository { get; }
        public Task Save();
    }
}