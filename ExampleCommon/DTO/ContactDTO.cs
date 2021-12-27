using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon.DTO
{
    public class ContactDTO
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool FollowedUp { get; set; } //might need to change to int
    }
}
