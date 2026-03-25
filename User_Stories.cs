
//US-010
    using var context = new AppDbContext();
    var customer = new Customer
    {
        FullName = "Menna Reda",
        Email = "manna.reda@example.com",
        PhoneNumber = "01278324943",
        CustomerProfile = new CustomerProfile
        {
            CustomerId = 121,
            Address = "188 Mohandiseen",
            City = "Kafr El-Sheikh",
            PostalCode = "83841",
            NationalId = "22249035810575"
        }
    };
    context.Customers.Add(customer);
    context.SaveChanges();



//US-011
    var ViewCustomerProfile = context
        .Customers
        .Include(c => c.CustomerProfile)
        .Include(c => c.Orders).SingleOrDefault(i=>i.CustomerId==1);
    Console.WriteLine(ViewCustomerProfile);
    Console.WriteLine(ViewCustomerProfile.CustomerProfile);           
    
    if (ViewCustomerProfile.Orders.Any())
    {
        Console.WriteLine("Order History");
        foreach (var order in ViewCustomerProfile.Orders)
        {              
            Console.WriteLine($"OrderId: {order.OrderId} , PlacedAt: {order.PlacedAt}");
        }
    }
    else
        Console.WriteLine("No orders found for this customer");


//US-012 
var customer = context.Customers.SingleOrDefault(i => i.CustomerId == 2);
if (customer != null)
{
    context.Entry(customer).Reference(c => c.CustomerProfile).Load();
    if (customer.CustomerProfile == null)
    {
        customer.CustomerProfile = new CustomerProfile
        {
            CustomerId = 122,
            Address = "188 Mohandiseen",
            City = "Kafr El-Sheikh",
            PostalCode = "83841",
            NationalId = "22249035810575"
        };

    }
    else
    {
        customer.CustomerProfile.Address = "Updated Delivery Address";
        Console.WriteLine("Address updated");
    }
    context.SaveChanges();
}
else
{
    Console.WriteLine("This customer not found");
}




//US-020 
var ProductCatalog = context.Products
    .AsNoTracking()
    .Include(p => p.Category)
    .Select(p => new
    {
        Name = p.Name,
        Price = p.Price,
        CategoryName = p.Category.Name
    }).OrderBy(p => p.Price);
foreach (var product in ProductCatalog)
{
    Console.WriteLine($"Name: {product.Name} , Price: {product.Price} , CategoryName: {product.CategoryName}");
}
            

//US-021
var Searchproducts = context.Products
    .Where(p => p.Category.Name.Contains("Phones"));
if (Searchproducts.Any())
{
    foreach (var product in Searchproducts)
    {
        Console.WriteLine($"{product.Name}");
    }
}


//US-022
var productdetails = context.Products
    .Include(p => p.productTags)
    .ThenInclude(pt => pt.Tag)
    .Include(p => p.Reviews);
if (productdetails.Any())
{
    foreach (var product in productdetails)
    {
        Console.WriteLine("Full product details");
        Console.WriteLine(product);
        if (product.productTags.Any())
        {
            Console.WriteLine("product Tags");
            foreach (var tag in product.productTags)
            {
                Console.WriteLine(tag.Tag);

            }
        }

        if (product.Reviews.Any())
        {
            Console.WriteLine("customer reviews");
            foreach (var review in product.Reviews)
            {
                Console.WriteLine($"{review}");
            }
            var r = product.Reviews.Average(r => r.Rating);
            var c = product.Reviews.Count();
            Console.WriteLine($"Average Rating= {r} , Total review count= {c}");
        }
        Console.WriteLine("------------------------------------------");
    }
}



//US-023
var products = context.Products
    .Include(r => r.Reviews)
    .GroupBy(p => p.ProductId)
    .Select(p => new
    {
        ProductName = p.First().Name,
        AverageRating = p.SelectMany(p => p.Reviews)
        .Average(p => p.Rating)


    }).OrderByDescending(p => p.AverageRating)
    .Take(5)
    .ToList();

foreach (var p in products)
{
    Console.WriteLine($"Product Name: {p.ProductName}, Average Rating: {p.AverageRating}");
}


//US-024
await context.Products
    .Where(p => p.StockQuantity == 0)
    .ExecuteUpdateAsync(p => p.SetProperty(p => p.IsActive, false));
var count = context.Products
    .Count(p => p.StockQuantity == 0 && p.IsActive == false);

Console.WriteLine($"Out-of-stock inactive products: {count}");



//US-030
using var transaction = context.Database.BeginTransaction();
try
{
    var order = new Order
    {
        CustomerId = 121,
        Status = OrderStatus.Pending,
        ShippedAt = DateTime.Now,
        PlacedAt = DateTime.Now,
        OrderItems = new List<OrderItem> {
        new OrderItem
        {
            OrderId=151,
            ProductId= 56,
            Quantity= 5,
            UnitPrice= 1800
        },
        new OrderItem
        {
            OrderId=151,
            ProductId= 66,
            Quantity= 7,
            UnitPrice= 1600
        }
    },
        Payment = new Payment
        {
            OrderId = 151,
            Method = "Debit Card",
            Status = PaymentStatus.Pending,
            Amount = 19765.21m,
            PaidAt = null
        }


    }; 
    order.TotalAmount= order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);
    foreach (var item in order.OrderItems)
    {
        var StockQuantity = context.Products.FirstOrDefault(p=>p.ProductId == item.ProductId);
        if (StockQuantity != null)
        {
            if(StockQuantity.StockQuantity<item.Quantity)
            {
                Console.WriteLine($"Not enough stock for product {StockQuantity.Name}");
            }
            StockQuantity.StockQuantity -= (int)item.Quantity;
        }
    }
    context.Orders.Add(order);
    context.SaveChanges();

    transaction.Commit(); 
    Console.WriteLine("Order placed successfully");
}
catch (Exception ex)
{
    transaction.Rollback(); 
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.InnerException?.Message);

}




//US-031
var Id = 7;
var orderHistory = context.Orders
     .Where(o => o.CustomerId == Id)
     .Include(o => o.OrderItems)
     .Include(o => o.Payment)
     .OrderByDescending(o => o.PlacedAt);
Console.WriteLine($"Order History for Customer Id ={Id}");
foreach (var customerOrder in orderHistory)
{
    Console.WriteLine($"OrderId: {customerOrder.OrderId} , Order Status: {customerOrder.Status}");
}
Console.WriteLine();
var MostRecentOrder = orderHistory.FirstOrDefault();
Console.WriteLine($"Most Recent Order: {MostRecentOrder?.OrderId} , {MostRecentOrder?.Status}");



//US-032
using var transaction = context.Database.BeginTransaction();
try
{
    var order = context.Orders
        .Include(o => o.OrderItems) 
        .ThenInclude(o => o.Product)   
        .Include(o => o.Payment)             
        .SingleOrDefault(o => o.OrderId == 15);
    if (order?.Status == OrderStatus.Pending)
    {
        order.Status = OrderStatus.Cancelled;

        foreach (var item in order.OrderItems)
        {
            item.Product.StockQuantity += (int)item.Quantity;
        }
        order.Payment?.Status = PaymentStatus.Refunded;
        context.Orders.Update(order);
        context.SaveChanges();

        
        Console.WriteLine("Order Status changed successfully");
    }
    else
        Console.WriteLine("Status for this order not Pending");
    transaction.Commit();
}
catch (Exception ex)
{
    transaction.Rollback();
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.InnerException?.Message);

}



//US-033 
var TotalRevenue = context.Orders
    .Where(o => o.Status == OrderStatus.Delivered)
    .GroupBy(o => o.PlacedAt.Month)
    .Select(o => new
    {
        o.Key,
        TotalAmount = o.Sum(o => o.TotalAmount)
    }).OrderBy(g => g.Key);
foreach (var T in TotalRevenue)
{
    Console.WriteLine($"Month Number: {T.Key} , Total Amount: {T.TotalAmount}");
}





//US-034
var PendingOrders = context.Orders.FromSqlRaw("shop.GetPendingOrders");
//OR
var PendingOrders1 = context.Orders.FromSqlRaw(@"SELECT *
                                                FROM shop.Orders 
                                                WHERE Status = 'Pending'");

foreach (var p in PendingOrders)
{
    Console.WriteLine($"OrderId= {p.OrderId} , PlacedAt= {p.PlacedAt}");
}



//US-040
using var transaction = context.Database.BeginTransaction();
try
{
    var code = "SAVE10";
    var discountcode = context.Discounts.SingleOrDefault(d => d.Code == code);
    if (discountcode !=null&& discountcode.IsActive
        && (discountcode.ExpiresAt > DateTime.Now)
        && (discountcode.CurrentUses < discountcode.MaxUses))
    {
        var order = context.Orders.SingleOrDefault(o => o.OrderId == 7);
        if (order != null)
        {
            order.TotalAmount = (order.TotalAmount) - (discountcode.Percentage) * (order.TotalAmount) / 100;
            discountcode.CurrentUses++;
        }
           
        Console.WriteLine("Discount is applied successfully");
    }
    else
        Console.WriteLine("Invalid discount code");
    context.SaveChanges();     
    transaction.Commit();
}
catch (Exception ex)
{
    transaction.Rollback();
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.InnerException?.Message);
}


//US-041
var deletedCount = await context.Discounts
    .Where(d => d.ExpiresAt < DateTime.UtcNow || !d.IsActive)
    .ExecuteDeleteAsync();

Console.WriteLine($"Deleted: {deletedCount}");//Deleted: 30



//US-050
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);
    var configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .Build();
    var connectionString= configuration.GetSection("constr").Value;
    optionsBuilder.UseLazyLoadingProxies()
        .UseSqlServer(connectionString);
}

var Product=context.Products.SingleOrDefault(x => x.ProductId == 5);
Console.WriteLine(Product);
foreach (var Review in Product.Reviews)
{
    Console.WriteLine(Review);
}



//US-051
var CustomersData = context.Customers
        .Include(o => o.Orders)
        .ThenInclude(o => o.OrderItems)
        .ThenInclude(p => p.Product)
        .ThenInclude(r => r.Reviews)
        .AsSplitQuery().SingleOrDefault(c => c.CustomerId == 7);

Console.WriteLine($"Customer Data \n{CustomersData}\n");
Console.WriteLine("---------------Customer Order with their Items with their product----------------");
foreach (var Order in CustomersData.Orders)
{

    Console.WriteLine($"Order Id: {Order.OrderId} , Order Status: {Order.Status}");
    foreach (var item in Order.OrderItems)
    {
        Console.WriteLine($"OrderItem Id: {item.OrderItemId} , OrderItem Quantity: {item.Quantity}");
        Console.WriteLine($"Product Id: {item.Product.ProductId} , Product Name: {item.Product.DisplayName}");
        foreach (var Review in item.Product.Reviews)
        {
            Console.WriteLine(Review);
        }
        Console.WriteLine("-----------------------------------------------");
    }
}



//US-052
var CustomersWithZeroOrders = context.Customers
    .GroupJoin(context.Orders,
                customer => customer.CustomerId,
                order => order.CustomerId,
                (customer, orders) => new
                {
                    customer.FullName,
                    customer.Email,
                    Nullorders = !orders.Any()
                })
    .Where(c => c.Nullorders);

//OR

var CustomersWithZeroOrders1 = context.Customers
       .Where(c => !context.Orders.Any(o => o.CustomerId == c.CustomerId))
       .Select(c => new
       {
           c.FullName,
           c.Email
       });
foreach (var c in CustomersWithZeroOrders)
{
    Console.WriteLine($"{c.FullName} , {c.Email}");

}


//US-053
var Query=context.Products.Join(context.OrderItems,
                    product=>product.ProductId,
                    item=>item.ProductId,
                    (produt, items) => new
                    {
                        ProductId=produt.ProductId,
                        ProductName=produt.Name,
                        Quantity =items.Quantity
                     })
    .GroupBy(p => new {p.ProductId,p.ProductName})
    .Select(m => new
    {
        ProductName=m.Key.ProductName,
        TotalSold=m.Sum(p=>p.Quantity)
    })
    .OrderBy(p=>p.TotalSold)
    .ToList();
foreach (var item in Query)
{
    Console.WriteLine(item);
}
