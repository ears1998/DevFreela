namespace DevFreela.Core.DTOs
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(int projectId, string creditCardNumber, string cvv, string expiresAt, string cardOwnerFullName, decimal amount)
        {
            ProjectId = projectId;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            CardOwnerFullName = cardOwnerFullName;
            Amount = amount;
        }

        public int ProjectId { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiresAt { get; set; }
        public string CardOwnerFullName { get; set; }
        public decimal Amount { get; set; }
    }
}
