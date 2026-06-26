using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate.Enums
{
    public enum Gender
    {
        [Display(Name = "نامشخص")]
        None,

        [Display(Name = "مرد")]
        Male,

        [Display(Name = "زن")]
        Female
    }
}
