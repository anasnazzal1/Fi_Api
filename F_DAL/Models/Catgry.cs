
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Models
{
    public class Catgry
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CatgryTranslation>translations { get; set; }
        
    }
}
