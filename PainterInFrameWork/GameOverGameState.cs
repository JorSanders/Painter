using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PainterInFrameWork
{
    class GameOverGameState : GameObjectList
    {
        private SpriteGameObject gameOver;

        public GameOverGameState()
        {
            gameOver= new SpriteGameObject("spr_gameover");

            gameOver.Position = new Vector2((Painter.Screen.X/2-300), (Painter.Screen.Y/2-150));

            this.Add(gameOver);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.AnyKeyPressed)
            {
                Painter.GameStateManager.SwitchTo("playingState");
            }
        }
    }
}
