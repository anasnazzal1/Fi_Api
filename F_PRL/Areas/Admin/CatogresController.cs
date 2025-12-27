using Azure;
using F_BLL.Service;
using F_DAL.DATA;
using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using F_DAL.Respsotry;
using F_PRL.Resorces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace F_PRL.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class CatogresController : ControllerBase
    {

        IStringLocalizer _localizer;
        ICatgryService _catgryService;
        public CatogresController(ICatgryService CatgryService, IStringLocalizer<SharedResources> localizer)
        {
            _catgryService = CatgryService;
            _localizer = localizer;
        }
      
        [HttpPost("")]
        public IActionResult Create(CatgryRequestDto requset)
        {
          
            _catgryService.Create(requset);

            return Ok(new { message = _localizer["success"].Value });

        }


    }
}
