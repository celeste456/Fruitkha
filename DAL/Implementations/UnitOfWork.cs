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

        public UnitOfWork(FruitkhaContext Context,
                         IProductDAL productDAL

            )
        {
            this._Context = Context;
            this.ProductDAL = productDAL;
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
