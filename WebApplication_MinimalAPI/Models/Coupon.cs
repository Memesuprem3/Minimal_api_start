namespace WebApplication_MinimalAPI.Models
{
    public class Coupon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Precent  { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created  { get; set; }
        public DateTime? LastUpdate { get; set; }

    }
}
