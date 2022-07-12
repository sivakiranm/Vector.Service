using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vector.API.Handlers
{
    public class VectorAPIContext
    {
        public static BasicAuthenticationIdentity Current
        {
            get
            {
                return HttpContext.Current?.User.Identity as BasicAuthenticationIdentity;
            }
        }
    }
}