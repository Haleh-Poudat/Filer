using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Domain.Entities.Common;

namespace eBooks.Domain.Entities.Library
{
    public class LibraryCategory : BaseEntity
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public Guid? ParentId { set; get; }

        public virtual LibraryCategory Parent { set; get; }

        public virtual ICollection<Ebook> Ebooks { set; get; }
    }
}