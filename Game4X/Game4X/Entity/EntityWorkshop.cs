﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    /// <summary>
    /// A workshop contains builders, and has one builder working at a time. Imagine the workshop as a seperate production queue for anything that can be built.
    /// If this were Civ 5, each city would have 1 workshop, and that workshop contains a buidler for every unit/building that could be currently built.
    /// </summary>
    public class EntityWorkshop : IHasUIObject
    {
        private List<EntityBuilder> BuilderList = new List<EntityBuilder>(); 
        private EntityBuilder CurrentBuilder;
        public IHasWorkshop Parent;//The parent that owns this workshop, typically a city/building, but possibly a pregnant unit or something lol
        public UI.UIObject UIObject;

        public EntityWorkshop(IHasWorkshop Parent)
        {
            this.Parent = Parent;
            this.UIObject = new UI.UIObject(TextureHelper.GetTexture((int)TextureHelper.EntityID.Unknown));
            this.UIObject.ParentObject = this;
            this.UIObject.IconTexture = TextureHelper.GetTexture((int)TextureHelper.EntityID.Unknown);
            this.UIObject.IconRectangle = new Rectangle(0, 0, UIObject.IconTexture.Width, UIObject.IconTexture.Height);
        }

        /// <summary>
        /// Construct a new EntityWorkshop
        /// </summary>
        /// <param name="Parent">The object that owns this EntityWorkshop</param>
        /// <param name="UIObject">A UI Object for this Workshop.</param>
        public EntityWorkshop(IHasWorkshop Parent, UI.UIObject ParentUIObject)
        {
            this.Parent = Parent;
            this.UIObject = ParentUIObject;
        }

        /// <summary>
        /// Adds the passed in builder to the BuilderList. Currently no checks for duplicates.
        /// </summary>
        /// <param name="Builder"></param>
        public void AddBuilderToBuilderList(EntityBuilder Builder)
        {
            BuilderList.Add(Builder);
            UIObject.ChildrenUIObjects.Add(Builder.UIObject);
            Builder.ParentWorkshop = this;  
        }

        /// <summary>
        /// Adds the passed in builder to the BuilderList. Currently no checks for duplicates.
        /// </summary>
        /// <param name="Builder"></param>
        public void RemoveBuilderFromBuilderList(EntityBuilder Builder)
        {
            BuilderList.Remove(Builder);
            UIObject.ChildrenUIObjects.Remove(Builder.UIObject);
            Builder = null;
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
            
                if (CurrentBuilder.Completed())
                {
                    Parent.PlaceEntity(CurrentBuilder.ParentEntity);                    //Place the Entity on the map
                    AddBuilderToBuilderList(CurrentBuilder.ParentEntity.GetBuilder());  //Add a new builder to the list
                    RemoveBuilderFromBuilderList(CurrentBuilder);                      //Remove the old builder from the builder list
                    CurrentBuilder = null;
                }
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
