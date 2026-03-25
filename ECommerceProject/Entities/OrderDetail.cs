using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Entities
{
    internal class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public OrderDetail() { }

        public OrderDetail(Order order, Product product, int quantity)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            OrderId = order.Id;
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductId = product.Id;
            Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be at least 1.");
        }

        public override string ToString()
        {
            return $"OrderId: {OrderId}, ProductId: {ProductId}, Quantity: {Quantity}, Product: {Product?.Name}, OrderDate: {Order?.OrderDate}";
        }
    }
}
