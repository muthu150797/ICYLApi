namespace ICYL.API.Entity
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; } ="";
        public string? LastName { get; set; } = "";
        public string? CompanyName { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Password { get; set; } = "";
        public string? PhoneNumber { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? City { get; set; } = "";
        public string? State { get; set; } = "";
        public string? Zip { get; set; } = "";
        public int TotalDonation { get; set; } = 0;

        public string? Message { get; set; } = "";
        public string? Role { get; set; } = "";
        public int StatusCode { get; set; }
        public bool Status { get; set; }
    }
}
