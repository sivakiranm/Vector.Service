using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vector.API.Handlers;
using Vector.Common.DataLayer;
using Vector.Common.Entities;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.UserManagement.ClientInfo;

namespace Vector.API.Controllers
{
    [RoutePrefix("api/garage")]
    [VectorActionAutorizationFilter]
    public class GarageController : ApiController
    {
    }
}
