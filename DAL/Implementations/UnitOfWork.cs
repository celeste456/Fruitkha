using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private FruitkhaContext _Context;

        public IProductDAL ProductDAL { get; set; }
        public ICategoryDAL CategoryDAL { get; set; }
        public IDiscountCodeDAL DiscountCodeDAL { get; set; }
        public IShoppingCartDAL ShoppingCartDAL { get; set; }
		public IShoppingCartItemDAL ShoppingCartItemDAL { get; set; }
		public IOrderDAL OrderDAL { get; set; }

		public UnitOfWork(FruitkhaContext Context,
                         IProductDAL productDAL, ICategoryDAL categoryDAL, IDiscountCodeDAL discountCode,
						 IShoppingCartDAL shoppingCartDAL,IOrderDAL orderDAL, IShoppingCartItemDAL shoppingCartItemDAL
			)
        {
            this._Context = Context;
            this.ProductDAL = productDAL;
            this.CategoryDAL = categoryDAL;
            this.DiscountCodeDAL = discountCode;
            this.ShoppingCartDAL = shoppingCartDAL;
            this.ShoppingCartItemDAL = shoppingCartItemDAL;
            this.OrderDAL = orderDAL;
        }

        public bool Complete()
        {
            try
            {
                _Context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return false;
            }
        }

        public void Dispose()
        {
            this._Context.Dispose();
        }
    }
}
