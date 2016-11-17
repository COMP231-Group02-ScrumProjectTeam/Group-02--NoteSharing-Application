using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public long Size { get; set; }
        public string Extension { get; set; }
        public System.DateTimeOffset? UploadDateTimeOffset { get; set; }
        public string DocumentType { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserComment> UserComments { get; set; }

    }
}
