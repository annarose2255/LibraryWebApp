using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class MediaCarouselModel
    {
        public MediaModel Media1 { get; set; }
        public MediaModel Media2 { get; set; }
        public MediaModel Media3 { get; set; }



        //MAY NEED TO GET RID OF!
        public MediaCarouselModel()
        {
            Media1 = new MediaModel();
            Media2 = new MediaModel();
            Media3 = new MediaModel();
        }
    }


}