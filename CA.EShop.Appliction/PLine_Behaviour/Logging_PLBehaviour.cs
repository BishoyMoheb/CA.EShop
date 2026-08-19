using System;
using System.Threading;
using System.Threading.Tasks;
using CA.EShop.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CA.EShop.Application.PLine_Behaviour
{
    public class Logging_PLBehaviour<TRquest, TResponse>
        : IPipelineBehavior<TRquest, TResponse>
        where TRquest : IRequest<TResponse>
        where TResponse : GenResult
    {
        private readonly ILogger<Logging_PLBehaviour<TRquest, TResponse>> _loggerI_LPB;

        public Logging_PLBehaviour(ILogger<Logging_PLBehaviour<TRquest, TResponse>> LoggerI_LPB)
        {
            _loggerI_LPB = LoggerI_LPB;
        }

        public async Task<TResponse> Handle(TRquest tRequest,
            RequestHandlerDelegate<TResponse> RHDelegate_Next,
            CancellationToken CToken)
        {
            _loggerI_LPB.LogInformation(
                "Starting request {RequestName}, {DateTimeUTC_Now}",
                typeof(TRquest).Name, DateTime.UtcNow);
            var tResponseResult = await RHDelegate_Next();
            if (tResponseResult.IsFailure)
            {
                _loggerI_LPB.LogError(
                "Request failure {RequestName}, {Error}, {DateTimeUTC_Now}",
                typeof(TRquest).Name, tResponseResult.genError, DateTime.UtcNow);
            }
            _loggerI_LPB.LogInformation(
                "Request completed {RequestName}, {DateTimeUTC_Now}",
                typeof(TRquest).Name, DateTime.UtcNow);
            return tResponseResult;
        }
    }
}