using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Entities
{
    internal class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            OrderDate = DateTime.UtcNow;
        }

        public Order(Customer customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            CustomerId = customer.Id;
            OrderDate = DateTime.UtcNow;
            OrderDetails = new List<OrderDetail>();
        }

        public override string ToString()
        {
            return $"Id: {Id}, OrderDate: {OrderDate}, CustomerId: {CustomerId}";
        }
    }
}
