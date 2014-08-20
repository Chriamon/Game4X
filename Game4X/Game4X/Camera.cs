﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game4X
{
    static class Camera
    {
        public static int ViewWidth { get; set; }
        public static int ViewHeight { get; set; }
        public static int WorldWidth { get; set; }
        public static int WorldHeight { get; set; }

        public static Vector2 DisplayOffset { get; set; }
        private static Vector2 _location = Vector2.Zero;
        public static Vector2 Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = new Vector2(
                    MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                    MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }
        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        public static void Move(Vector2 offset)
        {
            Location += offset;
        }
    }
}
