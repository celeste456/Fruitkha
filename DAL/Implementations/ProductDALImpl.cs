using Entities.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ProductDALImpl : DALGenericoImpl<Product>, IProductDAL
    {
        FruitkhaContext context;

        public ProductDALImpl (FruitkhaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
