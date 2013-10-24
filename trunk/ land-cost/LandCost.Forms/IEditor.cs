using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Forms
{
    public interface IEditor<T>
    {
        object Entity { get; set; }
        List<T> CheckList { get; set; }
        object ParentObject { get; set; }
        T NewItem { get; }
    }
}
