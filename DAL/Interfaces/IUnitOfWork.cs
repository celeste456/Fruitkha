using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductDAL ProductDAL { get; }
        ICategoryDAL CategoryDAL { get; }
        bool Complete();
    }
}
