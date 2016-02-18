using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum WorkNotificationStatus
    {
        [Display(Name = "Сервис работает в штатном режиме")]
        Ready = 0,

        [Display(Name = "Сервис недоступен, ведутся технические работы")]
        Busy 
    }
}
