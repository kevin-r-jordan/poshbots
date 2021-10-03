using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poshbots.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected IUnityContainer Container { get { return UnityConfig.GetConfiguredContainer(); } }
    }
}
