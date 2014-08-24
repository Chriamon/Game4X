using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game4X.Entity
{
    public interface IHasWorkshop
    {
        /// <summary>
        /// Put Entity E on the map.
        /// </summary>
        /// <param name="E"></param>
        void PlaceEntity(Entity E);
    }
}
