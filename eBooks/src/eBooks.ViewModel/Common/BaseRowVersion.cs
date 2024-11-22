using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Common
{
    public abstract class BaseRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}