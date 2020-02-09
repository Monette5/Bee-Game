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
    class Flowers
    {

        Vector2 Pos;
        Texture2D img;
        int PollenCount;
        Vector2 CentreOfFlower;
        


        public Flowers(Rectangle FlowerArea, Texture2D _img, int _PollenCount, Random _RND)
        {
            PollenCount = _PollenCount;
            img = _img;

            Pos.X = _RND.Next(FlowerArea.X, FlowerArea.Right);
            Pos.Y = _RND.Next(FlowerArea.Y, FlowerArea.Bottom);

            CentreOfFlower.X = Pos.X + 18;
            CentreOfFlower.Y = Pos.Y + 7;
 
        }


        public int update(Vector2 BeePos)
        {
            int ReturnedPollen = 0;
            if (Vector2.Distance(BeePos, CentreOfFlower) < 10)
            {
                if (PollenCount > 0)
                {
                    PollenCount--; //remove 1;
                    ReturnedPollen = 1;
                }
            }
            return ReturnedPollen;
           
        }


        public void draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(img, Pos, Color.White);
        }
    }
}
