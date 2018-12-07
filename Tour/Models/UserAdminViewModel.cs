using System.Collections.Generic;
using Tour.DomainModel;

namespace Tour.Models
{
    public class UserAdminViewModel
    {
        public string UserName { get; set; }

        public LocationType LocationType { get; set; }

        public LocationTypeDetailModel LocationTypeDetail { get; set; }        
      
    }
}