
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
        public List<MediaModel> CarouselMedia { get; set; }

        public List<DayHours> DayHours { get; set; }
        public IndexViewModel()
        {
            CarouselMedia = new List<MediaModel>();
            DayHours = new List<DayHours>();
        }
    }
}