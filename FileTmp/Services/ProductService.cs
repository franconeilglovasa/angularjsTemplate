using System;
using System.Text;
using FileTmp.Data;
using FileTmp.Models;

namespace FileTmp.Services
{
	public class ProductService
	{
        private readonly DataContext _context;
        private static Random random = new Random();

        const int maxSeedValue = 100;


        public ProductService(DataContext context)
		{
            _context = context;
        }

        public bool SeedProducts () {

            var products = new List<Product>();

            for (var ctr =0; ctr<maxSeedValue; ctr++) {
                var product = new Product
                {
                    Name = GenerateRandomString(5)
                };
                products.Add(product);
            }

             _context.Products.AddRange(products);
             _context.SaveChanges();
            return true;
        }

        public bool AddProduct(Product product) {
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }

        public List<Product> GetProductsByFirstLetter(string letter) {
            var products = _context.Products.Where(x => x.Name.StartsWith(letter))
                .ToList();
            return products;

        }

        public static string[] GenerateRandomStrings(int count)
        {
            HashSet<string> generatedStrings = new HashSet<string>();

            while (generatedStrings.Count < count)
            {
                int length = random.Next(6, 11); // Generates a random length between 6 and 10
                string randomString = GenerateRandomString(length);

                if (!generatedStrings.Contains(randomString))
                {
                    generatedStrings.Add(randomString);
                }
            }

            return generatedStrings.ToArray();
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
    }
}

