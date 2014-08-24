using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity.Orc
{
    class Peon: Unit
    {

        public Peon()
            : base()
        {
            this.Initialize();
            this.Texture = TextureHelper.GetTexture((int)(TextureHelper.EntityID.Orc_Peon));
            this.TextureRectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);

            //>>>Create the UI options that will exist upon selecting a unit i.e. Attack Move, Move, Fortify, Standing orders submenu, etc.

        }

        public override void Initialize()
        {
            base.Initialize();
            ProductionRequired = 200;
        }

        /// <summary>
        /// Returns a builder for the specified type of Entity.
        /// </summary>
        public override EntityBuilder GetBuilder()
        {
            EntityBuilder Builder = new EntityBuilder(TextureHelper.EntityID.Orc_Peon, ProductionRequired);
            Builder.ParentEntity = new Orc.Peon();
            return Builder;
        }
    }
}
