using Ecommerce.Controllers;

// ProductsController.Init();

class Program
{
    public async static Task Main(string[] args)
    {
        await ProductsController.Init();
    }
}