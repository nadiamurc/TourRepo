using System.Collections.Generic;
using PagedList;
using Tour.DomainModel;

namespace Tour.Models
{
    public class UserAdminViewModel
    {
        public IList<LocationDetail> LocationDetails;

        public string UserName { get; set; }

        public int PageCount { get; set; }
        public int PageNumber { get; set; }

        public LocationType LocationType { get; set; }

        public LocationTypeDetailModel LocationTypeDetail { get; set; }
        public IPagedList<LocationDetail> PagedList { get; internal set; }
    }
}