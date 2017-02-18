using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartaGram.Models
{
    public class AuthenticationModel
    {
        public Guid UserId { get; set; }
        public String userName { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String password { get; set; }

    }
}
