using ICYL.API.Data;
using ICYL.API.Entity;
using static ICYL.API.Entity.DonationAmount;

namespace ICYL.API.Services
{
    public class DonationService
    {
        Donations donation = new Donations();
        public DonationModelList GetDonations(dynamic config)
        {
            DonationModelList response = donation.GetDonations(config);
            return response;
        }
        public DonationModelList GetAllCategory(dynamic config)
        {
            DonationModelList response = donation.GetAllCategory(config);
            return response;
        }
        public AmountModelList GetDonationAmount()
        {
            AmountModelList response = donation.GetDonationAmount();
            return response;
        }
        public QuotesModel GetQuotes()
        {
            QuotesModel response = donation.GetQuotes();
            return response;
        }
        public DonationHistory GetDonationHistory(Login detail) { 
            DonationHistory response = donation.GetDonationHistory(detail);
            return response;
        }
        public QuotesReponseModel GetDonationCount(Login detail)
        {
            QuotesReponseModel response = donation.GetDonationCount(detail);
            return response;
        }

    }
}
