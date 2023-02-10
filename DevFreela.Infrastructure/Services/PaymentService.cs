using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private readonly string _paymentsQueue;

        public PaymentService(IMessageBusService messageBusService, IConfiguration configuration)
        {
            _messageBusService = messageBusService;
            _paymentsQueue = configuration.GetSection("QueuesRabbitMq:PaymentsQueue").Value;
        }

        public void Process(PaymentInfoDTO paymentInfo)
        {

            var paymentInfoJson = JsonSerializer.Serialize(paymentInfo);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(_paymentsQueue, paymentInfoBytes);
        }
    }
}
