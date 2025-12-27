using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_DAL.DTO.Request
{
    public class ResetPasswordRequest
    {

        public string Password { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }

    }
}
