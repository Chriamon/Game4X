using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.UI
{
    public class UIObject
    {
        public Entity.IHasUIObject ParentObject;    //A reference to the object that this UIObject represents.
        public Texture2D IconTexture;               //The texture to use for this object's Icon.
        public Rectangle IconRectangle;             //The specific rectangle on the texture to use for this object's icon.
        public double DisplayScale;                 //The scale to display the icon at.
        public String Hotkey;                       //The key assigned to this UIObject
        public List<UIObject> ChildrenUIObjects;    //A reference to all the children UI objects, these children are drawn by the HUD when this object is the active object.
        public UIObject ParentUIObject;             //A reference to the parent UIObject of this object.

        public UIObject()
        {
            ChildrenUIObjects = new List<UIObject>();
        }

        /// <summary>
        /// Called when this is clicked with the left mouse button.
        /// </summary>
        public void OnLeftClick()
        {
            if (ChildrenUIObjects.Count == 0)
            {
                //this object has no children

                ParentObject.OnIconClicked(); //Run the parent object's IconClicked function

                HUD.ClearActiveUIObject(); //>>clear the HUD?
            }
            else
            {
                //this object has children, set this as the ActiveUIObject and allow the HUD to draw it's children.

                HUD.SetActiveUIObject(this);
            }

        }

        /// <summary>
        /// Called when this is clicked with the right mouse button.
        /// </summary>
        public void OnRightClick()
        {

        }

        /// <summary>
        /// Checks if this is the current "Active" UI object in the HUD
        /// </summary>
        /// <returns>True if this is the active UI, false if it is not or if there is an error.</returns>
        public bool IsActive()
        {
            if (HUD.GetActiveUIObject() == this)
            {
                return true;
            }
            
            return false;
        }



    }
}
