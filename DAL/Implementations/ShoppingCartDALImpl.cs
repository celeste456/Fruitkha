using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
	public class ShoppingCartDALImpl : DALGenericoImpl<ShoppingCart>, IShoppingCartDAL
	{
		FruitkhaContext context;

		public ShoppingCartDALImpl(FruitkhaContext context) : base(context)
		{
			this.context = context;
		}
	}
}
