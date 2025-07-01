namespace NetCa.Domain.Constants;
public abstract class ApiConstants
{
    public readonly record struct ApiErrorDescription
    {
        public const string BadRequest = "BadRequest";
        public const string Unauthorized = "Unauthorized";
        public const string Forbidden = "Forbidden";
        public const string InternalServerError = "InternalServerError";
    }
}
