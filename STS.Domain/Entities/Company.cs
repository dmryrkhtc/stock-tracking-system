using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get;set; }
        public string TaxNo { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        
        public ICollection<User> Users { get; set; }//birden cok kullanicisi olabilir
        public ICollection<Product> Products { get; set; }//birden cok urunu olabilir
    }
}
