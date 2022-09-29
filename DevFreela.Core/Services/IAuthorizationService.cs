namespace DevFreela.Core.Services
{
    public interface IAuthorizationService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
