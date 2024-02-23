using Mataeem.Models;

namespace Mataeem.Lib
{
    public static class ProductExtensions
    {
        public static List<Guid> GetAllChildIds(this Product product)
        {
            var childIds = new List<Guid>();
            
            if (product.Products != null && product.Products.Any())
            {
                childIds.AddRange(product.Products.Select(p => p.Id));

                foreach (var childProduct in product.Products)
                {
                    childIds.AddRange(childProduct.GetAllChildIds());
                }
            }

            return childIds;
        }
    }
}
