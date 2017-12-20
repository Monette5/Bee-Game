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

namespace NewThisWorks
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Adds Sound
        Stopwatch TimeSinceStart = new Stopwatch();
        //SoundEffect flySound;
        //Texture definitions.
        //Daisy image
        Texture2D Daisy;
        Vector2 DaisyPos = new Vector2(100, 400);
        // Daisy Location.
        Vector2 Daisy1Pos = new Vector2(200, 400);
        Vector2 Daisy2Pos = new Vector2(300, 400);
        Vector2 Daisy3Pos = new Vector2(400, 400);
        public Boolean beeDead = false;
        Vector2 lifeFontPos = new Vector2(600, 20);
        Vector2 pollenFontPos = new Vector2(50, 20);
        //Background information.
        Texture2D imageTexture;
        Rectangle imageRect;
        // Bee image
        Texture2D[] bee = new Texture2D[8];
        const float DEFAULT_X_SPEED = 300;
        const float DEFAULT_Y_SPEED = 150;
        
        Vector2 beeSpeed = new Vector2(DEFAULT_X_SPEED, DEFAULT_Y_SPEED); 
        int lifeForce = 100;
        int Pollen = 0;
        //Rectangle beeRectangle;
        //Texture2D seagullPoop;
        //Vector2 seagullPoop = new Vector2(700, 20);
        Vector2 ScreenSize = new Vector2(800, 500);
        bool Direction = true;
        //Initial Bee position
        const float INIT_X_POS = 80;
        const float INIT_Y_POS = 0;
        
        // Bee's motion
        int KeyFrame;
       
        // seagull sprite
        Texture2D[] seagull = new Texture2D[8];
        
        // seagull location
        Vector2 seagullPos = new Vector2(800, 0);
        //Seagull Speed
        Vector2 seagullVelocity = new Vector2(500, 1);
        int KeyFrame1;
        SpriteFont lifeFont;
        SpriteFont pollenFont;
        
        Vector2 beePos = new Vector2(10,10);

   

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        void Reset()
        {
            beeSpeed.X = DEFAULT_X_SPEED;
            beeSpeed.Y = DEFAULT_Y_SPEED;

            beePos.Y = 0;

        }


        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //View the mouse pointer.
            this.IsMouseVisible = true;
            //Set seagull velocity.
            //seagullVelocity.X = 400;
            //seagullVelocity.Y = 0;
            base.Initialize();
        }

        /// </summary>
        protected override void LoadContent()
        {

            imageRect = new Rectangle(0, 0, 800, 500);
            // Make sure base.Initialize() is called before this or seagullSprite will be null

            //Speed = DEFAULT_X_SPEED;
            //seagullPos.X = (GraphicsDevice.Viewport.Width) / 3;
            //seagullPos.Y = (GraphicsDevice.Viewport.Height) / 8 ;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //flySound = Content.Load<SoundEffect>("fly");
            imageTexture = Content.Load<Texture2D>("Game Over");
            Daisy = Content.Load<Texture2D>("Daisy");

            //seagull = Content.Load<Texture2D>("seagull");
            //Loads the seagull pictures
            seagull[0] = Content.Load<Texture2D>("seagull");
            seagull[1] = Content.Load<Texture2D>("seagull1");
            seagull[2] = Content.Load<Texture2D>("seagull2");
            seagull[3] = Content.Load<Texture2D>("seagull3");
            seagull[4] = Content.Load<Texture2D>("seagull4");
            seagull[5] = Content.Load<Texture2D>("seagull5");
            seagull[6] = Content.Load<Texture2D>("seagull6");
            seagull[7] = Content.Load<Texture2D>("seagull7");
            //Loads the bee pictures.
            bee[0] = Content.Load<Texture2D>("bee");
            bee[1] = Content.Load<Texture2D>("bee1");
            bee[2] = Content.Load<Texture2D>("bee2");
            bee[3] = Content.Load<Texture2D>("bee3");
            bee[4] = Content.Load<Texture2D>("bee4");
            bee[5] = Content.Load<Texture2D>("bee5");
            bee[6] = Content.Load<Texture2D>("bee6");
            bee[7] = Content.Load<Texture2D>("bee7");
            // TODO: use this.Content to load your game content here


            //Bee = new flyingBee(new Vector2(INIT_X_POS, INIT_Y_POS), new Vector2(DEFAULT_X_SPEED, DEFAULT_Y_SPEED), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), bee);

            lifeFont = Content.Load<SpriteFont>("lifeFont");
            pollenFont = Content.Load<SpriteFont>("pollenFont");


        }


        /// UnloadContent will be called once per game and is the place to unload
        /// all content.

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            seagullPos += seagullVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyFrame1++;

            if (KeyFrame1 > 7)
            {
                KeyFrame1 = 0;
            // Sets maximum boundaries.
            int maxX = GraphicsDevice.Viewport.Width;
            int maxY = GraphicsDevice.Viewport.Height;

                //Controls the speed of the movement.

              
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
            //Returns the program to frame 0 when it reaches frame 7.    
            KeyFrame++;

            if (KeyFrame > 7)
            {
                KeyFrame = 0;
            
                 
                //Moves the bee around the screen by moving the mouse.
                MouseState MouseState = Mouse.GetState();
                Vector2 mousePos = new Vector2(MouseState.X - (bee[0].Width), MouseState.Y - (bee[0].Height));
                beePos = mousePos;
               
                

                //Controls the speed of the movement of the bee.

                if (mousePos.X > ScreenSize.X)
                {
                    beePos = beePos/2;
                }

                if (mousePos.X < 1)
                {
                    beePos.X = beePos.X - beePos.X / 2;
                }
                if (mousePos.Y > ScreenSize.Y)
                {
                    beePos.Y = beePos.Y - beePos.Y / 2;
                }

                if (mousePos.Y < 1)
                {
                    beePos.Y = beePos.Y - beePos.Y / 2;
                }
                if (Vector2.Distance(beePos, seagullPos) < 50)
                {
                    beeDead = true;
                }

              
                    //if (Vector2.Distance(beePos, seagullPos) < 50)
                   // {
                      // Exit();
                    //}


                    // Set Rectangle area around the bee, seagull and flower.

                    Rectangle beeRectangle = new Rectangle((int)beePos.X, (int)beePos.Y,
                            bee[7].Width, bee[7].Height);

                    Rectangle birdRectangle = new Rectangle((int)seagullPos.X, (int)seagullPos.Y,
                                seagull[5].Width, seagull[5].Height);

                    Rectangle DaisyRectangle =
                        new Rectangle((int)DaisyPos.X, (int)DaisyPos.Y,
                            Daisy.Width, Daisy.Height);

                    Rectangle DaisyRectangle2 =
                        new Rectangle((int)Daisy2Pos.X, (int)Daisy2Pos.Y,
                            Daisy.Width, Daisy.Height);

                    // Has bee been hit? Check rectangle intersection between bee and seagull.
                    //If bee has been hit reduce life by 1.
                    if (birdRectangle.Intersects(beeRectangle))
                    {
                        lifeForce -= 1;
                    }

                    // Has bee collecetd any pollen? Check intersection between bee and flower.
                    // If bee has intersected flower increase pollen by 1.
                    if (DaisyRectangle.Intersects(beeRectangle))
                    {
                        Pollen += 1;
                    }
                    if (DaisyRectangle2.Intersects(beeRectangle))
                    {
                        Pollen += 3;
                    }
                    // TODO: Add your update logic here
                    base.Update(gameTime);
                }

            }
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Window.Title = "Time since played = " + TimeSinceStart.Elapsed.Seconds;
            //Start the first sound
            if (TimeSinceStart.IsRunning == false)
            {
                //flySound.Play();
                TimeSinceStart.Reset();
                TimeSinceStart.Start();
            }
            //Keep the sound playing while the game is running.
            if (TimeSinceStart.IsRunning == true)
            {
                if (TimeSinceStart.Elapsed.Seconds > 4)
                {
                    //flySound.Play();
                    TimeSinceStart.Reset();
                    TimeSinceStart.Start();
                }
            }
            spriteBatch.Begin();
            // Draw the images on screen.
            spriteBatch.Draw(imageTexture, imageRect, Color.White);
            spriteBatch.Draw(Daisy, DaisyPos, Color.White);
            spriteBatch.Draw(Daisy, Daisy1Pos, Color.White);
            spriteBatch.Draw(Daisy, Daisy2Pos, Color.White);
            spriteBatch.Draw(Daisy, Daisy3Pos, Color.White);
            //spriteBatch.Draw(seagull[KeyFrame1], seagullPos, Color.White);
            spriteBatch.Draw(bee[KeyFrame], beePos, Color.White);
            if (Direction == false)
            {

                spriteBatch.Draw(seagull[KeyFrame1], seagullPos, Color.White);
            }
            else
            {
                spriteBatch.Draw(seagull[KeyFrame1], seagullPos, null, Color.White, 0F, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
            spriteBatch.DrawString(lifeFont, "Life: %" + lifeForce.ToString(), lifeFontPos, Color.White);
            spriteBatch.DrawString(pollenFont, "Pollen: %" + Pollen.ToString(), pollenFontPos, Color.White);


            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


    }
}




