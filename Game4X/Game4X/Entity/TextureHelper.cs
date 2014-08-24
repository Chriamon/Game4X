using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game4X.Entity
{
    public static class TextureHelper
    {
        static List<Texture2D> TextureList = new List<Texture2D>();

        public enum EntityID : int
        {
            Unknown,
            Entity,
            City,
            Building,
            Unit,
        };

        public static void LoadTextures(ContentManager Content)
        {
            TextureList.Add(Content.Load<Texture2D>(@"Textures\Test"));
            TextureList.Add(Content.Load<Texture2D>(@"Textures\Entity"));
            TextureList.Add(Content.Load<Texture2D>(@"Textures\City"));
            TextureList.Add(Content.Load<Texture2D>(@"Textures\Building"));
            TextureList.Add(Content.Load<Texture2D>(@"Textures\Unit"));
        }

        public static Texture2D GetTexture(int EntityID)
        {
            return TextureList.ElementAt(EntityID);
        }
    }
}
