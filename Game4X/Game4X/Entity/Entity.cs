using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    //the base entity class, this is for any entity, they all use these methods
    public class Entity : IHasUIObject
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

        protected UI.UIObject UIObject //The UIObject associated with this entity. When this unit is selected it will set this UIObject as active.
        {get; set;}

        protected int ProductionRequired
        { get; set; }

        public int owner //the playernumber of who owns this entity (if anyone does)
        { get; set; }

        public virtual void Initialize()
        {
            this.UIObject = new UI.UIObject(TextureHelper.GetTexture((int)TextureHelper.EntityID.Entity));
            this.UIObject.ParentObject = this;
        }

        //public virtual void LoadTexture(ContentManager Content, String Texture)
        //{
        //    this.Texture = Content.Load<Texture2D>(Texture);
        //}

        public Entity(Texture2D EntityTexture)
        {
            this.Initialize();
            this.Texture = EntityTexture;
            this.TextureRectangle = new Rectangle(0, 0, EntityTexture.Width, EntityTexture.Height);
        }

        public Entity(Texture2D EntityTexture, Rectangle EntityTextureRectangle)
        {
            this.Initialize();
            this.Texture = EntityTexture;
            this.TextureRectangle = EntityTextureRectangle;
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
            UI.HUD.SetActiveUIObject(this.UIObject);
        }

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

        public void SetAsActiveUI()
        {
            UI.HUD.SetActiveUIObject(UIObject);
        }

        public void OnIconClicked()
        {
            SetAsActiveUI();
        }

        /// <summary>
        /// Returns a builder for the specified type of Entity.
        /// </summary>
        public virtual EntityBuilder GetBuilder()
        {
            return new EntityBuilder(TextureHelper.EntityID.Entity, ProductionRequired);
        }
    }
}
