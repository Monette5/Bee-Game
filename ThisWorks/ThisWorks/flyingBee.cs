using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Diagnostics;

namespace ThisWorks
{
    class flyingBee
    {
        
        Vector2 beePos;
        Vector2 BeeSpeed;
        public int lifeForce = 10;
        public int Pollen = 0;
        Rectangle beeRectangle;
        Texture2D[] Imgs;
        Vector2 ScreenSize;
        int AnimationCount = 0;
        Vector2 OldBeePos;

        public Boolean BeeDead = false;

        public flyingBee(Vector2 _StartPos, Vector2 _Speed, Vector2 _ScreenSize, Texture2D[] _img)
        {
            beePos = _StartPos;
            BeeSpeed = _Speed;
            ScreenSize = _ScreenSize;
            Imgs = _img;
            beeRectangle = new Rectangle((int)beePos.X, (int)beePos.Y,
                        Imgs[AnimationCount].Width, Imgs[AnimationCount].Height);


        }

        public void update(Vector2 mousePos, Vector2 seagullPos)
        {

            AnimationCount++;
            if (AnimationCount >= Imgs.Count())
            {
                AnimationCount = 0;
            }


            if (beePos.X < 0)
                beePos.X = 0;
            else if (beePos.X > ScreenSize.X)
            {
                beePos.X = ScreenSize.X;
                BeeSpeed.X *= -1;
            }



            beePos = mousePos;
            OldBeePos = beePos;
            beePos = mousePos;
            //beePos.update(mousePos, seagullPos);

            //beeRectangle = new Rectangle((int)beePos.X, (int)beePos.Y,
            //            Imgs[AnimationCount].Width, Imgs[AnimationCount].Height);


            //Controls the speed of the movement of the bee.

            if (mousePos.X > ScreenSize.X)
            {
                beePos = OldBeePos;
            }

            if (mousePos.X < 1)
            {
                beePos.X = beePos.X - beePos.X / 2;
            }
            if (mousePos.Y > 500 - beeRectangle.Height)
            {
                beePos = OldBeePos;
            }

            if (mousePos.Y < 0)
            {
                beePos.Y = beePos.Y - beePos.Y / 2 - beePos.Y / 2;
            }

            if (Vector2.Distance(beePos, seagullPos) < 50)
            {
                lifeForce = lifeForce - 10;
            }


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 DrawPos; /// draw in center of the Cursor

            spriteBatch.Draw(Imgs[AnimationCount], CentreofBee(), Color.White);
        }

        public Vector2 CentreofBee()
        {
            Vector2 CentredBeePos = beePos;
            CentredBeePos.X -= Imgs[0].Width / 2;
            CentredBeePos.Y -= Imgs[0].Height / 2;
            return CentredBeePos;
        }








    }
          
}
