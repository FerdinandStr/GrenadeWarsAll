using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrenadeWars {
    public class Player {

        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public int DirectionX = 0; //+x=1 -x=-1 0=0
        public int DirectionY = 1;
        public float Health;
        public float playerScaling = 1.5f;
        public float speed = 5;

        public Rectangle testrect = new Rectangle ( 200, 200, 10, 10);

        public Rectangle playerRectange;
        public List<Rectangle> wallList = new List<Rectangle> { };

        public Player( bool isAlive, Color color, float angle, float health) {
            IsAlive = isAlive;
            Color = color;
            Angle = angle;
            Health = health;
        }

        internal void Uppdate(GameTime gameTime) {
            //Check Collision und Bulletshoot
        }
    }

}
