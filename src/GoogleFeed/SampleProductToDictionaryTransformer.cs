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
                    { "g:id", product.Id.ToString() },
                    { "title", product.Name },
                    { "link", product.Link },
                    { "g:price", product.Price },
                    { "description", product.Description },                    
                    { "g:condition", product.Condition }
                };
            }
        }
    }
}
