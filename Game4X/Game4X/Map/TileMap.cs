using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game4X
{
    public class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }

    public class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 50;
        public int MapHeight = 50;
        private Texture2D mouseMap;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseMap">The mousemap used for figuring out which hex the mouse is in</param>
        public TileMap(Texture2D mouseMap)
        {
            Random RNGsus = new Random();
            this.mouseMap = mouseMap;
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    int f = RNGsus.Next(4);
                    MapCell n = new MapCell(f);
                    if (f == 2)
                    {
                        if(RNGsus.Next(2) == 1)
                            n.AddBaseTile(5);
                    }
                    thisRow.Columns.Add(n);
                }
                Rows.Add(thisRow);
            }
            //TEMP MAP
            // Create Sample Map Data
            Rows[5].Columns[5].TileID = 2;
            Entity.Unit u = new Entity.Unit(Entity.TextureHelper.GetTexture((int)Entity.TextureHelper.EntityID.Unit));
            u.owner = 1;
            Rows[5].Columns[5].EntityList.Add(u);
            Rows[2].Columns[3].TileID = 3;
            Rows[2].Columns[3].EntityList.Add(new Entity.Unit(Entity.TextureHelper.GetTexture((int)Entity.TextureHelper.EntityID.Unit)));

            Rows[7].Columns[3].TileID = 2;
            Rows[7].Columns[3].EntityList.Add(new Entity.City(Entity.TextureHelper.GetTexture((int)Entity.TextureHelper.EntityID.City)));
            //Rows[0].Columns[4].TileID = 3;
            //Rows[0].Columns[5].TileID = 1;
            //Rows[0].Columns[6].TileID = 1;
            //Rows[0].Columns[7].TileID = 1;

            //Rows[1].Columns[3].TileID = 3;
            //Rows[1].Columns[4].TileID = 1;
            //Rows[1].Columns[5].TileID = 1;
            //Rows[1].Columns[6].TileID = 1;
            //Rows[1].Columns[7].TileID = 1;

            //Rows[2].Columns[2].TileID = 3;
            //Rows[2].Columns[3].TileID = 1;
            //Rows[2].Columns[4].TileID = 1;
            //Rows[2].Columns[5].TileID = 1;
            //Rows[2].Columns[6].TileID = 1;
            //Rows[2].Columns[7].TileID = 1;

            //Rows[3].Columns[2].TileID = 3;
            //Rows[3].Columns[3].TileID = 1;
            //Rows[3].Columns[4].TileID = 1;
            //Rows[3].Columns[5].TileID = 2;
            //Rows[3].Columns[6].TileID = 2;
            //Rows[3].Columns[7].TileID = 2;

            //Rows[4].Columns[2].TileID = 3;
            //Rows[4].Columns[3].TileID = 1;
            //Rows[4].Columns[4].TileID = 1;
            //Rows[4].Columns[5].TileID = 2;
            //Rows[4].Columns[6].TileID = 2;
            //Rows[4].Columns[7].TileID = 2;

            //Rows[5].Columns[2].TileID = 3;
            //Rows[5].Columns[3].TileID = 1;
            //Rows[5].Columns[4].TileID = 1;
            //Rows[5].Columns[5].TileID = 2;
            //Rows[5].Columns[6].TileID = 2;
            //Rows[5].Columns[7].TileID = 2;

            //Rows[6].Columns[3].TileID = 3;
            //Rows[6].Columns[4].TileID = 3;
            //Rows[6].Columns[5].TileID = 1;
            //Rows[6].Columns[6].TileID = 1;
            //Rows[6].Columns[7].TileID = 1;

            //Rows[7].Columns[3].TileID = 3;
            //Rows[7].Columns[4].TileID = 1;
            //Rows[7].Columns[5].TileID = 1;
            //Rows[7].Columns[6].TileID = 1;
            //Rows[7].Columns[7].TileID = 1;

            //Rows[8].Columns[2].TileID = 3;
            //Rows[8].Columns[3].TileID = 1;
            //Rows[8].Columns[4].TileID = 1;
            //Rows[8].Columns[5].TileID = 1;
            //Rows[8].Columns[6].TileID = 1;
            //Rows[8].Columns[7].TileID = 1;

            //Rows[9].Columns[2].TileID = 3;
            //Rows[9].Columns[3].TileID = 1;
            //Rows[9].Columns[4].TileID = 1;
            //Rows[9].Columns[5].TileID = 2;
            //Rows[9].Columns[6].TileID = 2;
            //Rows[9].Columns[7].TileID = 2;

            //Rows[10].Columns[2].TileID = 3;
            //Rows[10].Columns[3].TileID = 1;
            //Rows[10].Columns[4].TileID = 1;
            //Rows[10].Columns[5].TileID = 2;
            //Rows[10].Columns[6].TileID = 2;
            //Rows[10].Columns[7].TileID = 2;

            //Rows[11].Columns[2].TileID = 3;
            //Rows[11].Columns[3].TileID = 1;
            //Rows[11].Columns[4].TileID = 1;
            //Rows[11].Columns[5].TileID = 2;
            //Rows[11].Columns[6].TileID = 2;
            //Rows[11].Columns[7].TileID = 2;

            ////Rows[3].Columns[5].AddBaseTile(30);
            ////Rows[4].Columns[5].AddBaseTile(27);
            ////Rows[5].Columns[5].AddBaseTile(28);

            ////Rows[3].Columns[6].AddBaseTile(25);
            ////Rows[5].Columns[6].AddBaseTile(24);

            ////Rows[3].Columns[7].AddBaseTile(31);
            ////Rows[4].Columns[7].AddBaseTile(26);
            ////Rows[5].Columns[7].AddBaseTile(29);

            ////Rows[4].Columns[6].AddBaseTile(104);
            // End Create Sample Map Data
        }
        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }

        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / mouseMap.Width),
               ((int)(worldPoint.Y / mouseMap.Height)) * 2
               );

            int localPointX = worldPoint.X % mouseMap.Width;
            int localPointY = worldPoint.Y % mouseMap.Height;

            int dy = 0;
            int dx = 0;
            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, mouseMap.Width, mouseMap.Height).Contains(localPointX, localPointY))//if the mouse is on the screen
            {
                mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dy = -1;
                    dx = -1;
                    localPointX = localPointX + ((mouseMap.Width - 1) / 2);
                    localPointY = localPointY + ((mouseMap.Height - 1) / 2);
                }

                if (myUint[0] == 0xFF00FF00) // Green
                {
                    localPointX = localPointX + ((mouseMap.Width - 1) / 2);
                    dy = +1;
                    dx = -1;
                    localPointY = localPointY - ((mouseMap.Height - 1) / 2);
                }

                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - ((mouseMap.Width - 1) / 2);
                    localPointY = localPointY + ((mouseMap.Height - 1) / 2);
                }

                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;

                    localPointX = localPointX - ((mouseMap.Width - 1) / 2);
                    localPointY = localPointY - ((mouseMap.Height - 1) / 2);
                }
            }

            mapCell.Y += dy;
            mapCell.X += dx;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }
    }
}
