using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeineFix.Domain
{
    public class ProductImageDomainModel
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ImageExt { get; set; }
    }
}
