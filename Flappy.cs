﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Flappy
{
    public class Flappy : Game
    {
        public enum GameStates
        {
            Title,
            Play,
            Scoring,
            Countdown
        }

        public static GameStates gameState = GameStates.Title;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int SCREEN_WIDTH = 288;
        public static int SCREEN_HEIGHT = 512;

        //background and ground textures
        Texture2D background;
        Texture2D ground;
        public static Texture2D pipeTexture;

        private float backgroundScroll = 0;
        private float background2Scroll = SCREEN_WIDTH;
        private float groundScroll = 0;
        private float ground2Scroll = SCREEN_WIDTH;


        private const int BACKGROUND_SCROLL_SPEED = 30;
        private const int GROUND_SCROLL_SPEED = 60;

        public static bool scrolling = true;

        Bird bird;
        List<PipePairs> pipePairs = new List<PipePairs>();

        float counter;
        public Flappy()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        }

        protected override void Initialize()
        {
            bird = new Bird();
            bird.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pipeTexture = Content.Load<Texture2D>("pipeDown");
            background = Content.Load<Texture2D>("background-day");
            ground = Content.Load<Texture2D>("base");
            bird.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //background parallax
            if (backgroundScroll <= -288)
            {
                backgroundScroll = SCREEN_WIDTH;
            }
            else if (background2Scroll <= -288)
            {
                background2Scroll = SCREEN_WIDTH;
            }

            if (groundScroll <= -288)
            {
                groundScroll = SCREEN_WIDTH;
            }
            else if (ground2Scroll <= -288)
            {
                ground2Scroll = SCREEN_WIDTH;
            }


            backgroundScroll = backgroundScroll - BACKGROUND_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            background2Scroll = background2Scroll - BACKGROUND_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

            groundScroll = groundScroll - GROUND_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ground2Scroll = ground2Scroll - GROUND_SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(gameState == GameStates.Play)
            {
                counter += (float)gameTime.ElapsedGameTime.TotalSeconds;


                bird.Update(gameTime);

                if (counter > 2.5f)
                {
                    pipePairs.Add(new PipePairs());
                    counter = 0;
                }

                for (int i = 0; i < pipePairs.Count; i++)
                {
                    PipePairs pipes = pipePairs[i];
                    pipes.Update(gameTime);

                    foreach(var pip in pipes.pipes.Values)
                    {
                        if (bird.Collides(pip))
                        {
                            //collides
                        }
                    }

                    if (pipes.pipes["bottom"].Position.X < -pipes.pipes["bottom"].Texture.Width)
                    {
                        pipePairs.Remove(pipes);
                    }
                }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(backgroundScroll,0), Color.White);
            _spriteBatch.Draw(background, new Vector2(background2Scroll, 0), Color.White);

            foreach(var pipes in pipePairs)
            {
                pipes.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(ground, new Vector2(groundScroll,SCREEN_HEIGHT - ground.Height), Color.White);
            _spriteBatch.Draw(ground, new Vector2(ground2Scroll, SCREEN_HEIGHT - ground.Height), Color.White);
            bird.Draw(_spriteBatch);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}