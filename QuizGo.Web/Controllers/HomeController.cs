using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuizGo.Autentificacion.Entity;
using QuizGo.Web.Clases;

namespace QuizGo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Principal")]
        public ActionResult Principal()
        {
            return View();
        }

        #region Privada
        public class JsonFilter : ActionFilterAttribute
        {
            public string Param { get; set; }
            public Type JsonDataType { get; set; }
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
                {
                    string inputContent;
                    using (var sr = new StreamReader(filterContext.HttpContext.Request.InputStream))
                    {
                        inputContent = sr.ReadToEnd();
                    }
                    var result = JsonConvert.DeserializeObject(inputContent, JsonDataType);
                    filterContext.ActionParameters[Param] = result;
                   
                }
                base.OnActionExecuting(filterContext);
            }
        }
        #endregion

        [HttpPost]
        [QuizGo.Web.Controllers.HomeController.JsonFilter(Param= "autenticar", JsonDataType = typeof(BeAutentificacion))]
        public JsonResult Login(BeAutentificacion autenticar)
        {
                FiltroAutentificar filtroAutentificar = new FiltroAutentificar();
                filtroAutentificar.Usuario = autenticar.Usuario;
                filtroAutentificar.Password = autenticar.Password;

                var listaAutenticar = new List<BeAutentificacion>();

                listaAutenticar = (List<BeAutentificacion>)Util.MakeRequest(autenticar.GetType(),filtroAutentificar);

                return Json(listaAutenticar);
            
        }
    }
}