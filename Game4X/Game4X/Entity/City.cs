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

        public override void Initialize()
        {
            //Initialize this object's UI object
            this.UIObject = new UI.UIObject();
            this.UIObject.ParentObject = this;

            //Initialize a new EntityWorkshop
            EntityWorkshop = new EntityWorkshop(this);

            //Add the EntityWorkshop's UIObject as a child to this City's UIObject
            UI.UIObject WorkshopUIObject = EntityWorkshop.UIObject;
            UIObject.ChildrenUIObjects.Add(WorkshopUIObject);

            Unit u = new Unit(TextureHelper.GetTexture((int)TextureHelper.EntityID.Unit));//Temp
            EntityWorkshop.AddBuilderToBuilderList(u.GetBuilder());
        }

        public override bool OnTurnTick()
        {
            if (EntityWorkshop != null)
            {
                EntityWorkshop.AddProduction(100);//Temporary for testing
            }
            return base.OnTurnTick();
        }
        
    }
}
