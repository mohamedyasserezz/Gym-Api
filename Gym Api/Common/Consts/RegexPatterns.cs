namespace Gym_Api.Common.Consts
{
    public static class RegexPatterns
    {
        public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$";
        public const string Name = @"^[a-zA-Z\s]+$";
        public const string Phone = @"^(\d{10})$";
    }
}
