using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using System.Runtime.Caching;

namespace MyShop.DataAccess.InMemory
{
    public class ProudctRepository
    {

        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        public ProudctRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }

        }

        public void CommitToCache()
        {
            //TO ADD PRODUCTS TO OUR CACHE MEMORY
            cache["products"] = products;
        }


        public void InsertProduct(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            var prodToUpdate = products.Find(o => o.Id == p.Id);     //FirstOrDefault();
            if (prodToUpdate != null)
            {
                prodToUpdate = p;
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }

        public Product FindProd(string Id)
        {
            var prod = products.Find(o => o.Id == Id);     //FirstOrDefault();
            if (prod != null)
            {
                return prod;
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }

        public IEnumerable<Product> ListOfProducts()
        {
            return products.ToList();
        }

        //public IEnumerable<Product> ListOfProducts(IEnumerable<Product> prod)
        //{
        //    return prod.ToList();
        //}



        public void Delete(string Id)
        {
            var prod = products.Find(o => o.Id == Id);
            if (prod != null)
            {
                products.Remove(prod);
            }
            else
            {
                throw new Exception("No product found!");
            }
        }
    }
}
