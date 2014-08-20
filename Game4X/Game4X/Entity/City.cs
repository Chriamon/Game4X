using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    class City : Entity
    {
        public City(Texture2D CityTexture)
            : base(CityTexture)
        {
            this.Initialize();
            this.Texture = CityTexture;
            this.TextureRectangle = new Rectangle(0, 0, CityTexture.Width, CityTexture.Height);
        }

        /// <summary>
        /// Override OnSelect to set the hud up for CITY mode.
        /// </summary>
        public override void OnSelect()
        {
            //>>> Add the information necessarry to the HUD class for it to draw the City HUD

            base.OnSelect();
        }
    }
}
