using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.UI
{
    static class HUD
    {
        private static Texture2D baseHud;
        private static SpriteFont spriteFont;
        private static UIObject ActiveUIObject; //The ActiveUIObject is the object that the HUD will use to draw the large bottom HUD area stuff

        public static void LoadContent(ContentManager Content)
        {
            baseHud = Content.Load<Texture2D>(@"Textures\HUD");
            spriteFont = Content.Load<SpriteFont>(@"Textures\Courier New");
        }

        public static void SetActiveUIObject(UIObject UIobj)
        {
            ActiveUIObject = UIobj;
        }

        public static UIObject GetActiveUIObject()
        {
            return ActiveUIObject;
        }

        public static void ClearActiveUIObject()
        {
            ActiveUIObject = null;
        }

        /// <summary>
        /// Draws the HUD and everything associated with the HUD layer
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            //All this is temporary, if there is actually going to be a HUD that covers the bottom, should only render the hexes that arent hidden behind the HUD
            int height = 100;
            //bottom bar
            spriteBatch.Draw(baseHud, new Rectangle(0, Camera.ViewHeight - height, Camera.ViewWidth, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

            //top bar
            spriteBatch.Draw(baseHud, new Rectangle(0, 0, Camera.ViewWidth, 20), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);

            //text on the top bar
            spriteBatch.DrawString(spriteFont, "Turn:", new Vector2(Camera.ViewWidth * .9f, 0), Color.White, 0, Vector2.Zero, .45f, SpriteEffects.None, 0.0f);

            //Turn number
            spriteBatch.DrawString(spriteFont, Main.Turn.ToString(), new Vector2(Camera.ViewWidth * .9f + 50, 0), Color.White, 0, Vector2.Zero, .45f, SpriteEffects.None, 0.0f);

            //HUD elements for selected entity
            //>>>TODO: draw the HUD based on what is selected
            //foreach (Entity.Entity u in Main.SelectedUnitList)
            //{
            //    u.DrawHUD(spriteBatch);
            //}
        }
    }
}
