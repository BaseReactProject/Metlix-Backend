using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
namespace Domain.Entities
{
    public class FakeEntity:Entity<int>
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }
    }

}
