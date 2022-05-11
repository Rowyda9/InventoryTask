using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackCore.Utilities.Paging
{
    public class PagedResult<T>
    {

       public List<T> Result { set; get; }
        
       public int TotalCount { set; get; }
     
    }
}
