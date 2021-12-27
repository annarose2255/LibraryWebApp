using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon.DTO
{
    public class SupportDTO : BaseDTO
    {
        public int SupportId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsMember { get; set; }
        public string Message { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool FollowedUp { get; set; } //might need to change to int
        public int UserIDFollowedUp_FK {get; set; }


        public SupportDTO() : base()
        { }

    }
}
