using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game4X.Entity
{
    /// <summary>
    /// A workshop contains builders, and has one builder working at a time. Imagine the workshop as a seperate production queue for anything that can be built.
    /// If this were Civ 5, each city would have 1 workshop, and that workshop contains a buidler for every unit/building that could be currently built.
    /// </summary>
    public class EntityWorkshop : IHasUIObject
    {
        //private List<EntityBuilder> BuilderList = new List<EntityBuilder>(); //Is this needed?
        private EntityBuilder CurrentBuilder;
        private Entity Parent;//The parent that owns this workshop, typically a city/building, but possibly a pregnant unit or something lol
        private UI.UIObject UIObject;

        public EntityWorkshop(Entity Parent)
        {
            this.Parent = Parent;
            this.UIObject = new UI.UIObject();
            this.UIObject.ParentObject = Parent;
        }

        public EntityWorkshop(Entity Parent, UI.UIObject UIObject)
        {
            this.Parent = Parent;
            this.UIObject = UIObject;
        }

        /// <summary>
        /// Adds the passed in builder to the BuilderList. Currently no checks for duplicates.
        /// </summary>
        /// <param name="Builder"></param>
        public void AddBuilderToBuilderList(EntityBuilder Builder)
        {
            //BuilderList.Add(Builder);
            UIObject.ChildrenUIObjects.Add(Builder.UIObject);
        }

        /// <summary>
        /// Set the CurrentBuilder to the passed in Builder. Currently no checks that this workshop actually has the specified Builder on it's list.
        /// </summary>
        /// <param name="Builder"></param>
        public void SetCurrentBuilder(EntityBuilder Builder)
        {
            CurrentBuilder = Builder;
        }

        /// <summary>
        /// Adds the specified amount of production to the CurrentBuilder. If there is no current builder it currently will do nothing with the production.
        /// Extra production does not overflow currently.
        /// </summary>
        /// <param name="AmountOfProduction"></param>
        public void AddProduction(int AmountOfProduction)
        {
            if (CurrentBuilder != null)
            {
                CurrentBuilder.AddProduction(AmountOfProduction);
            }
        }

        public void SetAsActiveUI()
        {
            UI.HUD.SetActiveUIObject(UIObject);
        }

        public void OnIconClicked()
        {
            SetAsActiveUI();
        }
    }
}
