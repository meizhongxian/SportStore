using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue =false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="名称不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "描述不能为空")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="价格不能为负数")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="分类不能为空")]
        public string Category { get; set; }
    }
}
