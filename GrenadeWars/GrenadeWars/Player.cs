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
        public float Health;
        public float playerScaling = 1.5f;
        public float speed = 5;
        public List<Grenade> grenadeList = new List<Grenade> { };

        public Rectangle playerRectange;

        public Player( bool isAlive, Color color, float angle, float health) {
            IsAlive = isAlive;
            Color = color;
            Angle = angle;
            Health = health;
        }

        internal void Uppdate(GameTime gameTime) {
            //Check Collision und Bulletshoot
        }

        public void addGrenade(Grenade grenade) {
            grenadeList.Add(grenade);
        }
        public List<Grenade> getGrenadeList() {
            return grenadeList;
        }
    }

}
