namespace Vector.Common.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string userName, string pwd);
        Token NewToken(string userName, string pwd);
        bool VerifyToken(string token);
    }
}