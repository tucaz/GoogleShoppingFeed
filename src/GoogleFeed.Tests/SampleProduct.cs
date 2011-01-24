using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleFeed.Tests
{
    public class SampleProduct
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class SampleProductsGenerator
    {
        public static IEnumerable<SampleProduct> Generate(int howMany)
        {
            var rnd = new Random(howMany);
            
            for(int i = 1; i <= howMany; i++)
            {
                yield return new SampleProduct()
                {
                    Id = i,
                    Description = "This is a very nice product with lots of features. You may not need all of them, but they come for free, so why not?",
                    Name = String.Format("Product Name {0}", i),
                    Price = Convert.ToDecimal(i * rnd.Next())                    
                };
            }
        }
    }

    public class SampleProductToDictionaryTransformer
    {
        public IEnumerable<Dictionary<string, string>> Transform(IEnumerable<SampleProduct> products)
        {
            foreach (var product in products)
	        {
                yield return new Dictionary<string, string>
                {
                    { "Id", product.Id.ToString() },
                    { "Name", product.Name },
                    { "Description", product.Description },
                    { "Price", product.Price.ToString("N2") }
                };
	        }
        }
    }
}
