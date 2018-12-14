using BusinessLogicLayer.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult Index(int? page, string sortOrder, string currentFilter, string searchString)
        {
            int pagenumber = (page ?? 1) - 1; //I know what you're thinking, don't put it on 0 :)
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int totalCount = 0;
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel();
            userAdminViewModel.UserName = User.Identity.Name;
            var locationDetails = GetLocationDetailPage(pagenumber, 5, out totalCount);

            switch (sortOrder)
            {
                case "name_desc":
                    locationDetails = locationDetails.OrderByDescending(s => s.LocationName).ToList();
                    break;
                case "Town":
                    locationDetails = locationDetails.OrderBy(s => s.LocationTown).ToList();
                    break;
                case "date_desc":
                    locationDetails = locationDetails.OrderByDescending(s => s.CreatedOn).ToList();
                    break;
                default:
                    locationDetails = locationDetails.OrderByDescending(s => s.LocationName).ToList();
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                locationDetails = locationDetails.Where(s => s.LocationName.Contains(searchString)).ToList();
                                      // || s.FirstMidName.Contains(searchString));
            }
            ViewBag.Message = "Add Details.";
            userAdminViewModel.LocationDetails = locationDetails;
            
            var pagedList = locationDetails.ToPagedList(pageIndex, pageSize);
            userAdminViewModel.PagedList = pagedList;
            var pc = totalCount/pageSize;
            userAdminViewModel.PageCount = pc +1;
            userAdminViewModel.PageNumber = pageIndex;
            return View(userAdminViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel();
            userAdminViewModel.UserName = User.Identity.Name;

            userAdminViewModel.LocationTypeDetail = new LocationTypeDetailModel();
            userAdminViewModel.LocationTypeDetail.LocationTypeList = GetLocationTypeSelectList();
            userAdminViewModel.LocationTypeDetail.CountyList = GetCountySelectList();

            return PartialView("~/Views/UserAdmin/UserAdminPartials/_AddDetailsPartial.cshtml", userAdminViewModel);
            //return View(userAdminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdminViewModel userAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                var locationDetail = MapToLocationDetail(userAdminViewModel);
               
                locationDetail.CreatedBy = User.Identity.Name;
                locationDetail.CreatedOn = DateTime.Now;
                locationDetail.IsActive = true;
                _locationTypeRepository.SaveLocationDetail(locationDetail);

                ViewBag.Message = "Create.";

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: /Movies/Edit/5
        [HttpGet]
        //public ActionResult Edit(int id)
        public ActionResult Edit(int id)
        {
            LocationDetail locationDetail = _locationTypeRepository.GetLocationDetailById(id);//db.Movies.Find(id);

            UserAdminViewModel userAdminViewModel = MapToUserAdminViewModel(locationDetail);
            
            if (locationDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/UserAdmin/UserAdminPartials/_EditDetailsPartial.cshtml", userAdminViewModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAdminViewModel userAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                var locationDetail = MapToLocationDetail(userAdminViewModel);

                locationDetail.EditedBy = User.Identity.Name;
                locationDetail.EditedOn = DateTime.Now;
                _locationTypeRepository.SaveLocationDetail(locationDetail);
                //db.Entry(movie).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAdminViewModel);
        }

        [HttpGet]
        //public ActionResult Edit(int id)
        public ActionResult Delete(int id)
        {
            LocationDetail locationDetail = _locationTypeRepository.GetLocationDetailById(id);//db.Movies.Find(id);

            UserAdminViewModel userAdminViewModel = MapToUserAdminViewModel(locationDetail);

            if (locationDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/UserAdmin/UserAdminPartials/_DeleteDetailsPartial.cshtml", userAdminViewModel);
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserAdminViewModel userAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                var locationDetail = MapToLocationDetail(userAdminViewModel);
                //Soft Delete
                locationDetail.IsActive = false;
                locationDetail.EditedBy = User.Identity.Name;
                locationDetail.EditedOn = DateTime.Now;
                _locationTypeRepository.SaveLocationDetail(locationDetail);
               
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            //return View(userAdminViewModel);
        }


        #region Private Methods

        private IList<LocationDetail> GetLocationDetailPage(int page, int itemsPerPage, out int totalCount)
        {
            var locationDetails = _locationTypeRepository.GetAllLocationDetails().ToList();

            if (locationDetails != null)
            {
                locationDetails.OrderByDescending(x => x.LocationTypeID).Skip(itemsPerPage * page).Take(itemsPerPage).ToList();
            }

            totalCount = locationDetails.Count();//return the number of pages
            return locationDetails.ToList();//the query is now already executed, it is a subset of all the orders.
        }

        private SelectList GetLocationTypeSelectList()
        {
            var locationTypes = _locationTypeRepository.GetAllLocationTypes();

            var locationTypesSelectList = locationTypes
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.LocationTypeID.ToString(),
                                    Text = x.LocationTypeDescription
                                });
            return new SelectList(locationTypesSelectList, "Value", "Text");
        }

        private SelectList GetCountySelectList()
        {
            var allCounties = _locationTypeRepository.GetAllCounties().OrderByDescending(x => x.CountyValue);

            var countiesSelectList = allCounties
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.CountyValue,
                                    Text = x.CountyName,
                                });

            return new SelectList(countiesSelectList, "Value", "Text");
        }

        private LocationDetail MapToLocationDetail(UserAdminViewModel userAdminViewModel)
        {
            LocationDetail locationDetail = new LocationDetail
            {
                LocationDetailsId = userAdminViewModel.LocationTypeDetail.LocationTypeDetailsId,
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
                LocationLatitude = userAdminViewModel.LocationTypeDetail.LocationLatitude,
                LocationLongitude = userAdminViewModel.LocationTypeDetail.LocationLongitude,
                IsActive = userAdminViewModel.LocationTypeDetail.IsActive
            };

            //Handle Image
            if (userAdminViewModel.LocationTypeDetail.LocationTypeImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    userAdminViewModel.LocationTypeDetail.LocationTypeImage.InputStream.CopyTo(memoryStream);

                    byte[] array = memoryStream.GetBuffer();
                    locationDetail.LocationImage = array;
                }
            }
            return locationDetail;
        }

        private UserAdminViewModel MapToUserAdminViewModel(LocationDetail locationDetail)
        {
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel();
            userAdminViewModel.UserName = User.Identity.Name;

            userAdminViewModel.LocationTypeDetail = new LocationTypeDetailModel
            {
                LocationTypeDetailsId = locationDetail.LocationDetailsId,
                SelectedLocationTypeCode = locationDetail.LocationTypeID,
                LocationTypeName = locationDetail.LocationName,
                LocationTypeDescription = locationDetail.LocationDescription,
                LocationTypeAddress1 = locationDetail.LocationAddress1,
                LocationTypeAddress2 = locationDetail.LocationAddress2,
                LocationTypeAddress3 = locationDetail.LocationAddress3,
                LocationTypeTown = locationDetail.LocationTown,
                SelectedCounty = locationDetail.LocationCountyValue,
                LocationTypeEirCode = locationDetail.LocationEircode,
                LocationTypeEmail = locationDetail.LocationEmail,
                LocationTypeURL = locationDetail.LocationURL,
                LocationTypePhone = locationDetail.LocationPhone,
                LocationLatitude = locationDetail.LocationLatitude,
                LocationLongitude = locationDetail.LocationLongitude,
                //DisplayLocationTypeImage = locationDetail.LocationImage,
                IsActive = locationDetail.IsActive 
            };
            
            if (locationDetail.LocationImage != null)
            {
                var base64 = Convert.ToBase64String(locationDetail.LocationImage);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                userAdminViewModel.LocationTypeDetail.DisplayLocationTypeImage = locationDetail.LocationImage;

            }
            
            userAdminViewModel.LocationTypeDetail.CountyList = GetCountySelectList();
            userAdminViewModel.LocationTypeDetail.LocationTypeList = GetLocationTypeSelectList();

            return userAdminViewModel;
        }
        
        #endregion

        //public ActionResult GetImage(byte[] imageData)
        //{
        //    byte[] img = 
        //      return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
        //}
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