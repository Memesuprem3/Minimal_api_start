﻿namespace WebApplication_MinimalAPI.Models.DTOs
{
    public class CouponUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Precent { get; set; }
        public bool IsActive { get; set; }

    }
}
