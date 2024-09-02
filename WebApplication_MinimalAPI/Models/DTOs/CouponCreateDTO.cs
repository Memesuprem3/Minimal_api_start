namespace CouponAPI.Models.DTOs
{
    public class CouponCreateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Precent { get; set; }
        public bool IsActive { get; set; }
    }
}
