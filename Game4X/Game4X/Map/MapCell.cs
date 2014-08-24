using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game4X
{
    public class MapCell
    {
        public List<int> BaseTiles = new List<int>();
        public List<Entity.Entity> EntityList = new List<Entity.Entity>();

        int mapy
        { get; set; }

        int mapx
        { get; set; }

        public void UpdateCoords(int x, int y)
        {
            mapy = y;
            mapx = x;
        }

        //TODO: Implement pathing: find shortest path, ensure it's clear, and ensure unit has enough moevement to make the full move
        public bool UnitCanMoveHere(Entity.Unit u)
        {
            bool clear = (this.EntityList.Count > 0) ? false : true;//TODO, instead of false or true, check collision layer vs each of those units

            if ((BaseTiles[0] == 1 || BaseTiles[0] == 2 || BaseTiles[0] == 3) && clear)
            {
                return true;
            }
            else
                return false;
        }

        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        public void DrawUnits(SpriteBatch spriteBatch, int mapx, int mapy)
        {
            UpdateCoords(mapx, mapy);
            DrawUnits(spriteBatch);
        }

        public void DrawUnits(SpriteBatch spriteBatch)
        {
            foreach (Entity.Entity currEntity in EntityList)
            {
                currEntity.DrawEntity(spriteBatch, mapx, mapy);
            }
        }

        public static Boolean IsGroundPathable(int BaseTile)
        {
            return true;
            //return BaseTiles[0].IsGroundPathable();
        }

        public void RemoveUnit(Entity.Entity u)
        {
            EntityList.Remove(u);
        }

        public void AddUnit(Entity.Entity u)
        {
            EntityList.Add(u);
        }

        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }

        public MapCell(int tileID)
        {
            TileID = tileID;
        }
    }
}
