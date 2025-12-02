using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Respsotry
{
    public interface ICatgryResostry
    {

       public List<Catgry> GetaAll();

        public Catgry Create(Catgry requset);
    }
}
