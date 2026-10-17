namespace PentiaCase.Web.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public string? OrderName { get; set; }
        public double OrderPrice { get; set; }
        public string? OrderDate { get; set; }
        public int SalesPersonId { get; set; }
    }
}
