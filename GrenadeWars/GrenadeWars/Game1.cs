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

namespace GrenadeWars {

    public class Game1 : Microsoft.Xna.Framework.Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D grenadierTexture;
        Texture2D grenadeTexture;

        List<Rectangle> wallList = new List<Rectangle> { };
        Rectangle wall = new Rectangle(1000, 1000, 1, 1);
        

        Player[] players;

        int numberOfPlayers = 4;

        int screenWidth;
        int screenHeight;

        int maxX, minX, maxY, minY;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        //    LOAD CONTENT    //
        protected override void LoadContent() {
            device = graphics.GraphicsDevice;

            spriteBatch = new SpriteBatch(device);

            backgroundTexture = Content.Load<Texture2D>("Background");
            grenadierTexture = Content.Load<Texture2D>("Grenadier");
            //tankTexture = Content.Load<Texture2D>("MulticolorTanks");
            grenadeTexture = Content.Load<Texture2D>("Retro-Coin-icon");


            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            maxX = screenWidth;
            maxY = screenHeight;
            minX = 0;
            minY = 0;

            SetUpPlayers();
            wallList.Add(wall);
        }


        protected override void Initialize() {

            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "GrenadeWars";

            base.Initialize();
        }

        protected override void UnloadContent() {
        }


        protected override void Update(GameTime gameTime) {
            ProcessKeyboard();
            base.Update(gameTime);

            foreach(Rectangle rect in wallList){
                if(players[0].playerRectange.Intersects(rect)){
                    Console.WriteLine("COLLISION");
                }
            }
            


            
        }

        public void ProcessKeyboard() {
            KeyboardState boardState = Keyboard.GetState();
            
           //PFEILE// 
            if (boardState.IsKeyDown(Keys.Left)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.X > 0) {
                    players[0].Position.X -= 2;
                    players[0].playerRectange.X = (int) players[0].Position.X;
                }
            }
            if (boardState.IsKeyDown(Keys.Right)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.X < maxX - grenadierTexture.Width * players[0].playerScaling) {  // PLAYER SIZE hinzufügen
                    players[0].Position.X += 2;
                    players[0].playerRectange.X = (int) players[0].Position.X;
                }
            }
            if (boardState.IsKeyDown(Keys.Up)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.Y > grenadierTexture.Height*players[0].playerScaling) {
                    players[0].Position.Y -= 2;
                    players[0].playerRectange.Y = (int)players[0].Position.Y;
                }
            }
            if (boardState.IsKeyDown(Keys.Down)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.Y < maxY) {
                    players[0].Position.Y += 2;
                    players[0].playerRectange.Y = (int)players[0].Position.Y;
                }
            }
            if (boardState.IsKeyDown(Keys.RightControl)) {
                Console.WriteLine(players[0].Position);

                int x1 = wallList[wallList.Count -1 ].X;
                int y1 = wallList[wallList.Count -1].Y;
                int x2 = (int)players[2].Position.X;
                int y2 = (int)players[2].Position.Y;

                if(((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2)) > 100){
                    Rectangle wall = new Rectangle((int)players[0].Position.X, (int)players[0].Position.Y, 10, 10);
                    wallList.Add(wall);
                }
                

            }

            //WASD//
            if (boardState.IsKeyDown(Keys.A)) {
                Console.WriteLine(players[1].Position);
                if (players[1].Position.X > 0) {
                    players[1].Position.X -= 2;
                    players[1].playerRectange.X = (int)players[1].Position.X;
                }
            }
            if (boardState.IsKeyDown(Keys.D)) {
                Console.WriteLine(players[1].Position);
                if (players[1].Position.X < maxX - grenadierTexture.Width * players[1].playerScaling) {  // PLAYER SIZE hinzufügen
                    players[1].Position.X += 2;
                    players[1].playerRectange.X = (int)players[1].Position.X;
                }
            }
            if (boardState.IsKeyDown(Keys.W)) {
                Console.WriteLine(players[1].Position);
                if (players[1].Position.Y > grenadierTexture.Height * players[1].playerScaling) {
                    players[1].Position.Y -= 2;
                    players[1].playerRectange.Y = (int)players[1].Position.Y;
                }
            }
            if (boardState.IsKeyDown(Keys.S)) {
                Console.WriteLine(players[1].Position);
                if (players[1].Position.Y < maxY) {
                    players[1].Position.Y += 2;
                    players[1].playerRectange.Y = (int)players[1].Position.Y;
                }
            }
            if (boardState.IsKeyDown(Keys.LeftControl)) {
                Console.WriteLine(players[1].Position);
                Rectangle wall = new Rectangle((int)players[1].Position.X, (int)players[1].Position.Y, 10, 10);
                wallList.Add(wall);

            }
        }

        // -------------------------------------------------------- //

        
        protected override void Draw(GameTime gameTime) {

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void DrawScenery() {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }

        public void DrawPlayers() {
            foreach (Player player in players) {
                if (player.IsAlive) {
                    spriteBatch.Draw(grenadierTexture,
                        player.Position,
                        null, //part of the immage
                        player.Color,//color modulation
                        player.Angle,//rotation
                        new Vector2(0, grenadierTexture.Height),//positioning bottom left
                        player.playerScaling, //scaling
                        SpriteEffects.None, //mirroring imgage
                        0); //layer of the image
                }
                if (wallList != null) {
                        foreach (Rectangle rect in wallList) {
                            spriteBatch.Draw(grenadeTexture, rect, Color.White);
                        }
                }
            }
        }




        private void SetUpPlayers() {

            Color[] playerColors = new Color[10];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Green;
            playerColors[2] = Color.Blue;
            playerColors[3] = Color.Purple;
            playerColors[4] = Color.Orange;
            playerColors[5] = Color.Indigo;
            playerColors[6] = Color.Yellow;
            playerColors[7] = Color.SaddleBrown;
            playerColors[8] = Color.Tomato;
            playerColors[9] = Color.Turquoise;

            players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++) {

                players[i] = new Player(true, playerColors[i], MathHelper.ToRadians(0), 100);
            }

            players[0].Position = new Vector2(100, 100);
            players[1].Position = new Vector2(400, 100);
            players[0].playerRectange = new Rectangle(grenadierTexture.Height, grenadierTexture.Width, 100, 100);
            players[1].playerRectange = new Rectangle(grenadierTexture.Height, grenadierTexture.Width, 400, 100);

        }
    }
}
