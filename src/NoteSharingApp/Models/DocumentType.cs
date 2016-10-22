using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShare.Models
{
    public class DocumentType
    {
        public DocumentType()
        {
            this.Documents = new HashSet<Document>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
