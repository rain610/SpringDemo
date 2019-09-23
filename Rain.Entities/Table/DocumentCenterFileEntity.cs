using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Entities
{
    [Table("DocCenter_File")]
    public class DocumentCenterFileEntity
    {
        public string FileCode { get; set; }
        public string FileName { get; set; }
    }
}
