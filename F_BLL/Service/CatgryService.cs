using F_DAL.DATA;
using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using F_DAL.Respsotry;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_BLL.Service
{
    public class CatgryService : ICatgryService
    {
        ICatgryResostry _catgryResostry;
        public CatgryService(ICatgryResostry catgryResostry)
        {
            _catgryResostry = catgryResostry;
        }

        public Catgry Create(CatgryRequestDto requset)
        {
            var catgrys = requset.Adapt<Catgry>();
            _catgryResostry.Create(catgrys);
            return catgrys;
        }

        public List<CatgryResponseDto> GetaAll()
        {
            var catgry = _catgryResostry.GetaAll();
            var response = catgry.Adapt<List<CatgryResponseDto>>();

            return response;
        }
    }
}
