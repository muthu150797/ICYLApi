namespace ICYL.API.Entity
{
    public class OTPResponse
    {
     public bool Status { get;set; }
        public string? Message { get; set; }
        public string? OTP { get; set; }
        public int? UserId { get; set; }
        public string? NewPassword { get; set; }
    }
}
