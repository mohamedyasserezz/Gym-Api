namespace Gym_Api.Common
{
    public record Error(string code, string Description, int? statusCode)
    {
        public static readonly Error None = new(string.Empty, string.Empty, null);
    }
}
