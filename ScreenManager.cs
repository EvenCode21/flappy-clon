using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Flappy
{
    class ScreenManager
    {
        
        public static Dictionary<string,Screen> screens = new Dictionary<string,Screen>();
        public ScreenManager()
        {
            screens.Add("title", new TitleScreen());
            screens.Add("count",new CountDownScreen());
            screens.Add("play",new PlayScreen());
            screens.Add("gameover",new GameOverScreen());
        }
        public void Initialize()
        {
            foreach(var screen in screens.Values)
            {
                screen.Initialize();
            }
            SaveSystem.Load();
        }

        public void LoadContent(ContentManager Content)
        {
            foreach(var screen in screens.Values)
            {
                screen.LoadContent(Content);
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (Flappy.gameState)
            {
                case Flappy.GameStates.Title:
                    screens["title"].Update(gameTime); 
                    break;
                case Flappy.GameStates.Countdown:
                    screens["count"].Update(gameTime);
                    break;
                case Flappy.GameStates.Play:
                    screens["play"].Update(gameTime);
                    break;
                case Flappy.GameStates.Scoring:
                    screens["gameover"].Update(gameTime);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch(Flappy.gameState)
            {
                case Flappy.GameStates.Title:
                    screens["title"].Draw(spriteBatch);
                    break;
                case Flappy.GameStates.Countdown:
                    screens["count"].Draw(spriteBatch);
                    break;
                case Flappy.GameStates.Play:
                    screens["play"].Draw(spriteBatch);
                    break;
                case Flappy.GameStates.Scoring:
                    screens["gameover"].Draw(spriteBatch);
                    break;
            }
        }

        
    }
}
