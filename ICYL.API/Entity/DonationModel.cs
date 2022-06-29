namespace ICYL.API.Entity
{
    public class DonationModel
    {
        public int? Id { get; set; }
        public string? DonationName { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public string? AccountNumber { get; set; }
        public string? Message { get; set; }
    }
    public class DonationModelList
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }

        public List<DonationModel>? DonationList { get; set; }
    }
}
