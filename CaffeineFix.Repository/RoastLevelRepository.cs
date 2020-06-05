using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Repository.Infrastructure;
using CaffeineFix.Repository.Infrastructure.Contract;

namespace CaffeineFix.Repository
{
    public class RoastLevelRepository : BaseRepository<RoastLevel>
    {
        public RoastLevelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
