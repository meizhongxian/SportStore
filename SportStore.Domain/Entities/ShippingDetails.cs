using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="名字不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage ="地址一")]
        [Display(Name ="地址一")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage ="城市不能为空")]
        public string City { get; set; }

        [Required(ErrorMessage ="状态不能为空")]
        public string State { get; set; }
        public string Zip { get; set; }

        [Required(ErrorMessage = "国家不能为空")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
