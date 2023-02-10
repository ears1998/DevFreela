using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }

        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project != null) project.Finish();

            var paymentInfoDto = new PaymentInfoDTO(
                                        request.Id, 
                                        request.CreditCardNumber, 
                                        request.Cvv,
                                        request.ExpiresAt,
                                        request.CardOwnerFullName,
                                        project.TotalCost);

            _paymentService.Process(paymentInfoDto);

            project.SetPaymentPendingStatus();

            await _projectRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
