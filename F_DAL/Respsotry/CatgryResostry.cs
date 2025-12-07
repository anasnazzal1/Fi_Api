using F_DAL.DATA;
using F_DAL.DTO.Request;
using F_DAL.DTO.Response;
using F_DAL.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.Respsotry
{
    public class CatgryResostry : ICatgryResostry

    {
        ApplecationDbContext _context;
        public CatgryResostry(ApplecationDbContext context)
        {
            _context = context;
        }

        public List<Catgry> GetaAll()
        {
           
            var catgry = _context.Catgries.Include(c => c.translations).ToList();

            return catgry;

        }

       public  Catgry Create(Catgry request) {

            _context.Catgries.Add(request);
            _context.SaveChanges();
            return request;

        }
    }
}
