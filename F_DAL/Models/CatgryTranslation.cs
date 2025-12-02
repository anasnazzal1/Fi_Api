using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Models
{
    public class CatgryTranslation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Language { get; set; }
        public int CatgryId { get; set; }
        public Catgry catgry { get; set; }

    }
}
