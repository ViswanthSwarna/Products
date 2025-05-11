using Products.Domain.Entities;
using Products.Infrastructure.Data;
using Products.Repositories.Classes;
using Products.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Repository.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ZeissAppDbContext context) : base(context)
        {
        }
    }
}
