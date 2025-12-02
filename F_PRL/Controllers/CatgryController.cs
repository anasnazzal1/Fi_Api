using Azure;
using F_BLL.Service;
using F_DAL.DATA;
using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using F_DAL.Respsotry;
using F_PRL.Resorces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace F_PRL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatgryController : ControllerBase
    {
       
        IStringLocalizer _localizer;
        ICatgryService _catgryService;
       public  CatgryController(ICatgryService CatgryService, IStringLocalizer<SharedResources>localizer)
        {
            _catgryService = CatgryService;
              _localizer = localizer;
        }
        [HttpGet("")]
        public IActionResult index()
        {
            var catgry = _catgryService.GetaAll();


            return Ok(new {message = _localizer["success"].Value, catgry });
        }
        [HttpPost("")]
        public IActionResult Create(CatgryRequestDto requset)
        {

            _catgryService.Create(requset);

            return Ok(new { message = _localizer["success"].Value });

        }


    }
}
