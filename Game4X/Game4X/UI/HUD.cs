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
        /// Send a click to the HUD at the specified X/Y coordinates
        /// </summary>
        /// <param name="x">The x coordinate of the mouse click</param>
        /// <param name="y">The y coordinate of the mouse click</param>
        public static void OnClick(int x, int y)
        {
            //>> This function will need a LOT of cleanup eventually.

            if (ActiveUIObject != null)
            {
                //There is an active UIObject
                Rectangle IconRect;
                if (ActiveUIObject.ChildrenUIObjects.Count > 0)
                {
                    //The ActiveUIObject has children
                    foreach (UIObject obj in ActiveUIObject.ChildrenUIObjects)
                    {
                        IconRect = obj.DisplayRectangle;

                        if (x > IconRect.X && x < IconRect.X + IconRect.Width && y > IconRect.Y && y < IconRect.Y + IconRect.Height)
                        { 
                            //The mouseclick is on this icon.
                            obj.OnLeftClick();
                            break;
                        }
                        
                    }
                }
                else
                {
                    IconRect = ActiveUIObject.DisplayRectangle;

                    if (x > IconRect.X && x < IconRect.X + IconRect.Width && y > IconRect.Y && y < IconRect.Y + IconRect.Height)
                    {
                        //The mouseclick is on this icon.
                        ActiveUIObject.OnLeftClick();
                    }
                }
            }
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

            //HUD elements for selected UIObject
            //>> This section will need MASSIVE cleanup eventually.
            if (ActiveUIObject != null)
            {
                //There is an active UIObject
                int xoffset = 15;
                if (ActiveUIObject.ChildrenUIObjects.Count > 0)
                {
                    //The ActiveUIObject has children
                    foreach (UIObject obj in ActiveUIObject.ChildrenUIObjects)
                    {
                        obj.DisplayRectangle = new Rectangle(xoffset, Camera.ViewHeight - height + 12, (int)(obj.IconRectangle.Width * obj.DisplayScale), (int)(obj.IconRectangle.Height * obj.DisplayScale));
                        spriteBatch.Draw(obj.IconTexture, obj.DisplayRectangle ,obj.IconRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                        xoffset += obj.IconRectangle.Width;
                    }
                }
                else
                {
                    //The ActiveUIObject has no children (this shouldn't happen in most cases)
                    ActiveUIObject.DisplayRectangle = new Rectangle(xoffset, Camera.ViewHeight - height, (int)(ActiveUIObject.IconRectangle.Width * ActiveUIObject.DisplayScale), (int)(ActiveUIObject.IconRectangle.Height * ActiveUIObject.DisplayScale));
                    spriteBatch.Draw(ActiveUIObject.IconTexture, new Rectangle(xoffset, Camera.ViewHeight - height, (int)(ActiveUIObject.IconRectangle.Width * ActiveUIObject.DisplayScale), (int)(ActiveUIObject.IconRectangle.Height * ActiveUIObject.DisplayScale)), ActiveUIObject.IconRectangle, Color.HotPink, 0, Vector2.Zero, SpriteEffects.None, 0);
                }
                
            }
           
        }
    }
}
