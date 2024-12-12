﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy
{
    class PipePairs
    {
        private float GAP = 440;
        Random random = new Random();

        public Dictionary<string, Pipe> pipes = new Dictionary<string, Pipe>();

        public PipePairs()
        {
            pipes.Add("bottom", new Pipe("bottom", random.Next(Flappy.SCREEN_HEIGHT / 3, Flappy.SCREEN_HEIGHT - 180)));
            pipes.Add("top", new Pipe("top", pipes["bottom"].Position.Y - GAP));

        }
        public void Update(GameTime gameTime)
        {
            foreach(var pipe in pipes.Values)
            {
                pipe.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var pipe in pipes.Values)
            {
                pipe.Draw(spriteBatch);
            }
        }

    }
}