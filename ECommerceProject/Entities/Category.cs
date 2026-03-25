using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Entities
{
    internal class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public Category(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return $"Id:{Id}, Category: {Name}";
        }
    }
}
