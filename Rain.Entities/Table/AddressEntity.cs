using Rain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Entities
{
    [Table("Person.Address")]
    public class AddressEntity
    {
        [NotCopy]
        [Display(Name = "地址ID")]
        public int AddressID { get; set; }
        //public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        //public string City { get; set; }
        //public int StateProvinceID { get; set; }
        //public string PostalCode { get; set; }
        //public byte[] SpatialLocation { get; set; }
        //public Guid rowguid { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}
