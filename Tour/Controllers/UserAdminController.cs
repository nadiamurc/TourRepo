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
    //[RequireHttps]
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
            userAdminViewModel.LocationTypeDetail = new LocationTypeDetailModel();

            userAdminViewModel.LocationTypeDetail.LocationTypeList = GetLocationTypeSelectList();
            userAdminViewModel.LocationTypeDetail.CountyList = GetCountySelectList();
            //.LocationTypeList = _locationTypeRepository.GetAllLocationTypes();

            //Need to map the Form input to DB model

            ViewBag.Message = "Add Details.";

            return View(userAdminViewModel);
        }

        private SelectList GetLocationTypeSelectList()
        {
            var counties = _locationTypeRepository.GetAllLocationTypes();

            var countyList = counties
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.LocationTypeID.ToString(),
                                    Text = x.LocationTypeDescription
                                });
            return new SelectList(countyList, "Value", "Text");
        }
        
        private SelectList GetCountySelectList()
        {
            var locationTypes = _locationTypeRepository.GetAllCounties().OrderByDescending(x => x.CountyValue);

            var types = locationTypes                        
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.CountyValue,
                                    Text = x.CountyName
                                });
            return new SelectList(types, "Value", "Text");

        }
       
        //public ActionResult UserAdmin()
        //{
        //    UserAdminViewModel userAdminViewModel = new UserAdminViewModel();

            
        //    userAdminViewModel.UserName = User.Identity.Name;
        //    userAdminViewModel.LocationTypeDetail = new LocationTypeDetailModel();

        //    userAdminViewModel.LocationTypeDetail.LocationTypeList = GetLocationTypeSelectList();
        //    //.LocationTypeList = _locationTypeRepository.GetAllLocationTypes();

        //    //Need to map the Form input to DB model

        //    ViewBag.Message = "Your application MyAccount page.";

        //    return View(userAdminViewModel);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdminViewModel userAdminViewModel)
        {

            if (ModelState.IsValid)
            {
                //possibly use Automapper
                
                var locationDetail = MapToLocationDetail(userAdminViewModel);
                
                _locationTypeRepository.SaveLocationDetail(locationDetail);
    
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
                //return View(userAdminViewModel);
            }
        }

        private LocationDetail MapToLocationDetail(UserAdminViewModel userAdminViewModel)
        {
            LocationDetail locationDetail = new LocationDetail
            {
                LocationName = userAdminViewModel.LocationTypeDetail.LocationTypeName,
                LocationTypeID = userAdminViewModel.LocationTypeDetail.SelectedLocationTypeCode,
                LocationDescription = userAdminViewModel.LocationTypeDetail.LocationTypeDescription,
                LocationAddress1 = userAdminViewModel.LocationTypeDetail.LocationTypeAddress1,
                LocationAddress2 = userAdminViewModel.LocationTypeDetail.LocationTypeAddress2,
                LocationAddress3 = userAdminViewModel.LocationTypeDetail.LocationTypeAddress3,
                LocationTown = userAdminViewModel.LocationTypeDetail.LocationTypeTown,
                LocationCountyValue = userAdminViewModel.LocationTypeDetail.SelectedCounty,
                LocationEircode = userAdminViewModel.LocationTypeDetail.LocationTypeEirCode,
                LocationEmail = userAdminViewModel.LocationTypeDetail.LocationTypeEmail,
                LocationURL = userAdminViewModel.LocationTypeDetail.LocationTypeURL,
                LocationPhone = userAdminViewModel.LocationTypeDetail.LocationTypePhone,
                LocationGPS = userAdminViewModel.LocationTypeDetail.LocationTypeGPS,
                LocationImage = userAdminViewModel.LocationTypeDetail.LocationTypeImage

            };


            return locationDetail;
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(movie);
        //}
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}