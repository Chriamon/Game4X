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

        public Unit(Texture2D UnitTexture)
            : base(UnitTexture)
        {
            this.Initialize();
            this.Texture = UnitTexture;
            this.TextureRectangle = new Rectangle(0, 0, UnitTexture.Width, UnitTexture.Height);

            //>>>Create the UI options that will exist upon selecting a unit i.e. Attack Move, Move, Fortify, Standing orders submenu, etc.

        }

        /// <summary>
        /// Orders the unit to move to the specified row/column. This default method uses a default pathing
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
        /// </summary>
        public override EntityBuilder GetBuilder()
        {
            return new EntityBuilder(typeof(Unit), ProductionRequired);
        }
    }
}
