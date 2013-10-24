using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    public interface IControlledRemover
    {
        void PrepareRemoval(object item);
    }
}
