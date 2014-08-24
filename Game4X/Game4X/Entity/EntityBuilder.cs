using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game4X.Entity
{
    /// <summary>
    /// This class is used whenever a unit needs to be created.
    /// </summary>
    public class EntityBuilder : IHasUIObject
    {
        public Entity ParentEntity;
        public TextureHelper.EntityID EntityType;
        public int ProductionRequired; //Temporary, eventually switch to a resources struct with specific resources?
        private int ProductionCompleted;
        public UI.UIObject UIObject;
        public EntityWorkshop ParentWorkshop;

        public EntityBuilder(TextureHelper.EntityID EntityType, int ProductionRequired)
        {
            this.EntityType = EntityType;
            this.ProductionRequired = ProductionRequired;
            this.UIObject = new UI.UIObject(TextureHelper.GetTexture((int)EntityType));
            this.UIObject.ParentObject = this;
        }

        public void OnIconClicked()
        {
            ParentWorkshop.SetCurrentBuilder(this);
            ParentWorkshop.SetAsActiveUI();
        }

        public void AddProduction(int Production)
        {
            this.ProductionCompleted += Production;
        }

        public bool Completed()
        {
            if (ProductionCompleted > ProductionRequired)
            {
                return true;
            }

            return false;
        }

    }
}
