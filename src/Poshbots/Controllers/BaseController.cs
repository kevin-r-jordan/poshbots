using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poshbots.Controllers
{
    public class BaseController : Controller
    {
        protected IUnityContainer Container { get { return UnityConfig.GetConfiguredContainer();  } }
    }
}