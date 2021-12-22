using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon
{
    public class MediaDTO
    { 
        //TODO: add view that will have the media type, genere, and publisher/author 
        public int MediaId { get; set; }
        public int MediaTypeID { get; set; }
        public int GenreTypeID { get; set; }
        public int PublisherID { get; set; }
        public int IsCheckedOutUserID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedByUserID { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }

    }
}
