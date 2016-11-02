using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteSharingApp.Models
{
    public class Document
    {
        public Document()
        {
            this.UserComments = new HashSet<UserComment>();
        }

        public int ID { get; set; }
        public int UserID { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public byte[] Size { get; set; }
        public string Extension { get; set; }
        public System.DateTime ModifiedData { get; set; }
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserComment> UserComments { get; set; }

    }
}
