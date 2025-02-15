﻿using System.ComponentModel.DataAnnotations;

namespace IMS.Services.OrderAPI.Models.Domain
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        public Boolean Status {  get; set; }

        public Double OrderValue { get; set; }


        public ICollection<OrderItem> Items { get; set; }

    }
}
