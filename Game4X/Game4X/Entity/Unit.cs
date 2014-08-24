using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    public class Unit : Entity
    {
        protected int movement
        { get; set; }

        public Unit()
            : base()
        {
           Initialize();
           this.Texture = TextureHelper.GetTexture((int)(TextureHelper.EntityID.Unit));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            //>>>Create the UI options that will exist upon selecting a unit i.e. Attack Move, Move, Fortify, Standing orders submenu, etc.
            this.UIObject = new UI.UIObject(TextureHelper.GetTexture((int)TextureHelper.EntityID.Unit));
            this.UIObject.ParentObject = this;
            this.DisplayScale = 1.0f;
        }

        /// <summary>
        /// Orders the unit to move to the specified row/column. This default method uses a default pathing.
        /// Only override if the unit moves differntly than default.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public virtual bool Move(int row, int column)
        {
            MapCell Source;
            MapCell TargetCell;
            Source = Main.theMap.Rows[mapy].Columns[mapx];
            TargetCell = Main.theMap.Rows[row].Columns[column];

            if (TargetCell.UnitCanMoveHere(this))
            {
                Source.EntityList.Remove(this);
                TargetCell.EntityList.Add(this);
                return true;
            }
            else
            {
            }

            return false;
        }

        /// <summary>
        /// Returns a builder for the specified type of Entity.
        /// MUST be overridden for each unit
        /// </summary>
        public override EntityBuilder GetBuilder()
        {
            EntityBuilder Builder = new EntityBuilder(TextureHelper.EntityID.Unit, ProductionRequired);
            Builder.ParentEntity = new Unit();
            return Builder;
        }
    }
}
