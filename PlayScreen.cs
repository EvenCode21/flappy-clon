using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Flappy
{
    class PlayScreen : Screen
    {

        Bird bird;
        public static List<PipePairs> pipePairs = new List<PipePairs>();

        float counter;
        float countDown = 3000;

        public override void Initialize()
        {

            bird = new Bird();
            bird.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            bird.LoadContent(Content);
        }
        public override void Update(GameTime gameTime)
        {
            if(!bird.died)
            {
                counter += (float)gameTime.ElapsedGameTime.TotalSeconds;

                

                bird.Update(gameTime);

                if (bird.Collides())
                {
                    bird.died = true;
                    bird.hit.Play();
                }

            if (counter > 2.5f)
            {
                pipePairs.Add(new PipePairs());
                counter = 0;
            }

                for (int i = 0; i < pipePairs.Count; i++)
                {
                    PipePairs pipes = pipePairs[i];
                    pipes.Update(gameTime);
                    bird.Scoring(pipes);
                    foreach (var pip in pipes.pipes.Values)
                    {
                        if (bird.Collides(pip))
                        {
                            bird.died = true;
                            bird.hit.Play();
                        }
                    }

                    if (pipes.pipes["bottom"].Position.X < -pipes.pipes["bottom"].Texture.Width)
                    {
                        pipePairs.Remove(pipes);
                    }
                }
            }

            if (bird.died)
            {
                countDown -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(countDown <= 0)
                {
                    if(SaveSystem.highScore <= Bird.score)
                        SaveSystem.Save();

                    Flappy.gameState = Flappy.GameStates.Scoring;
                    countDown = 3000;
                    bird.died = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pipes in pipePairs)
            {
                pipes.Draw(spriteBatch);
            }
            if(Bird.score < 10)
            {
                spriteBatch.Draw(CountDownScreen.numbers[Bird.score], Vector2.Zero, Color.White);
            }
            else if(Bird.score >= 10 && Bird.score < 100)
            {
                spriteBatch.Draw(CountDownScreen.numbers[Bird.firstDigit], Vector2.Zero, Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[Bird.secondDigit], new Vector2(20,0), Color.White);
            }else
            {
                spriteBatch.Draw(CountDownScreen.numbers[Bird.firstDigit], Vector2.Zero, Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[Bird.secondDigit], new Vector2(20, 0), Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[Bird.lastDigit], new Vector2(40, 0), Color.White);
            }

            SaveSystem.DrawHighScore(spriteBatch);
            bird.Draw(spriteBatch);

        }
    }
}
