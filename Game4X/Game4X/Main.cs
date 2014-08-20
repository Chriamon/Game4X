using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game4X
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //These are misc textures that are used "globally" for convenience
        //TODO: refactor this and put them in their own class
        public static Texture2D highlight;
        public static Texture2D select;
        public static Texture2D UnitTexture;//TEMPORARY DEBUG TESTING
        public static Texture2D CityTexture;//TEMPORARY DEBUG TESTING
        public static List<Entity.Unit> SelectedUnitList = new List<Entity.Unit>();
        SpriteFont defaultSpriteFont;//not used yet, but to be used to draw text if needed on the play field
        
        //this is the currently selected tile, eventually refactor this and put it in an appropriate class
        //TODO: Make the difference between these to clear (take care of this during the move to it's own class)
        static Vector2 selectLoc = Vector2.Zero;
        static Point selectPoint = Point.Zero;

        //TODO: take the "mapinterpreter" code and convert it for use with this (loading maps from bitmap files).
        public static TileMap theMap;
        int squaresAcross = 17;        
        int squaresDown = 37;
        int baseOffsetX = -14;
        int baseOffsetY = -14;

        //Tracks current turn, 
        //TODO: eventually move turn tracking to it's own class
        public static int Turn = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add initialization logic here instead of spread out like it currently is.
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //This will need to be redone to call each unit's loadcontent to allow each unit to load it's own texture (as the HUD does)
            UnitTexture = Content.Load<Texture2D>(@"Textures\Unit");
            CityTexture = Content.Load<Texture2D>(@"Textures\City");
            highlight = Content.Load<Texture2D>(@"Textures\TileSets\highlight");
            select = Content.Load<Texture2D>(@"Textures\TileSets\select");
            defaultSpriteFont = Content.Load<SpriteFont>(@"Textures\Courier New");
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\tileset");
            HUD.LoadContent(Content);

            //Currently randomly generates a map when a new TileMap is created, todo: load/save map from files, actual map generation
            theMap = new TileMap(Content.Load<Texture2D>(@"Textures\TileSets\mousemap"));

            //Initializes the camera, TODO: probably move this to the Camera's Initialize function
            Camera.ViewWidth = this.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = this.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((theMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((theMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // This is a default XNA class, not entirely sure what we would want to put here?
            // TODO: Unload any non ContentManager content here
        }

        //Temporary solutions to allow buttons to only fire once
        bool leftClickFlag = false;
        bool rightClickFlag = false;
        bool nFlag = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            MouseState ms = Mouse.GetState();

            //-----------------------------------------------------------------------------
            //Left Click

            if (ms.LeftButton == ButtonState.Pressed && leftClickFlag)
            {
                leftClickFlag = false;
                
                //UnitSelection behavior
                //todo: consider don't set this point unless there is something in that hex to select?
                Vector2 tempLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Point tempPoint = theMap.WorldToMapCell(new Point((int)tempLoc.X, (int)tempLoc.Y));

                if ((tempPoint.X < 0) || (tempPoint.X >= theMap.MapWidth) || (tempPoint.Y >= theMap.MapHeight) || tempPoint.Y < 0)
                    return;

                MapCell SelectedCell = theMap.Rows[tempPoint.Y].Columns[tempPoint.X];

                SelectCell(tempPoint.Y, tempPoint.X);
            }
            if(ms.LeftButton == ButtonState.Released && !leftClickFlag)
            {
                leftClickFlag = true;
               
            }

            //-----------------------------------------------------------------------------
            //Right Click

            if (ms.RightButton == ButtonState.Pressed && rightClickFlag)
            {
                Vector2 tempLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Point tempPoint = theMap.WorldToMapCell(new Point((int)tempLoc.X, (int)tempLoc.Y));

                if ((tempPoint.X < 0) || (tempPoint.X >= theMap.MapWidth) || (tempPoint.Y >= theMap.MapHeight) || tempPoint.Y < 0)
                    return;

                MapCell ClickedCell = theMap.Rows[tempPoint.Y].Columns[tempPoint.X];
                
                if (SelectedUnitList.Count > 0)
                {
                    //if any units are selected
                    
                    bool allMoved = true;

                    foreach (Entity.Unit selectedUnit in SelectedUnitList)
                    {
                        //order movement on that unit
                        if (!selectedUnit.Move(tempPoint.Y, tempPoint.X))
                        {
                            allMoved = false;
                        }
                    }

                    if (allMoved)
                    {
                        //All units successfully moved

                        SelectCell(tempPoint.Y, tempPoint.X);
                    }
                }
            }
            if (ms.RightButton == ButtonState.Released && !rightClickFlag)
            {
                rightClickFlag = true;
            }


            //-----------------------------------------------------------------------------
            //Keyboard

            KeyboardState ks = Keyboard.GetState();

            //TEMP: testing turn
            if (ks.IsKeyDown(Keys.N) && !nFlag)
            {
                nFlag = true;
                NextTurn();
            }
            if (ks.IsKeyUp(Keys.N) && nFlag)
            {
                nFlag = false;
            }

            int speed = 5;
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Move(new Vector2(-speed, 0));
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Move(new Vector2(speed, 0));
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Move(new Vector2(0, -speed));
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Move(new Vector2(0, speed));
            }

            if (ks.IsKeyDown(Keys.Escape))
            {
                ResetUnitSelection();
            }

            //-----------------------------------------------------------------------------
            //Run each selected unit's OnUpdate

            //foreach (Entity.Entity U in SelectedUnitList)
            //{
            //    U.SelectedOnUpdate();
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// Selects the cell at the specified Row/Column, If a unit is there, that units OnSelect() is called and the unit is added to the SelectionUnitList
        /// TODO: Perhaps move this somewhere else other than main? Some class that deals with interacting with the map perhaps?
        /// </summary>
        /// <param name="row">The row that the selection occured at</param>
        /// <param name="col">The column that the selection occured at</param>
        private void SelectCell(int row, int col)
        {
            if ((col < 0) || (col >= theMap.MapWidth) || (row >= theMap.MapHeight) || row < 0)
                return;

            MapCell SelectedCell = theMap.Rows[row].Columns[col];

            if (SelectedCell.EntityList.Count >= 1)
            {
                ResetUnitSelection();
                selectPoint = new Point(col, row);
                foreach (Entity.Entity E in SelectedCell.EntityList)
                {
                    Entity.Unit U = E as Entity.Unit;
                    if (U == null)
                        continue;
                    U.OnSelect();
                    SelectedUnitList.Add(U);
                }
            }
            else
            {
                //ResetUnitSelection();
            }
        }

        /// <summary>
        /// Finishes the current turn 
        /// TODO: Eventually figure out more long-term architecture for turns, perhaps a "Turn" could be a class, and it could store information about everything, allowing for replays
        /// </summary>
        private void NextTurn()
        {
            for (int y = 0; y < theMap.MapHeight; y++)
            {
                int rowOffset = 0;
                if ((y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < theMap.MapWidth; x++)
                {
                    foreach (Entity.Entity U in theMap.Rows[y].Columns[x].EntityList)
                    {
                        if (!U.OnTurnTick())
                        {
                            return;
                        }
                    }
                }
            }
            Turn++;
        }

        /// <summary>
        /// Debug function to force the next turn
        /// </summary>
        private void ForceNextTurn()
        {
            Turn++;
        }

        /// <summary>
        /// Resets the "selection" of everything
        /// </summary>
        private static void ResetUnitSelection()
        {
            selectLoc = Vector2.Zero;
            selectPoint = Point.Zero;
            SelectedUnitList = new List<Entity.Unit>();
        }

        /// <summary>
        /// TODO: Refactor this loop to just show the "high level" logic, have each object take care of drawing itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            //Draw the hex grid
            DrawHexGrid(spriteBatch);

            //Draw the highlight on mouseover
            //TODO: eventually create different logic for this, 2 modes, 
            //      Mode 1) Something is selected - show the appropriate highlight, i.e. building being placed->show the building outline
            //              -Maybe take care of this the same way that selection is eventually taken care of, allow the selected entity to draw it's highlight?
            //      Mode 2) Nothing selected - default highlight, just highlight the mouse'd over hex
            DrawHighlight(spriteBatch);

            //Draw Selected Tile
            //TODO: possibly move this to the units with this as the generic method, allowing units to have nonstandard selection hex layouts
            if (selectPoint != Point.Zero)
            {
                Vector2 DistanceToCenterHex = new Vector2((select.Width - 33) / 2, (select.Height - 27) / 2);

                int selectrowOffset = 0;

                if ((selectPoint.Y) % 2 == 1)
                {
                    selectrowOffset = Tile.OddRowXOffset;
                }

                //Draw the selection mask
                spriteBatch.Draw(
                    select,
                    Camera.WorldToScreen(new Vector2((selectPoint.X * Tile.TileStepX) + selectrowOffset - DistanceToCenterHex.X, 
                                                     (selectPoint.Y) * Tile.TileStepY - DistanceToCenterHex.Y)),
                    new Rectangle(0, 0, select.Width, select.Height),
                    Color.Black * .3f,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0.0f);
            }

            //Draw the HUD
            HUD.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the hex grid
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawHexGrid(SpriteBatch spriteBatch)
        {
            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = x + firstX;
                    int mapy = y + firstY;

                    if ((mapx >= theMap.MapWidth) || (mapy >= theMap.MapHeight))
                        continue;

                    foreach (int tileID in theMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(new Vector2((mapx * Tile.TileStepX) + rowOffset, 
                                                              mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            1.0f);
                    }
                    theMap.Rows[mapy].Columns[mapx].DrawUnits(spriteBatch, mapx, mapy);
                }
            }
        }

        /// <summary>
        /// Draws the highlight on mouseover. Works for any highlight textures that are "evenly clustered" i.e. 1 hex,  7 hex cluster, 19 hex cluster
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawHighlight(SpriteBatch spriteBatch)
        {
            Vector2 highlightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point highlightPoint = theMap.WorldToMapCell(new Point((int)highlightLoc.X, (int)highlightLoc.Y));
            Vector2 DistanceToCenterHex = new Vector2((highlight.Width - 33) / 2, (highlight.Height - 27) / 2);

            int highlightrowOffset = 0;

            if ((highlightPoint.Y) % 2 == 1)
            {
                highlightrowOffset = Tile.OddRowXOffset;
            }

            spriteBatch.Draw(
                highlight,
                Camera.WorldToScreen(new Vector2((highlightPoint.X * Tile.TileStepX) + highlightrowOffset - DistanceToCenterHex.X, 
                                                 (highlightPoint.Y) * Tile.TileStepY - DistanceToCenterHex.Y)),
                new Rectangle(0, 0, highlight.Width, highlight.Height), 
                Color.White * .3f, 
                0.0f, 
                Vector2.Zero, 
                1.0f, 
                SpriteEffects.None, 
                0.0f);
        }
    }

}
