using E_Commerce_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website
{
    public class Program
    {

        // Service Functions

        // 01 Register a New User =======

        public static void RegisterUser(ECommerceContext context)
        {
            Console.WriteLine("\n=== Register New User ===");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            string passwordHash = password;

            Console.WriteLine("Enter full name: ");
            string fullName = Console.ReadLine();

            Console.WriteLine("Enter phone number (optional, press Enter to skip): ");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter address (optional, press Enter to skip): ");
            string address = Console.ReadLine();

            User user = new User
            {
                username = username,
                email = email,
                passwordHash = passwordHash,
                fullName = fullName,
                phoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone,
                address = string.IsNullOrWhiteSpace(address) ? null : address,
                registrationDate = DateTime.Now,
                isActive = true
            };

            context.Users.Add(user);
            context.SaveChanges();

            Console.WriteLine($"User ID = {user.userId}");
        }


        // 02 Add a New Product to a Category======== 
        public static void AddProduct(ECommerceContext context)
            {

                Console.WriteLine("\n=== Add New Product ===");

         
                Console.WriteLine("\nEnter Category ID: ");
                int categoryId = int.Parse(Console.ReadLine());

                Console.WriteLine("Product Name: ");
                string productName = Console.ReadLine();

                Console.WriteLine("Description (optional): ");
                string description = Console.ReadLine();

                Console.WriteLine("Price: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Stock Quantity: ");
                int stockQuantity = int.Parse(Console.ReadLine());

                Console.WriteLine("Image URL (optional): ");
                string imageUrl = Console.ReadLine();


                var categories = context.Categories.ToList();

                if (categories.Count == 0)
                {
                    Console.WriteLine("No categories found. Please add a category first.");
                    return;
                }

                Console.WriteLine("\nAvailable Categories:");
                foreach (var c in categories)
                {
                    Console.WriteLine($" ID:{c.categoryId} | Name: {c.categoryName}");
                }

                Product product = new Product
                {
                    productName = productName,
                    Description = string.IsNullOrWhiteSpace(description) ? null : description,
                    Price = price,
                    StockQuantity = stockQuantity,
                    ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl,
                    CreatedAt = DateTime.Now,
                    IsAvailable = true,
                    CategoryId = categoryId
                };

                context.Products.Add(product);
                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine("Product Added Successfully.");
                Console.WriteLine($"Product ID = {product.ProductId}");
            }


        // 03 Place an Order ===========
        public static void PlaceOrder(ECommerceContext context)

            {

            Console.WriteLine("\n========== Place Order ==========");

            // Display Users
            Console.WriteLine("\nAvailable Users:");
            foreach (var u in context.Users)
            {
                Console.WriteLine($" ID:{u.userId} | Name:{u.fullName}");
            }

            Console.WriteLine("\nEnter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Shipping Address: ");
            string shippingAddress = Console.ReadLine();

            Console.WriteLine("Enter Payment Method: ");
            string paymentMethod = Console.ReadLine();


            // Create Order
            Order order = new Order
            {
                userId = userId,
                orderDate = DateTime.Now,
                totalAmount = 0,
                status = "Pending",
                shippingAddress = shippingAddress,
                paymentMethod = paymentMethod
            };

            // Save Order first to generate orderId
            context.Orders.Add(order);
            context.SaveChanges();

            decimal total = 0;
            bool addMore = true;

            while (addMore)
            {
                Console.WriteLine("\nAvailable Products:");

                foreach (var p in context.Products)
                {
                    Console.WriteLine($" ID:{p.ProductId} | Name: {p.productName} | Price: {p.Price} | Stock: {p.StockQuantity}");
                }

                Console.WriteLine("\nEnter Product ID: ");
                int productId = int.Parse(Console.ReadLine());

                Product productData = context.Products.FirstOrDefault(p => p.ProductId == productId);

                if (productData == null)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }

                Console.WriteLine("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                if (quantity > productData.StockQuantity)
                {
                    Console.WriteLine("Not enough stock.");
                    continue;
                }

                // Create OrderItem
                OrderItem item = new OrderItem
                {
                    orderId = order.orderId,
                    productId = productId,
                    quantity = quantity,
                    unitPrice = productData.Price
                };

                context.OrderItems.Add(item);

                // Calculate Total
                total += item.unitPrice * quantity;

                // Reduce Stock
                productData.StockQuantity -= quantity;

                Console.WriteLine("\nAdd another product? (Y/N): ");
                string answer = Console.ReadLine();

                if (answer.ToUpper() != "Y")
                {
                    addMore = false;
                }
            }

            // Update Order Total
            order.totalAmount = total;

            context.SaveChanges();

            Console.WriteLine("\n=================================");
            Console.WriteLine("Order Placed Successfully.");
            Console.WriteLine($"Order ID : {order.orderId}");
            Console.WriteLine($"Total Amount : {order.totalAmount}");
            Console.WriteLine("=================================");

        }


        //  04 Write a Product Review ============ 

        public static void WriteReview(ECommerceContext context)

              {

            Console.WriteLine("\n========== Write Product Review ==========");

            // Display Users
            Console.WriteLine("\nAvailable Users:");
            foreach (var u in context.Users)
            {
                Console.WriteLine($"ID:{u.userId} | Name:{u.fullName}");
            }

            Console.Write("\nEnter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            // Display Products
            Console.WriteLine("\nAvailable Products:");
            foreach (var p in context.Products)
            {
               Console.WriteLine($"ID:{p.ProductId} | Name: {p.productName}");
            }

            Console.Write("\nEnter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Rating (1-5): ");
            int rating = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Comment (Optional): ");
            string? comment = Console.ReadLine();

            Review review = new Review
            {
                productId = productId,
                rating = rating,
                comment = string.IsNullOrWhiteSpace(comment) ? null : comment,
                reviewDate = DateTime.Now
            };

            context.Reviews.Add(review);
            context.SaveChanges();

            Console.WriteLine("\nReview Added Successfully.");
            Console.WriteLine($"Review ID: {review.reviewId}");
            }


        //  05 Update Product Price and Availability==============

           public static void UpdateProduct(ECommerceContext context)

             {

            Console.WriteLine("\n========== Update Product ==========");

            // Display Products
            foreach (var p in context.Products)
            {
                Console.WriteLine($" ID:{p.ProductId} | Name:{p.productName} | Price: {p.Price} | Available: {p.IsAvailable}");
            }

            Console.Write("\nEnter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            // Fetch Product
            Product product = context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter New Price: ");
            product.Price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Is Product Available? (true / false): ");
            product.IsAvailable = Convert.ToBoolean(Console.ReadLine());

            // Save Changes
            context.SaveChanges();

            Console.WriteLine("\nProduct Updated Successfully.");

            }


             // 06 Cancel an Order=============
        public static void CancelOrder(ECommerceContext context)

            {

            Console.WriteLine("\n========== Cancel Order ==========");

            // Display Orders
            foreach (var O in context.Orders)
            {
                Console.WriteLine($"Order ID: {O.orderId} | User ID: {O.userId} | Status: {O.status}");
            }

            Console.Write("\nEnter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            // Find Order
            Order order = context.Orders.FirstOrDefault(o => o.orderId == orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (order.status == "Cancelled")
            {
                Console.WriteLine("This order is already cancelled.");
                return;
            }

            // Load Order Items
            var orderItems = context.OrderItems
                                    .Where(i => i.orderId == orderId)
                                    .ToList();

            // Restore Stock
            foreach (var item in orderItems)
            {
                Product product = context.Products.FirstOrDefault(p => p.ProductId == item.productId);

                if (product != null)
                {
                    product.StockQuantity += item.quantity;
                }
            }

            // Update Order Status
            order.status = "Cancelled";

            // Save Changes
            context.SaveChanges();

            Console.WriteLine("\nOrder Cancelled Successfully.");

            }

        // 07 Delete a Review===================== 

        public static void DeleteReview(ECommerceContext context)

        {

        }


             // 08 View All Products (Get All) 
             public static void ViewAllProducts(ECommerceContext context)

               {


               }

                  // 09 Filter Products by Category and Price Range ================

             public static void FilterProducts(ECommerceContext context)

               {

               }
               //10 Get Category with All Its Products (Include) ===================
             public static void GetCategoryWithProducts(ECommerceContext context)

              {

              }


                // 11 View Order History with Full Details
        
               public static void ViewOrderHistory(ECommerceContext context)

                {

                }
                 // 12 Product Summary Report (Projection + Lazy
               public static void ProductSummaryReport(ECommerceContext context)

                 {

                 }


               public static void Main(string[] args)

                 {

                using ECommerceContext context = new ECommerceContext();

                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("\n========================================");
                    Console.WriteLine("        E-Commerce System");
                    Console.WriteLine("========================================");
                    Console.WriteLine(" 1  - Register User");
                    Console.WriteLine(" 2  - Add Product");
                    Console.WriteLine(" 3  - Plac eOrder");
                    Console.WriteLine(" 4  - Write Review");
                    Console.WriteLine(" 5  - Update Product");
                    Console.WriteLine(" 6  - CancelOrder");
                    Console.WriteLine(" 7  - DeleteReview");
                    Console.WriteLine(" 8  - ViewAllProducts");
                    Console.WriteLine(" 9  - FilterProducts");
                    Console.WriteLine(" 10 - GetCategoryWithProducts");
                    Console.WriteLine(" 11 - ViewOrderHistory");
                    Console.WriteLine("12 -ProductSummaryReport");
                    Console.WriteLine(" 0  - Exit");
                    Console.WriteLine("========================================");
                    Console.Write("Select option: ");

                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {

                        case 1: RegisterUser(context); break;
                        case 2: AddProduct(context); break;
                        case 3: PlaceOrder(context); break;
                        case 4: WriteReview(context); break;
                        case 5: UpdateProduct(context); break;
                        case 6: CancelOrder(context); break;
                        case 7: DeleteReview(context); break;
                        case 8: ViewAllProducts(context); break;
                        case 9: FilterProducts(context); break;
                        case 10: GetCategoryWithProducts(context); break;
                        case 11: ViewOrderHistory(context); break;
                        case 12: ProductSummaryReport(context); break;
                        case 0: exit = true; break;
                        default: Console.WriteLine("Invalid option. Please try again."); break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                Console.WriteLine("Goodbye!");
             }
          }
        }



          



