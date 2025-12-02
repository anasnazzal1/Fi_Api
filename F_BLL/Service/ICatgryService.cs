using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{
    public interface ICatgryService
    {
        public List<CatgryResponseDto> GetaAll();

        public Catgry Create(CatgryRequestDto requset);
    }
}
