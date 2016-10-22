using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteSharingApp.Models
{
    public class UserComment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DocumentID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Vote { get; set; }

        public virtual Document Document { get; set; }
        public virtual User User { get; set; }
    }
}
