using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    class Building : Entity
    {
        public Building(Texture2D BuildingTexture)
            : base(BuildingTexture)
        {
            this.Initialize();
            this.Texture = BuildingTexture;
            this.TextureRectangle = new Rectangle(0, 0, BuildingTexture.Width, BuildingTexture.Height);
        }
    }
}
