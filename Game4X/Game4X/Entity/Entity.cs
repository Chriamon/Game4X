using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    //the base entity class, this is for any entity, they all need these methods implemented
    public class Entity
    {
        protected int hp;

        protected int mapy //the y location on the map where this entity is stored
        {get; set;}

        protected int mapx //the x location on the map where this entity is stored
        {get; set;}

        protected Texture2D Texture //the texture for this entity
        {get; set;}

        protected Rectangle TextureRectangle //the rectangle within the texture, outlines exactly what to draw for this entity
        {get; set;}

        public int owner //the playernumber of who owns this entity (if anyone does)
        { get; set; }

        public virtual void Initialize()
        {
            owner = 0;
        }

        public virtual void LoadTexture(ContentManager Content, String Texture)
        {
            this.Texture = Content.Load<Texture2D>(Texture);
        }

        public Entity(Texture2D EntityText)
        {
            this.Initialize();
            this.Texture = EntityText;
            this.TextureRectangle = new Rectangle(0, 0, EntityText.Width, EntityText.Height);
        }

        //called whenever this entity is to take damage
        public virtual void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
                OnDeath();
        }

        /// <summary>
        /// Runs one time when this entity is first selected
        /// </summary>
        public virtual void OnSelect()
        {
            
        }

        ///// <summary>
        ///// Runs everytime the game's "Update" loop is run if this unit is currently selected **This is temporary, might not stay if turns out to not be too useful
        ///// </summary>
        //public virtual void SelectedOnUpdate()
        //{
        //    
        //}

        /// <summary>
        /// Runs right before the turn goes over to the next turn. If this returns false, prevents the next turn from happening (and stops running other entity's TurnTick as a result)
        /// </summary>
        public virtual bool OnTurnTick()
        {
            return true;
        }

        /// <summary>
        /// runs when this entity is to "die"
        /// remove the entity from drawing and dispose of it
        /// </summary>
        public virtual void OnDeath()
        { 

        }

        public virtual void DrawEntity(SpriteBatch spriteBatch, int mapx, int mapy)
        {
            this.mapx = mapx;
            this.mapy = mapy;
            DrawEntity(spriteBatch);
        }

        /// <summary>
        /// 0 =  me anything else = enemy
        /// </summary>
        /// <param name="playernum"></param>
        /// <returns></returns>
        public static Color getOwnerPlayerColor(int playernum)
        {
            if (playernum == 0)
            {
                return Color.White;
            }
            else
            {
                return Color.Red;
            }
        }

        /// <summary>
        /// Draws the unit, uses last known x and y coordinates
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void DrawEntity(SpriteBatch spriteBatch)
        {
            int rowOffset = 0;
            if (mapy % 2 == 1)
                rowOffset = Tile.OddRowXOffset;

            Color PlayerColor = getOwnerPlayerColor(this.owner);

            spriteBatch.Draw(
                           Texture,
                           Camera.WorldToScreen(new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                           TextureRectangle,
                           PlayerColor,
                           0.0f,
                           Vector2.Zero,
                           1.0f,
                           SpriteEffects.None,
                           0.0f);
            

        }

        //Going to have the HUD draw itself, and the OnSelect() command will tell the HUD what to display.
        ///// <summary>
        ///// Draws the HUD elements for this unit, this is only called while the unit is selected
        ///// </summary>
        ///// <param name="spriteBatch"></param>
        //public virtual void DrawHUD(SpriteBatch spriteBatch)
        //{

        //}
    }
}
