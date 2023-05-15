using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Pong_Game.Content;
using System;
using System.IO;  
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace Pong_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static int width;
        public static int height;
        public static Random random;
        public SpriteFont font1;
        private ScoreManager _scoreManager;
        public static int Highscore = 0;
        public static Score scores;
        private List<Sprite> spites;
        enum GameState {PlAY, HighScores};
        GameState gameState = GameState.PlAY;
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            random = new Random();
            try
            {
                Highscore = int.Parse(File.ReadAllText("Highsc.txt"));
            }
            catch (Exception IO)
            {
                string m = IO.Message;
            }
             


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            //create a spriteBatch which is used to darw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            var texture1 = this.Content.Load<Texture2D>("Player");
            var texture1_1 = this.Content.Load<Texture2D>("Player_B");
            var texture2 = this.Content.Load<Texture2D>("Ball");
            scores = new Score(Content.Load<SpriteFont>("Font"));
            font1 = this.Content.Load<SpriteFont>("Font2");
            spites = new List<Sprite>()           //creation of the List of sprites and placing sprites in it
            {
                new Sprite(Content.Load<Texture2D>("Background")),
                new Player(texture1)
                {
                    Position = new Vector2(20,(height/2)- (texture1.Height/2)),
                    
                    input = new Input()
                    {
                        Up= Keys.W,
                        Down = Keys.S,
                    }
                },
               new Player(texture1_1)
                {
                    Position = new Vector2(width-20-texture1.Width,(height/2)- (texture1.Height/2)),
                    
                    input = new Input()
                    {
                        Up= Keys.Up,
                        Down = Keys.Down,
                    }
                },

                new Ball(texture2)
                {
                    Position = new Vector2((width/2)-(texture2.Width/2),(height/2)- (texture2.Height/2)),
                    score = scores,
                    
                },
            };
         

        }

        
        protected override void UnloadContent()
        {
           

            // TODO: unload any content manager here
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.PlAY:
                    foreach (var sprite in spites)
                    {
                        sprite.Update(gameTime, spites);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        gameState = GameState.HighScores;
                    }

                        break;
                case GameState.HighScores:


                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        
                        gameState = GameState.PlAY;
                    }
                    break;
            }

                // TODO: Add your update logic here

                base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.PlAY:

                    foreach (var sprite in spites)
                    {
                        sprite.Draw(spriteBatch);
                        scores.Draw(spriteBatch);
                        string Highscorestr = "Highscore : " + Highscore.ToString(); 
                        spriteBatch.DrawString(font1,Highscorestr, new Vector2(20, Game1.height / 2 - Game1.height / 4), Color.White); //display on screen
                    }
                    break;
                
                default:
                    gameState = GameState.PlAY;
                    break;
            }

                    spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}