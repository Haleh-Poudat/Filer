using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.Domain.Entities.Common
{
    public abstract class BaseEntity : Entity
    {
        #region Properties

        public virtual Guid Id { get; set; }

        #endregion Properties
    }
}