using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Tour.Models
{
    public class LocationTypeDetailModel
    {
        [Required(ErrorMessage = "Location Name is required")]
        [DisplayName("Name *:")]
        public string LocationTypeName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description *:")]
        public string LocationTypeDescription { get; set; }
        [DisplayName("Address 1 :")]
        public string LocationTypeAddress1 { get; set; }
        [DisplayName("Address 2 :")]
        public string LocationTypeAddress2 { get; set; }
        [DisplayName("Address 3 :")]
        public string LocationTypeAddress3 { get; set; }
        [DisplayName("Town :")]
        public string LocationTypeTown { get; set; }

        public string SelectedCounty { get; set; }

        [DisplayName("Select County :")]
        public SelectList CountyList { get; set; }

        [DisplayName("Eircode :")]
        public string LocationTypeEirCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone :")]
        public string LocationTypePhone { get; set; }

        [DisplayName("Web Address :")]
        [DataType(DataType.Url)]
        public string LocationTypeURL { get; set; }

        [DisplayName("Email :")]
        [DataType(DataType.EmailAddress)]
        public string LocationTypeEmail { get; set; }

        [DisplayName("Image :")]
        public byte[] LocationTypeImage { get; set; }

        [DisplayName("GPS :")]
        public string LocationTypeGPS { get; set; }

        [Required(ErrorMessage = "Select Type is required")]
        public string SelectedLocationTypeCode { get; set; }
       
        [DisplayName("Select Location Type *:")]
        public SelectList LocationTypeList { get; set; }
    }
}