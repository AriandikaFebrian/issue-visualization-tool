namespace NetCa.Domain.Constants;

/// <summary>
/// ApiConstants
/// </summary>
public abstract class ApiConstants
{
    /// <summary>
    /// ApiErrorDescription
    /// </summary>
    public readonly record struct ApiErrorDescription
    {
        /// <summary>
        /// BadRequest
        /// </summary>
        public const string BadRequest = "BadRequest";

        /// <summary>
        /// Unauthorized
        /// </summary>
        public const string Unauthorized = "Unauthorized";

        /// <summary>
        /// Forbidden
        /// </summary>
        public const string Forbidden = "Forbidden";

        /// <summary>
        /// InternalServerError
        /// </summary>
        public const string InternalServerError = "InternalServerError";
    }
}
