using System.Collections.Generic;

namespace GoogleFeed
{
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
