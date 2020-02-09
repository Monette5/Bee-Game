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
    class flyingBird
    {
        // seagull sprite
        Random rnd = new Random();
        // seagull location
        Vector2 seagullPos = new Vector2(800, 0);
        //Seagull Speed
        Vector2 seagullVelocity = new Vector2(500, 1);
        Rectangle seagullRectangle;
        Texture2D[] Imgs;
        Vector2 ScreenSize;
        bool Direction = true;
        //true = right
        //false = left;
       
        int AnimationCount = 0;

        public flyingBird(Vector2 _seagullStartPos, Vector2 _seagullVelocity, Vector2 _ScreenSize, Texture2D[] _img)
        {
            seagullPos = _seagullStartPos;
            seagullVelocity = _seagullVelocity;
            ScreenSize = _ScreenSize;
            Imgs = _img;

            seagullRectangle = new Rectangle((int)seagullPos.X, (int)seagullPos.Y,
                                                   Imgs[AnimationCount].Width, Imgs[AnimationCount].Height);

            
          
        }


        //public void Update(Vector2 seagullPos, Vector2 beePos)
        public void Update(GameTime gameTime)
        {
            // Allows the game to exit
            
            seagullPos.X +=  ( seagullVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds) / (int) rnd.Next(2,5);


            

                AnimationCount++;
                if (AnimationCount >= Imgs.Count())
                {
                    AnimationCount = 0;

                }


                if (seagullPos.X > ScreenSize.X + 200 || seagullPos.X < 0 - 200)
                {
                    seagullVelocity.X *= -1;
                    Direction = !Direction;
                }


                if (seagullPos.Y < 0)
                    seagullVelocity.Y *= -1;
                else if (seagullPos.Y > ScreenSize.Y)
                {
                    seagullPos.Y = 0;
                    seagullVelocity.X = 50;
                    seagullVelocity.Y = 1;
                }

            }
        



        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 DrawPos; /// draw in center of the Cursor
            if (Direction == false)
            {

                spriteBatch.Draw(Imgs[AnimationCount], seagullPos, Color.White);
            }
            else
            {
                spriteBatch.Draw(Imgs[AnimationCount], seagullPos, null, Color.White, 0F, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}


