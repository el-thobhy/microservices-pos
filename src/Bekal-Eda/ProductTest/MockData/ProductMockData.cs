using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTest.MockData
{
    public class ProductMockData
    {
        public static List<ProductEntity> GetProduct()
        {
            return new List<ProductEntity>
            {
                new ProductEntity()
                {
                    Id = new Guid("CBB7C8F1-BC5B-47A9-8957-BF08E013447F"),
                    Name = "Tas",
                    Price = 100000
                },
                new ProductEntity()
                {
                    Id = new Guid("5CA86A79-56FD-4D7D-ADD4-65E57C185E0E"),
                    Name = "Tas",
                    Description = "Tas Backpack",
                    Price = 100000
                },
            };
        }
    }
}
