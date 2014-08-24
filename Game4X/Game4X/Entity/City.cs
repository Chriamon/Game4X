using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    class City : Entity, IHasWorkshop
    {
        protected EntityWorkshop Workshop; //A workshop gives the city a production queue

        public City()
            : base()
        {
            this.Initialize();
            this.Texture = TextureHelper.GetTexture((int)(TextureHelper.EntityID.City));
            this.TextureRectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
        }

        public override void Initialize()
        {
            //Initialize this object's UI object
            UIObject = new UI.UIObject(TextureHelper.GetTexture((int)TextureHelper.EntityID.City));
            UIObject.ParentObject = this;

            DisplayScale = 1.0f;

            //Initialize a new EntityWorkshop
            Workshop = new EntityWorkshop(this);

            //Add the EntityWorkshop's UIObject as a child to this City's UIObject
            UI.UIObject WorkshopUIObject = Workshop.UIObject;
            UIObject.ChildrenUIObjects.Add(WorkshopUIObject);

            //Example of how to add a unit to the workshop. Eventually this should be cleaned up.
            Unit u = new Unit();
            Workshop.AddBuilderToBuilderList(u.GetBuilder());

        }

        public override bool OnTurnTick()
        {
            if (Workshop != null)
            {
                Workshop.AddProduction(100);//Temporary for testing
            }
            return base.OnTurnTick();
        }


        public void PlaceEntity(Entity E)
        {
            MapCell SourceCell = Main.theMap.Rows[mapy].Columns[mapx];

            SourceCell.AddUnit(E);
        }
    }
}
