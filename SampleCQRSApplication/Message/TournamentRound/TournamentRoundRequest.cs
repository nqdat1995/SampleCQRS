namespace SampleCQRSApplication.Message
{
    public class TournamentRoundRequest
    {
        public int RoundId { get; set; }
        public int TournamentId { get; set; }
    }
}
