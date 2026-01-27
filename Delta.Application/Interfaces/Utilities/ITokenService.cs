namespace Delta.Application.Interfaces.Utilities
{
    public interface ITokenService
    {
        string GenerateToken(
            int userId,
            string userName,
            string email,
            string role,
            string category
        );
    }
}
