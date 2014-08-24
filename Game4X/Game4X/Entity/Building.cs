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
        public Building()
            : base()
        {
            this.Initialize();
            this.Texture = TextureHelper.GetTexture((int)(TextureHelper.EntityID.Building));
            this.TextureRectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
        }

        /// <summary>
        /// Returns a builder for the specified type of Entity.
        /// </summary>
        public override EntityBuilder GetBuilder()
        {
            return new EntityBuilder(TextureHelper.EntityID.Building, ProductionRequired);
        }
    }
}
