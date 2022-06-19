namespace ICYL.API.Entity
{
    public class QuotesModel
    {
     public bool Status { get; set; }
        public string? Message { get; set; }
        public List<QuotesList> QuotesLists { get; set; }
    }
    public class QuotesList
    {
        public int QuotesId { get; set; }
        public string? QuotesTitle { get; set; }
    }
}
