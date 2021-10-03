using Microsoft.AspNet.Identity;
using Poshbots.Core.Entities;
using Poshbots.Core.Services;
using Poshbots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Security;
using System.Runtime.Caching;

namespace Poshbots.Controllers
{
    public class TrainingController : BaseController
    {
        public ActionResult Id(string id)
        {
            if(String.IsNullOrEmpty(id)) return new RedirectResult("/Battle");

            return View();
        }
    }
}