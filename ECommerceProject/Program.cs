//using ECommerceProject.Data;
//using ECommerceProject.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ECommerceProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();
//            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;");

//            using var context = new ECommerceDbContext(optionsBuilder.Options);

//            #region Seed Data (Create)
//            Console.WriteLine("=== CREATING DATA ===\n");

//            Create Categories
//            var electronics = new Category { Name = "Electronics" };
//            var clothing = new Category { Name = "Clothing" };
//            var sports = new Category { Name = "Sports" };
//            var books = new Category { Name = "Books" };
//            var home = new Category { Name = "Home & Garden" };

//            context.Categories.AddRange(electronics, clothing, sports, books, home);
//            context.SaveChanges();
//            Console.WriteLine("✓ Created 5 Categories");

//            Create Products
//            var laptop = new Product { Name = "Gaming Laptop", Price = 1500m, CategoryId = electronics.Id };
//            var smartphone = new Product { Name = "Smartphone Pro", Price = 999m, CategoryId = electronics.Id };
//            var tablet = new Product { Name = "Tablet 12.9\"", Price = 599m, CategoryId = electronics.Id };
//            var headphones = new Product { Name = "Wireless Headphones", Price = 199m, CategoryId = electronics.Id };

//            var tshirt = new Product { Name = "Cotton T-Shirt", Price = 25m, CategoryId = clothing.Id };
//            var jeans = new Product { Name = "Blue Jeans", Price = 60m, CategoryId = clothing.Id };
//            var jacket = new Product { Name = "Winter Jacket", Price = 120m, CategoryId = clothing.Id };
//            var sneakers = new Product { Name = "Athletic Sneakers", Price = 85m, CategoryId = clothing.Id };

//            var basketball = new Product { Name = "Professional Basketball", Price = 45m, CategoryId = sports.Id };
//            var yogamat = new Product { Name = "Yoga Mat", Price = 30m, CategoryId = sports.Id };
//            var dumbbell = new Product { Name = "10kg Dumbbell", Price = 35m, CategoryId = sports.Id };

//            var csharpbook = new Product { Name = "C# Advanced", Price = 55m, CategoryId = books.Id };
//            var pythonbook = new Product { Name = "Python for Data Science", Price = 50m, CategoryId = books.Id };

//            var flowerpot = new Product { Name = "Ceramic Flowerpot", Price = 20m, CategoryId = home.Id };

//            context.Products.AddRange(laptop, smartphone, tablet, headphones, tshirt, jeans, jacket, sneakers,
//                                     basketball, yogamat, dumbbell, csharpbook, pythonbook, flowerpot);
//            context.SaveChanges();
//            Console.WriteLine("✓ Created 14 Products\n");

//            Create Customers
//            var alice = new Customer { Name = "Alice Johnson", Email = "alice.johnson@example.com" };
//            var bob = new Customer { Name = "Bob Smith", Email = "bob.smith@example.com" };
//            var charlie = new Customer { Name = "Charlie Brown", Email = "charlie.brown@example.com" };
//            var diana = new Customer { Name = "Diana Prince", Email = "diana.prince@example.com" };
//            var eve = new Customer { Name = "Eve Wilson", Email = "eve.wilson@example.com" };

//            context.Customers.AddRange(alice, bob, charlie, diana, eve);
//            context.SaveChanges();
//            Console.WriteLine("✓ Created 5 Customers\n");

//            Create Orders
//            var order1 = new Order { CustomerId = alice.Id, OrderDate = DateTime.Now.AddDays(-10) };
//            var order2 = new Order { CustomerId = bob.Id, OrderDate = DateTime.Now.AddDays(-8) };
//            var order3 = new Order { CustomerId = charlie.Id, OrderDate = DateTime.Now.AddDays(-5) };
//            var order4 = new Order { CustomerId = diana.Id, OrderDate = DateTime.Now.AddDays(-3) };
//            var order5 = new Order { CustomerId = eve.Id, OrderDate = DateTime.Now.AddDays(-1) };

//            context.Orders.AddRange(order1, order2, order3, order4, order5);
//            context.SaveChanges();
//            Console.WriteLine("✓ Created 5 Orders\n");

//            Create Order Details
//            context.OrderDetails.AddRange(
//                new OrderDetail { OrderId = order1.Id, ProductId = laptop.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order1.Id, ProductId = headphones.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order1.Id, ProductId = csharpbook.Id, Quantity = 2 },

//                new OrderDetail { OrderId = order2.Id, ProductId = smartphone.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order2.Id, ProductId = jacket.Id, Quantity = 1 },

//                new OrderDetail { OrderId = order3.Id, ProductId = tablet.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order3.Id, ProductId = yogamat.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order3.Id, ProductId = tshirt.Id, Quantity = 3 },

//                new OrderDetail { OrderId = order4.Id, ProductId = basketball.Id, Quantity = 2 },
//                new OrderDetail { OrderId = order4.Id, ProductId = dumbbell.Id, Quantity = 1 },

//                new OrderDetail { OrderId = order5.Id, ProductId = sneakers.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order5.Id, ProductId = pythonbook.Id, Quantity = 1 },
//                new OrderDetail { OrderId = order5.Id, ProductId = flowerpot.Id, Quantity = 4 }
//            );
//            context.SaveChanges();
//            Console.WriteLine("✓ Created 13 Order Details\n");
//            #endregion

//            #region Read Data (Read)
//            Console.WriteLine("\n=== READING DATA ===\n");

//            // Read: Products that have been ordered
//            Console.WriteLine("--- Products That Have Been Ordered ---");
//            var orderedProducts = context.Products
//                .Include(p => p.OrderDetails)
//                .ThenInclude(od => od.Order)
//                .Where(p => p.OrderDetails.Any())
//                .ToList();

//            foreach (var product in orderedProducts)
//            {
//                int totalQuantity = product.OrderDetails.Sum(od => od.Quantity);
//                Console.WriteLine($"  ✓ {product.Name} - Ordered {totalQuantity} times (Total Value: ${product.Price * totalQuantity:F2})");
//            }

//            // Read: Products that have NOT been ordered
//            Console.WriteLine("\n--- Products NOT Ordered Yet ---");
//            var unorderedProducts = context.Products
//                .Include(p => p.OrderDetails)
//                .Where(p => !p.OrderDetails.Any())
//                .ToList();

//            foreach (var product in unorderedProducts)
//            {
//                Console.WriteLine($"  ✗ {product.Name} - ${product.Price:F2}");
//            }

//            // Read: All customers with their order count
//            Console.WriteLine("\n--- Customers & Their Orders ---");
//            var customersWithOrders = context.Customers
//                .Include(c => c.Orders)
//                .ToList();

//            foreach (var customer in customersWithOrders)
//            {
//                int orderCount = customer.Orders.Count;
//                Console.WriteLine($"  {customer.Name} ({customer.Email}) - {orderCount} order(s)");
//            }

//            // Read: Orders with all details
//            Console.WriteLine("\n--- All Orders with Details ---");
//            var allOrders = context.Orders
//                .Include(o => o.Customer)
//                .Include(o => o.OrderDetails)
//                .ThenInclude(od => od.Product)
//                .ToList();

//            foreach (var order in allOrders)
//            {
//                decimal orderTotal = order.OrderDetails.Sum(od => od.Product.Price * od.Quantity);
//                Console.WriteLine($"  Order #{order.Id} - Customer: {order.Customer.Name} - Date: {order.OrderDate:yyyy-MM-dd} - Total: ${orderTotal:F2}");
//                foreach (var detail in order.OrderDetails)
//                {
//                    Console.WriteLine($"    └─ {detail.Product.Name} x{detail.Quantity} @ ${detail.Product.Price:F2}");
//                }
//            }

//            // Read: Most valuable orders
//            Console.WriteLine("\n--- Top 3 Most Valuable Orders ---");
//            var topOrders = context.Orders
//                .Include(o => o.Customer)
//                .Include(o => o.OrderDetails)
//                .ThenInclude(od => od.Product)
//                .AsEnumerable()
//                .Select(o => new
//                {
//                    Order = o,
//                    Total = o.OrderDetails.Sum(od => od.Product.Price * od.Quantity)
//                })
//                .OrderByDescending(x => x.Total)
//                .Take(3)
//                .ToList();

//            foreach (var item in topOrders)
//            {
//                Console.WriteLine($"  Order #{item.Order.Id} ({item.Order.Customer.Name}): ${item.Total:F2}");
//            }

//            // Read: Products by category
//            Console.WriteLine("\n--- Products by Category ---");
//            var categories = context.Categories
//                .Include(c => c.Products)
//                .ToList();

//            foreach (var cat in categories)
//            {
//                Console.WriteLine($"  {cat.Name} ({cat.Products.Count} products)");
//                foreach (var prod in cat.Products)
//                {
//                    Console.WriteLine($"    • {prod.Name} - ${prod.Price:F2}");
//                }
//            }
//            #endregion

//            #region Update Data (Update)
//            Console.WriteLine("\n\n=== UPDATING DATA ===\n");

//        Update: Transfer Alice's orders to Bob
//            Console.WriteLine("--- Transferring Alice's Orders to Bob ---");
//            var aliceOrders = context.Orders.Where(o => o.CustomerId == alice.Id).ToList();
//            var ordersTransferred = 0;

//            foreach (var order in aliceOrders)
//            {
//                order.CustomerId = bob.Id;
//                order.Customer = bob;
//                ordersTransferred++;
//            }

//            context.UpdateRange(aliceOrders);
//            context.SaveChanges();
//            Console.WriteLine($"✓ Transferred {ordersTransferred} order(s) from Alice to Bob\n");

//        Update: Apply 10 % discount to Electronics category
//            Console.WriteLine("--- Applying 10% Discount to Electronics ---");
//            var electronicsProducts = context.Products
//                .Where(p => p.CategoryId == electronics.Id)
//                .ToList();

//            foreach (var product in electronicsProducts)
//            {
//                var oldPrice = product.Price;
//                product.Price = product.Price * 0.90m; // 10% discount
//                Console.WriteLine($"  {product.Name}: ${oldPrice:F2} → ${product.Price:F2}");
//            }

//            context.UpdateRange(electronicsProducts);
//            context.SaveChanges();
//            Console.WriteLine();

//        Update: Increase quantities in an order

//       Console.WriteLine("--- Increasing Quantities in Order #3 ---");
//            var order3Details = context.OrderDetails
//                .Include(od => od.Product)
//                .Where(od => od.OrderId == order3.Id)
//                .ToList();

//            foreach (var detail in order3Details)
//            {
//                var oldQty = detail.Quantity;
//                detail.Quantity += 2;
//                Console.WriteLine($"  {detail.Product.Name}: {oldQty} → {detail.Quantity} units");
//            }

//            context.UpdateRange(order3Details);
//            context.SaveChanges();
//            Console.WriteLine();
//            #endregion

//            #region Delete Data (Delete)
//            Console.WriteLine("\n=== DELETING DATA ===\n");

//        Delete: Remove all order details for a specific product(Yoga Mat)

//       Console.WriteLine("--- Deleting Order Details for 'Yoga Mat' ---");
//            var yogamatDetails = context.OrderDetails
//           .Include(od => od.Product)
//           .Where(od => od.Product.Name == "Yoga Mat")
//           .ToList();

//            int deletedDetails = yogamatDetails.Count;
//            foreach (var detail in yogamatDetails)
//            {
//                context.OrderDetails.Remove(detail);
//            }
//            context.SaveChanges();
//            Console.WriteLine($"✓ Deleted {deletedDetails} order detail(s)\n");

//        Delete: Remove the product itself

//       Console.WriteLine("--- Deleting 'Yoga Mat' Product ---");
//            var yogaMatProduct = context.Products.FirstOrDefault(p => p.Name == "Yoga Mat");
//            if (yogaMatProduct != null)
//            {
//                context.Products.Remove(yogaMatProduct);
//                context.SaveChanges();
//                Console.WriteLine("✓ Yoga Mat product deleted\n");
//            }

//        Delete: Remove an order and its details

//       Console.WriteLine("--- Deleting Order #2 and Its Details ---");
//            var orderToDelete = context.Orders
//                .Include(o => o.OrderDetails)
//                .FirstOrDefault(o => o.Id == order2.Id);

//            if (orderToDelete != null)
//            {
//                foreach (var detail in orderToDelete.OrderDetails)
//                {
//                    context.OrderDetails.Remove(detail);
//                }
//                context.Orders.Remove(orderToDelete);
//                context.SaveChanges();
//                Console.WriteLine("✓ Order #2 and all its details deleted\n");
//            }

//            Final Summary

//           Console.WriteLine("\n=== FINAL DATA SUMMARY ===");
//            Console.WriteLine($"Total Categories: {context.Categories.Count()}");
//            Console.WriteLine($"Total Products: {context.Products.Count()}");
//            Console.WriteLine($"Total Customers: {context.Customers.Count()}");
//            Console.WriteLine($"Total Orders: {context.Orders.Count()}");
//            Console.WriteLine($"Total Order Details: {context.OrderDetails.Count()}");
//            #endregion
//        }
//    }
//}