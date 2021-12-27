
using LibraryCommon;
using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationExample.Models
{
    public class IndexViewModel
    {
        public MediaCarouselModel CarouselMedia { get; set; }

        public List<DayHours> DayHours { get; set; }
    }
}