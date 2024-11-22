using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Domain.Entities.Common;

namespace eBooks.Domain.Entities.Library
{
    public class Ebook : BaseEntity
    {
        #region Properties

        public string Title { set; get; }

        /// <summary>
        /// File Name
        /// </summary>
        public string Name { set; get; }

        public string Description { set; get; }

        /// <summary>
        /// File Path
        /// </summary>
        public string Path { set; get; }

        public Guid LibraryCategoryId { set; get; }

        public virtual LibraryCategory LibraryCategory { set; get; }

        #endregion Properties
    }
}