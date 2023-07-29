using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        //SaveChange() - DbContext
        public void Commit()
        {
            _context.SaveChanges();
        }

        //SaveChangeAsync() - DbContext
        public async Task CommitAsync()
        {
            _context.SaveChangesAsync();
        }
       
    }
}
