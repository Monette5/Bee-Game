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
    class Poop
    {
        Vector2 PoopPos = new Vector2(800, 0);
        
        Vector2 PoopVelocity = new Vector2(500, 1);
       
        Rectangle PoopRectangle;
        Texture2D[] Imgs;
        Vector2 ScreenSize;
        bool Direction = true;
        Vector2 CentreOfPoop;
        Random rnd = new Random();
      int AnimationCount = 0;

        public Poop(Vector2 poopStartPos, Vector2 poopVelocity, Vector2 _ScreenSize, Texture2D[] _img)

        {    
            
          PoopPos = poopStartPos;
            PoopVelocity = poopVelocity;
            ScreenSize = _ScreenSize;
            Imgs = _img;

            PoopRectangle = new Rectangle((int)PoopPos.X, (int)PoopPos.Y,
                                                   Imgs[AnimationCount].Width, Imgs[AnimationCount].Height);


            

            CentreOfPoop.X = PoopPos.X + 18;
            CentreOfPoop.Y = PoopPos.Y + 7;
        }
   


        //public void Update(Vector2 seagullPos, Vector2 beePos)
        public void Update(GameTime gameTime)
        {
            // Allows the game to exit

            PoopPos.X += (PoopVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds) / (int)rnd.Next(2, 5);




            AnimationCount++;
            if (AnimationCount >= Imgs.Count())
            {
                AnimationCount = 0;

            }


            //if (PoopPos.X > ScreenSize.X + 200 || PoopPos.X < 0 - 200)
            //{
            //    PoopVelocity.X *= -1;
            //    Direction = !Direction;
            //}
            if (PoopPos.X < 0)
                PoopVelocity.X *= -1;
            else if (PoopPos.X > ScreenSize.X)
            {
                PoopPos.X = 0;
                PoopVelocity.Y = 50;
                PoopVelocity.X = 1;
            }

            //if (PoopPos.Y < 0)
            //    PoopVelocity.Y *= -1;
            //else if (PoopPos.Y > ScreenSize.Y)
            //{
            //    PoopPos.Y = 0;
            //    PoopVelocity.X = 50;
            //    PoopVelocity.Y = 1;
            //}
            if (PoopPos.Y > ScreenSize.Y + 200 || PoopPos.Y < 0 - 200)
            {
                PoopVelocity.X *= -1;
                Direction = !Direction;
            }
        }




        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 DrawPos; /// draw in center of the Cursor
            if (Direction == false)
            {

                spriteBatch.Draw(Imgs[AnimationCount], PoopPos, Color.White);
            }
            else
            {
                spriteBatch.Draw(Imgs[AnimationCount], PoopPos, null, Color.White, 0F, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}
