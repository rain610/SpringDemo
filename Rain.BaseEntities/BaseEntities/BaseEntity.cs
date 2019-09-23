using Rain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.BaseEntities
{
    [Serializable]
    public class BaseEntity
    {
        [Display(Name = "编号")]
        [NotCopy]
        public int ID { get; set; }
    }
}
