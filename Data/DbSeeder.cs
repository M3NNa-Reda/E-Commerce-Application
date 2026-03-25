using E_Commerce_Application.Data;
using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce_Application.Seeding
{
    public class DbSeeder
    {
        private readonly AppDbContext _context;

        public DbSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (!_context.Categories.Any())
                {
                    var categories = LoadJsonData<Category>("categories.json");
                    if (categories != null) _context.Categories.AddRange(categories);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Customers.Any())
                {
                    var customers = LoadJsonData<Customer>("customers.json");
                    if (customers != null) _context.Customers.AddRange(customers);
                    await _context.SaveChangesAsync();
                }

                if (!_context.CustomerProfiles.Any())
                {
                    var customerProfiles = LoadJsonData<CustomerProfile>("customerProfiles.json");
                    if (customerProfiles != null) _context.CustomerProfiles.AddRange(customerProfiles);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Products.Any())
                {
                    var products = LoadJsonData<Product>("products.json");
                    if (products != null) _context.Products.AddRange(products);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Tags.Any())
                {
                    var tags = LoadJsonData<Tag>("tags.json");
                    if (tags != null) _context.Tags.AddRange(tags);
                    await _context.SaveChangesAsync();
                }

                if (!_context.ProductTags.Any())
                {
                    var productTags = LoadJsonData<ProductTag>("productTags.json");
                    if (productTags != null) _context.ProductTags.AddRange(productTags);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Orders.Any())
                {
                    var orders = LoadJsonData<Order>("orders.json");
                    if (orders != null) _context.Orders.AddRange(orders);
                    await _context.SaveChangesAsync();
                }

                if (!_context.OrderItems.Any())
                {
                    var orderItems = LoadJsonData<OrderItem>("orderItems.json");
                    if (orderItems != null) _context.OrderItems.AddRange(orderItems);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Payments.Any())
                {
                    var payments = LoadJsonData<Payment>("payments.json");
                    if (payments != null) _context.Payments.AddRange(payments);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Reviews.Any())
                {
                    var reviews = LoadJsonData<Review>("reviews.json");
                    if (reviews != null) _context.Reviews.AddRange(reviews);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Discounts.Any())
                {
                    var discounts = LoadJsonData<Discount>("discounts.json");
                    if (discounts != null) _context.Discounts.AddRange(discounts);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                await transaction.RollbackAsync();
            }
        }

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private static List<T> LoadJsonData<T>(string fileName)
        {
            var path = Path.Combine("JsonData", fileName);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
        }
    }
}