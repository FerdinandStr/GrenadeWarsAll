using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GrenadeWars {
    public class Grenade {
        public Vector2 Position;
        public float grenadeScaling = 1f;
        public float Angle;
        public float Range;
        public float Radius;

        public Grenade(Vector2 position, float angle, float range){
            Position = position;
            Angle = angle;
            Range = range;
            Radius = 5f;
        }
        public Grenade(Vector2 position, float angle, float scaling, float range, float radius){
            Position = position;
            Angle = angle;
            grenadeScaling = scaling;
            Range = range;
            Radius = radius;
        }


        internal void Uppdate(GameTime gameTime) {
            //Check Collision und Bulletshoot
        }
    }
}
