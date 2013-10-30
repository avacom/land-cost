using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Forms
{
    public interface IModificationAnnouncer
    {
        event EventHandler Modified;
    }
}
