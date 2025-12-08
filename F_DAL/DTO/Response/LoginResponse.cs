using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.DTO.Response
{
    public class LoginResponse
    {
        public bool Sucsess { get; set; }
        public string Message { get; set; }

        public List<string> Errors { get; set; }
        public  string ? acssessToken   { get; set; }

    }
}
