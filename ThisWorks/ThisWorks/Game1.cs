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
using System.IO;

namespace ThisWorks
{


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string GameStatus = "Start";
        // 3 Status's
        //Start
        //Play
        //End

        //Adds Sound
        Stopwatch TimeSinceStart = new Stopwatch();
        SoundEffect flySound;
        //Texture definitions.
        //Daisy image
        Texture2D Daisy;
        Rectangle FlowerArea = new Rectangle(43, 350, 680, 25);
        int FlowerCount = 25;
        
        Flowers[] Daisys;
        Random RND = new Random();
        //int ReturnedPollen = 0;
        Boolean gameover;
        Texture2D titleScreen;
        Rectangle titleScreenRect;
        Texture2D gameOverImage;
        Rectangle gameOverRect;
        Vector2 lifeFontPos = new Vector2(600, 20);
        Vector2 pollenFontPos = new Vector2(50, 20);
        //Background information.
        Texture2D imageTexture;
        Rectangle imageRect;
        // Bee image
        Texture2D[] beeimg = new Texture2D[8];
        Texture2D[] birdPoop = new Texture2D[8];
        const float DEFAULT_X_SPEED = 300;
        const float DEFAULT_Y_SPEED = 150;
        Texture2D[] seagull = new Texture2D[8];
        const float INIT_X_POS = 80;
        const float INIT_Y_POS = 0;
        const float X_POS = 600;
        const float Y_POS = 100;
        const float X_SPEED = 500;
        const float Y_SPEED = 200;
        


        //int KeyFrame;
        // Bee's motion

        flyingBird Seagull;
        SpriteFont lifeFont;
        SpriteFont pollenFont;
        flyingBee Bee;
        Poop seagullPoop;
       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //void Reset()

        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.


        protected override void Initialize()
        {
            //GameState currentGameState = GameState.TitleScreen;


            // TODO: Add your initialization logic here
            //View the mouse pointer.
            this.IsMouseVisible = true;

            base.Initialize();
        }
        void reset()
        {
            Bee.lifeForce = 100;
            
            gameover = false;

        }

        protected override void LoadContent()
        {

            imageRect = new Rectangle(0, 0, 800, 500);
            titleScreenRect = new Rectangle(0, 0, 800, 500);
            gameOverRect = new Rectangle(0, 0, 800, 500);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //flySound = Content.Load<SoundEffect>("fly");
            titleScreen = Content.Load<Texture2D>("titleScreen");
            gameOverImage = Content.Load<Texture2D>("gameover1");
            imageTexture = Content.Load<Texture2D>("background");
            Daisy = Content.Load<Texture2D>("Daisy");
            Daisys = new Flowers[FlowerCount];
            
            for (int icount = 0; icount < FlowerCount; icount++)
            {
                Daisys[icount] = new Flowers(FlowerArea, Daisy, 10, RND);


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
                beeimg[0] = Content.Load<Texture2D>("bee");
                beeimg[1] = Content.Load<Texture2D>("bee1");
                beeimg[2] = Content.Load<Texture2D>("bee2");
                beeimg[3] = Content.Load<Texture2D>("bee3");
                beeimg[4] = Content.Load<Texture2D>("bee4");
                beeimg[5] = Content.Load<Texture2D>("bee5");
                beeimg[6] = Content.Load<Texture2D>("bee6");
                beeimg[7] = Content.Load<Texture2D>("bee7");

                birdPoop[0] = Content.Load<Texture2D>("seagullPoop");
                birdPoop[1] = Content.Load<Texture2D>("seagullPoop1");
                birdPoop[2] = Content.Load<Texture2D>("seagullPoop2");
                birdPoop[3] = Content.Load<Texture2D>("seagullPoop3");
                birdPoop[4] = Content.Load<Texture2D>("seagullPoop4");
                birdPoop[5] = Content.Load<Texture2D>("seagullPoop5");
                birdPoop[6] = Content.Load<Texture2D>("seagullPoop6");
                birdPoop[7] = Content.Load<Texture2D>("seagullPoop7");
                // TODO: use this.Content to load your game content here

                Seagull = new flyingBird(new Vector2(600, 30), new Vector2(500, 10), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), seagull);
                Bee = new flyingBee(new Vector2(INIT_X_POS, INIT_Y_POS), new Vector2(DEFAULT_X_SPEED, DEFAULT_Y_SPEED), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), beeimg);
                seagullPoop = new Poop(new Vector2(600, 30), new Vector2(500, 10), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), birdPoop);
                lifeFont = Content.Load<SpriteFont>("lifeFont");
                pollenFont = Content.Load<SpriteFont>("pollenFont");

                if (File.Exists(@"highscore.txt")) // This checks to see if the file exists
                {
                    String line;		// Create a string variable to read each line into
                    StreamReader sr = new StreamReader(@"highscore.txt");	// Open the file
                    line = sr.ReadLine();	// Read the first line in the text file
                    sr.Close();			// Close the file
                    line = line.Trim(); 	// This trims spaces from either side of the text
                    Bee.Pollen = (int)Convert.ToDecimal(line);	// This converts line to numeric
                }


            }
        }


        /// UnloadContent will be called once per game and is the place to unload
        /// all content.

        protected override void UnloadContent()
        {
            StreamWriter sw = new StreamWriter(@"highscore.txt");
            sw.WriteLine(Bee.Pollen.ToString());
            sw.Close();
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
       
            MouseState MouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(MouseState.X, MouseState.Y);

            Vector2 seagullPos = new Vector2(600 - (seagull[0].Width), 30 - (seagull[0].Height));

            if (GameStatus == "Start")
            {
                if (MouseState.LeftButton == ButtonState.Pressed)
                {
                    GameStatus = "Play";
                }

            }

            //if (GameStatus == "End")
            //{
            //    if (Bee.lifeForce <= 0)

            //        GameStatus = "End";

            //    //change game status
            //}

            

            if (GameStatus == "Play")
            {
                Bee.update(mousePos, seagullPos);



                for (int icount = 0; icount < FlowerCount; icount++)
                {
                    Bee.Pollen += Daisys[icount].update(mousePos);


                }

                Seagull.Update(gameTime);
                //if (Bee.lifeForce < 0)
                //{
                //    GameStatus = "End";

                //    //change game status
                //}
                if (Bee.lifeForce == 0)
                gameover = true;
            

            }

            base.Update(gameTime);

        }


        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            //GameState currentGameState = GameState.TitleScreen;
            if (GameStatus == "Start")
            {
                spriteBatch.Begin();
                {
                    // add draw for start game
                    spriteBatch.Draw(titleScreen, titleScreenRect, Color.White);
                }
                spriteBatch.End();
            }
            //if (GameStatus == "End")
            //{
           

            

            if (GameStatus == "Play")
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
                {
                    //if (currentGameState == GameStarted)

                    // Draw the images on screen.
                    spriteBatch.Draw(imageTexture, imageRect, Color.White);

                    //spriteBatch.Draw(seagull[KeyFrame1], seagullPos, Color.White);
                    for (int icount = 0; icount < FlowerCount; icount++)
                    {
                        Daisys[icount].draw(spriteBatch);


                    }

                    Seagull.Draw(spriteBatch);
                    Bee.Draw(spriteBatch);

                    //draw font
                    spriteBatch.DrawString(lifeFont, "Life: " + Bee.lifeForce.ToString(), lifeFontPos, Color.White);
                    spriteBatch.DrawString(pollenFont, "Pollen: " + Bee.Pollen.ToString(), pollenFontPos, Color.White);
                    if (gameover)

                        spriteBatch.Draw(gameOverImage, gameOverRect, Color.White);

                    spriteBatch.End();
                    // TODO: Add your drawing code here
                }
                base.Draw(gameTime);
            }


        }

        public int Pollen { get; set; }
    }
}

    



                
       
    

