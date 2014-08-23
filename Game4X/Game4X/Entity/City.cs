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
        private EntityWorkshop EntityWorkshop; //A workshop gives the city a production queue

        public City(Texture2D CityTexture)
            : base(CityTexture)
        {
            this.Initialize();
            this.Texture = CityTexture;
            this.TextureRectangle = new Rectangle(0, 0, CityTexture.Width, CityTexture.Height);
        }

        
    }
}
