using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour.DomainModel;
using Tour.Models;

namespace Tour.Controllers
{
    [RequireHttps]
    public class UserAdminController : Controller
    {
        private readonly ILocationTypeRepository _locationTypeRepository;

        public UserAdminController(ILocationTypeRepository locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }
        public ActionResult Index()
        {
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel();


            userAdminViewModel.UserName = User.Identity.Name;
            userAdminViewModel.GetAllLocationTypes = _locationTypeRepository.GetAllLocationTypes();

            ViewBag.Message = "Your application MyAccount page.";

            return View();
        }

        public ActionResult UserAdmin()
        {
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel();

            
            userAdminViewModel.UserName = User.Identity.Name;
            userAdminViewModel.GetAllLocationTypes = _locationTypeRepository.GetAllLocationTypes();

            ViewBag.Message = "Your application MyAccount page.";

            return View(userAdminViewModel);
        }

        [HttpPost]
        public ActionResult Create(UserAdminViewModel userAdminViewModel)
        {

            if (ModelState.IsValid)
            {
                _locationTypeRepository.SaveLocationType(userAdminViewModel.LocationType);
               

                return RedirectToAction("Index");
            }
            else
            {
                return View(userAdminViewModel);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}