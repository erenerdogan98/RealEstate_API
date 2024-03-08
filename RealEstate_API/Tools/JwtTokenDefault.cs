namespace RealEstate_API.Tools
{
    public class JwtTokenDefault
    {
        public const string ValidAudience = "https://localhost"; 
        public const string ValidIssuer = "https://localhost"; 
        public const string Key = "realEsTate.01234534asd.AspCore8.0.1+-*";  // will change , it is not good to take this Key value here 
        public const int Expire = 10; // 10 minutes , default value is 1 hour
    }
}
