using Rain.BaseRepository;
using Rain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Repository
{
    public class DocumentCenterFileRepository
    {
        public List<DocumentCenterFileEntity> List()
        {
            var context = RainDbContext.GetContext();
            var test = (from t in context.Set<DocumentCenterFileEntity>()
                        select t).ToList();
            return test;
        }
    }
}
