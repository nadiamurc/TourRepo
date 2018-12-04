using BusinessLogicLayer.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tour.DomainModel;

namespace Tour.Models
{
    public class UserAdminViewModel
    {
        public string UserName { get; set; }

        public LocationType LocationType { get; set; }
        public IList<LocationType> GetAllLocationTypes { get; set; }

    }
}