using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        List<String> lista;
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]

        public ActionResult Login(Usuarios ouser)
        {
            if (ouser.user != " " && ouser.contraseña != " ")
            {
              Usuarios usuariobase = ouser.login(ouser);
              

                    bool validarcontraseña = ouser.ValidarContraseña(ouser, usuariobase);
                    bool validarusuario = ouser.ValidarUsuario(ouser, usuariobase);
                    bool validaradmin = ouser.ValidarAdmin(usuariobase);
                    if (validarcontraseña && validarusuario)
                    {
                        
                        if (validaradmin)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                        return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ViewBag.mensaje = " El usuario o contraseña es invalido";
                        ViewBag.ValidationFailedLogin = "Yes";
                    return View("Index",ouser);
                    }



               
            }
            else
            {
                ViewBag.ValidationFailedLogin = "Yes";
                return View("Index", ouser);
               
            }


        }
        [HttpPost]
        public ActionResult Registrar(Usuarios ouser)
        {
            string consulta = "AgregarUsuario";

            if (ModelState.IsValid)
            {
                bool validarcontraseña = ouser.CompararContraseña(ouser);

                if (validarcontraseña)
                {
                    ouser.registrar(ouser, consulta);
                    return View("index");
                }
                else
                {
                    ViewBag.mensaje = "Su contraseña y validacion no coinciden";
                     ViewBag.ValidationFailed = "Yes"; // flag to handle the client side scroll !
                    return View("Index", ouser);

                }


            }
            else
            {
                ViewBag.ValidationFailed = "Yes"; // flag to handle the client side scroll !
                return View("Index",ouser);
            }

        }

        public ActionResult Cotizar()
        {
            lista = new List<string>();
            lista.Add("Pesos");
            lista.Add("Dolares");
            lista.Add("Reales");
            ViewBag.lista = lista;
            return View();
        }
        [HttpPost]
        public ActionResult Cotizar(Monedas moneda)
        {
            double resultado = 0;
            switch (moneda.monedaaconvertir.ToString())
            {

                case "Pesos":
                    return new HttpStatusCodeResult(401);
                case "Reales":
                    return new HttpStatusCodeResult(403);
                case "Dolares":
                    if (moneda.monedaconvertida.ToString() == "Pesos")
                    { resultado = moneda.cantidad*19.45; }
                    else
                    {
                        resultado = moneda.cantidad * 3.2;
                    }
                    lista = new List<string>();
                    lista.Add("Pesos");
                    lista.Add("Dolares");
                    lista.Add("Reales");
                    ViewBag.lista = lista;
                    ViewBag.resultado = resultado;
                    return View();

            }
            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}