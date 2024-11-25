using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnitOfWork
    {
        private FruitkhaContext _Context;
        public ICategoryDAL CategoryDAL { get; set; }

        public UnitOfWork(FruitkhaContext Context,
                        ICategoryDAL categoryDAL

            )
        {
            this._Context = Context;
            this.CategoryDAL = categoryDAL;
        }

        public bool Complete()
        {
            try
            {
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            this._Context.Dispose();
        }
    }
}
