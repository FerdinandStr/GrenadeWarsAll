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
        Rectangle rect;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D grenadierTexture;
        Texture2D grenadeTexture;

        //List<Rectangle> wallList = new List<Rectangle> { };        

        Player[] players;

        int numberOfPlayers = 2;

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
            

            foreach(Rectangle rect in players[0].wallList){
                if(players[0].playerRectange.Intersects(rect)){
                    Console.WriteLine("COLLISION");
                }
            }

            //players[0].Position.X += 1;

            foreach (Player player in players) {
                int moveX = (1 * player.DirectionX);
                int moveY = (1 * player.DirectionY);
                player.Position.X += moveX;
                player.Position.Y += moveY;

                rect = player.wallList[player.wallList.Count - 1];
                if (moveY == -1) {
                    rect.Height += 1;
                    rect.Y += -1;
                }
                else if (moveY == 1) {
                    rect.Height += 1;
                }
                if (moveX == -1) {
                    rect.Width += 1;
                    rect.X += -1;
                }
                else if (moveX == 1) {
                    rect.Width += 1;
                }
                player.wallList[player.wallList.Count - 1]= rect;
            }


            base.Update(gameTime);
            
        }

        public void ProcessKeyboard() {
            KeyboardState boardState = Keyboard.GetState();
            
           //PFEILE// 
            if (boardState.IsKeyDown(Keys.Left)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.X > 0) {
                    players[0].DirectionX = -1;
                    players[0].DirectionY = 0;
                    players[0].wallList.Add(new Rectangle((int)players[0].Position.X, (int)players[0].Position.Y, 10, 10));
                }
            }
            if (boardState.IsKeyDown(Keys.Right)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.X < maxX - grenadierTexture.Width * players[0].playerScaling) {  // PLAYER SIZE hinzufügen
                    players[0].DirectionX = 1;
                    players[0].DirectionY = 0;
                    players[0].wallList.Add(new Rectangle((int)players[0].Position.X, (int)players[0].Position.Y, 10, 10));
                }
            }
            if (boardState.IsKeyDown(Keys.Up)) {
                Console.WriteLine(players[0].Position);
                if (players[0].Position.X < maxX - grenadierTexture.Width * players[0].playerScaling) {  // PLAYER SIZE hinzufügen
                    players[0].DirectionX = 0;
                    players[0].DirectionY = -1;
                    players[0].wallList.Add(new Rectangle((int)players[0].Position.X, (int)players[0].Position.Y, 10, 10));
                }
            }
            if (boardState.IsKeyDown(Keys.Down)) {
                Console.WriteLine(players[0].Position);
                if (players[1].Position.Y < maxY) {
                    players[0].DirectionX = 0;
                    players[0].DirectionY = 1;
                    players[0].wallList.Add(new Rectangle((int)players[0].Position.X, (int)players[0].Position.Y, 10, 10));
                }
            }
            if (boardState.IsKeyDown(Keys.RightControl)) {
                Console.WriteLine(players[0].Position);

                
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
                        null, //part of the image
                        player.Color,//color modulation
                        player.Angle,//rotation
                        new Vector2(0, grenadierTexture.Height),//positioning bottom left
                        player.playerScaling, //scaling
                        SpriteEffects.None, //mirroring imgage
                        0); //layer of the image
                }
                if (player.wallList != null) {
                        foreach (Rectangle rect in player.wallList) {
                            spriteBatch.Draw(grenadeTexture, rect, Color.White);
                            
                        }
                }
                //SpriteFont font;
                //font = Content.Load<SpriteFont>("Segoe");
                //spriteBatch.DrawString(font,rect.X.ToString(),new Vector2(10,200),Color.HotPink);
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

            players[0].wallList.Add(new Rectangle(100, 100, 10, 10));
            players[1].wallList.Add(new Rectangle(400, 100, 10, 10));

        }
    }
}
