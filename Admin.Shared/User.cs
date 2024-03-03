namespace Admin.Shared
{
    public class User : Raven.Identity.IdentityUser
    {
        public const string AdminRole = "Admin";
        public const string MemberRole = "Member";

        /// <summary>
        /// The user's DisplayName name.
        /// </summary>
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
