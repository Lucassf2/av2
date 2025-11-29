using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HamburgueriaBlazor.Data
{
    public class OrderHeader
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; set; }

        [Required]
        [Display(Name = "Shipping")]
        public decimal Shipping { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
