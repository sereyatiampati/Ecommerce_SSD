
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.Utils;

namespace Ecommerce.Controllers
{
    public class ProductsController
    {
        ProductService productService = new ProductService();
        public async static Task Init()
        {
           
            Console.WriteLine("Happy to have you using this E-commerce console app!!");
            Console.WriteLine(new string('*', 20));
            Console.WriteLine("1. Add a Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Update a Product");
            Console.WriteLine("4. Delete a Product");

            var input = Console.ReadLine();
            var results = Validation.Validate(new List<string> { input });

            if (!results) 
            {
                await Init();
            }
            else
            {
                await new ProductsController().CallMenu(input);
            }
        }
        public async Task CallMenu(string id)
        {
            switch (id)
            {
                case "1":
                    await AddNewProduct();
                    break;
                case "2":
                    await ViewProducts();
                    break;
                case "3":
                    await UpdateProduct();
                    break;
                case "4":
                    await DeleteProduct();
                    break;
                default:
                    ProductsController.Init();
                    break;
            }
        }

        // call my categories from an enum file
        ProductsCategory category = new ProductsCategory();

        public async Task AddNewProduct()
        {
            Console.Write("Enter product name: ");
            var productName = Console.ReadLine();
            if (productName == "")
            {
                
                Console.WriteLine("Product name cannot be empty");
                await AddNewProduct();
            }

            Console.Write("Enter product description: ");
            var productDescription = Console.ReadLine();
            if (productDescription == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product description cannot be empty");
                Console.ResetColor();
                await AddNewProduct();
            }

            Console.Write("Enter product price: ");
            var productPrice = Console.ReadLine();
            // check if the price is a number
            if (!int.TryParse(productPrice, out var price))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product price must be a number");
                Console.ResetColor();
                await AddNewProduct();
            }
            if (string.IsNullOrWhiteSpace(productPrice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product price cannot be empty");
                Console.ResetColor();
                await AddNewProduct();
            }

            Console.WriteLine("Select a category: ");
            foreach (var item in Enum.GetValues(typeof(ProductsCategory)))
            {
                Console.WriteLine($"{(int)item}. {item}");
            }

            if (!int.TryParse(Console.ReadLine(), out var selectedCategory) ||
        !Enum.IsDefined(typeof(ProductsCategory), selectedCategory))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid category selection");
                Console.ResetColor();
                await AddNewProduct();
            }

            var newProduct = new AddProducts()
            {
                ProductName = productName,
                ProductDescription = productDescription,
                ProductPrice = productPrice,
                ProductCategory = ((ProductsCategory)selectedCategory).ToString()
            };

            try
            {
                var res = await productService.CreateProductAsync(newProduct);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(res.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // View a product logic implementation
        public async Task ViewProducts()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}");
                    Console.ResetColor();
                }
                Console.Write("Chose to view one product: ");

                var id = Console.ReadLine();

                await ViewOneProduct(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // View one product logic implementation
        public async Task ViewOneProduct(string id)
        {
            try
            {
                var product = await productService.GetProductAsync(id);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}, {product.ProductCategory}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.Write("Please enter a valid product id: ");
                Console.ResetColor();

                var input = Console.ReadLine();
                var results = Validation.Validate(new List<string> { input });
                await ViewOneProduct(input);
            }
        }

        // Update a product logic implementation
        public async Task UpdateProduct()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice},  {product.ProductCategory}"); //{product.ProductRating},
                    Console.ResetColor();
                }
                Console.Write("Enter product id: ");
                var id = Console.ReadLine();


                var current_product = products.FirstOrDefault(x => x.Id == id);

                Console.Write($"Enter product name: {current_product.ProductName}: New name: ");
                var productName = Console.ReadLine();
                if (productName == "")
                {
                    productName = current_product.ProductName;
                }

                Console.Write($"Enter product description: {current_product.ProductDescription}: New description: ");
                var productDescription = Console.ReadLine();
                if (productDescription == "")
                {
                    productDescription = current_product.ProductDescription;
                }

                Console.Write($"Enter product price: {current_product.ProductPrice}: New price: ");
                var productPrice = Console.ReadLine();
                if (productPrice == "")
                {
                    productPrice = current_product.ProductPrice;
                }

                Console.Write($"Enter product category: {current_product.ProductCategory}: New category: ");
                var productCategory = Console.ReadLine();
                if (productCategory == "")
                {
                    productCategory = current_product.ProductCategory;
                }

                var newProduct = new Products()
                {
                    Id = id,
                    ProductName = productName,
                    ProductDescription = productDescription,
                    ProductPrice = productPrice,
                    ProductCategory = productCategory
                };

                try
                {
                    var res = await productService.UpdateProductAsync(newProduct);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(res.Message);
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Delete a product logic implementation
        public async Task DeleteProduct()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await Console.Out.WriteLineAsync($"{product.Id}. {product.ProductName}, {product.ProductDescription} @ {product.ProductPrice}");
                    Console.ResetColor();
                }
                Console.Write("Enter product id to delete: ");
                var id = Console.ReadLine();

                try
                {
                    var res = await productService.DeleteProductAsync(id);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(res.Message);
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}