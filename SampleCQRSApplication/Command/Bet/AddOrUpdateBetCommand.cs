using AutoMapper;
using MediatR;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateBetCommand : IRequest<IResultResponse>
    {
        public int MatchId { get; set; }
        public BetRequest Bet { get; set; }
    }
    public class AddOrUpdateBetCommandHandler : IRequestHandler<AddOrUpdateBetCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateBetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateBetCommand request, CancellationToken cancellationToken)
        {
            Match match = null;
            User user = null;

            await Task.WhenAll(
                Task.Run(async () =>
                {
                    match = await unitOfWork.MatchRepository.GetByID(request.MatchId);
                }),
                Task.Run(async () =>
                {
                    user = await unitOfWork.UserRepository.GetByID(request.Bet.UserId);
                })
                );

            if (match == null || user == null)
            {
                return ResultResponse.BuildResponse(0);
            }

            var bet = unitOfWork.BetRepository.Get(filter: x => x.Match == match && x.User == user).FirstOrDefault();

            if (bet == null) //Create
            {
                var tempBet = new Bet
                {
                    Match = match,
                    User = user
                };

                if (request.Bet.IsDraw)
                {
                    tempBet.IsDraw = true;
                    unitOfWork.BetRepository.Insert(tempBet);
                    return ResultResponse.BuildResponse(tempBet.Id);
                }

                var team = await unitOfWork.TeamRepository.GetByID(request.Bet.TeamId);
                if (team == null)
                    return ResultResponse.BuildResponse(0);

                tempBet.Team = team;
                unitOfWork.BetRepository.Insert(tempBet);
                return ResultResponse.BuildResponse(tempBet.Id);
            }

            if (request.Bet.IsDraw)
            {
                bet.IsDraw = true;
                bet.TeamId = 0;
                unitOfWork.BetRepository.Update(bet);
                return ResultResponse.BuildResponse(bet.Id);
            }

            var team2 = await unitOfWork.TeamRepository.GetByID(request.Bet.TeamId);
            if (team2 == null)
                return ResultResponse.BuildResponse(0);

            bet.IsDraw = false;
            bet.Team = team2;
            unitOfWork.BetRepository.Update(bet);
            return ResultResponse.BuildResponse(bet.Id);
        }
    }
}
