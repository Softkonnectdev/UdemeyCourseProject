using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productCates;

        public ProductCategoryRepository()
        {
            productCates = cache["productCates"] as List<ProductCategory>;
            if (productCates == null)
            {
                productCates = new List<ProductCategory>();
            }
        }


        public void CommitToCache()
        {
            cache["productCates"] = productCates;
        }


        public void InsertProduct(ProductCategory pc)
        {
            productCates.Add(pc);
        }

        public void Update(ProductCategory pc)
        {
            var prodCateToUpdate = productCates.Find(o => o.Id == pc.Id);     //FirstOrDefault();
            if (prodCateToUpdate != null)
            {
                prodCateToUpdate = pc;
            }
            else
            {
                throw new Exception("Product category does not exist!");
            }
        }

        public ProductCategory FindProd(string Id)
        {
            var prod = productCates.Find(o => o.Id == Id);     //FirstOrDefault();
            if (prod != null)
            {
                return prod;
            }
            else
            {
                throw new Exception("Product category does not exist!");
            }
        }

        public IEnumerable<ProductCategory> ListOfProductCates()
        {
            return productCates.ToList();
        }

        //public IEnumerable<Product> ListOfProducts(IEnumerable<Product> prod)
        //{
        //    return prod.ToList();
        //}



        public void Delete(string Id)
        {
            var prod = productCates.Find(o => o.Id == Id);
            if (prod != null)
            {
                productCates.Remove(prod);
            }
            else
            {
                throw new Exception("No product Category found!");
            }
        }


    }
}
