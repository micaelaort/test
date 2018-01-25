using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class AdminController : Controller
    {
        List<Usuarios> listauser = new List<Usuarios>();
        Usuarios user = new Usuarios();
        // GET: Admin
        public ActionResult Index()
        {
            listauser = user.SeleccionarUsuarios();
            ViewBag.lista = listauser;
            return View();
        }

        public ActionResult Modificar(int id)
        {
            user = user.TraerUsuario(id);
            return View("Modificar",user);
        }
        [HttpPost]
        public ActionResult Modificar(Usuarios ouser)
        {

            if (ModelState.IsValid)
            {
                bool validarcontraseña = ouser.CompararContraseña(ouser);

                if (validarcontraseña)
                {
                    user.Modificar(ouser);
                    listauser = user.SeleccionarUsuarios();
                    ViewBag.lista = listauser;
                    return View("Index");
                }
                else
                {
                    
                    return View("Modificar", ouser);

                }


            }
            else
            {
                
                return View("Modificar", ouser);
            }



        
        }

   
        public ActionResult Eliminar(int id)
        {
            user.EliminarUsuario(id);
            listauser = user.SeleccionarUsuarios();
            ViewBag.lista = listauser;
            return View("Index");
        }
    }
}