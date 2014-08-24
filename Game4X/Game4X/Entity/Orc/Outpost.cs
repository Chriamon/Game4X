using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity.Orc
{
    class Outpost : City
    {
        public Outpost()
            : base()
        {
            this.Initialize();
            this.Texture = TextureHelper.GetTexture((int)(TextureHelper.EntityID.Orc_Outpost));
            this.TextureRectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
        }

        public override void Initialize()
        {

            //Initialize this object's UI object
            UIObject = new UI.UIObject(TextureHelper.GetTexture((int)TextureHelper.EntityID.Orc_Outpost));
            UIObject.ParentObject = this;

            DisplayScale = .6f;

            //Initialize a new EntityWorkshop
            Workshop = new EntityWorkshop(this);

            //Add the EntityWorkshop's UIObject as a child to this City's UIObject
            UI.UIObject WorkshopUIObject = Workshop.UIObject;
            WorkshopUIObject.IconTexture = TextureHelper.GetTexture((int)TextureHelper.EntityID.Orc_BuildIcon);
            WorkshopUIObject.IconRectangle = new Rectangle(0, 0, WorkshopUIObject.IconTexture.Width, WorkshopUIObject.IconTexture.Height);
            WorkshopUIObject.DisplayScale = .75f;
            UIObject.ChildrenUIObjects.Add(WorkshopUIObject);

            Peon u = new Peon();//Temp
            Workshop.AddBuilderToBuilderList(u.GetBuilder());
        }
    }
}
