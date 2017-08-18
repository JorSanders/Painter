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
    class PainterGameWorld: GameObjectList
    {
        private SpriteGameObject background = null;
        private RotatableSpriteGameObject cannonBarrel = null;
        private ThreeColorGameObject cannonColor = null;
        private ThreeColorGameObject can1 = null, can2 = null, can3 = null;
        private Ball ball;
        private TextGameObject scoreText;
        private SpriteGameObject scoreBar = null;
        private GameObjectList livesSprites;
        private int score, lives;

        public PainterGameWorld()
        {
            background = new SpriteGameObject("spr_background");
            cannonBarrel = new RotatableSpriteGameObject("spr_cannon_barrel");
            cannonBarrel.Position = new Vector2(74, 404);
            cannonBarrel.Origin = new Vector2(34, 34);

            cannonColor = new ThreeColorGameObject("spr_cannon_red", "spr_cannon_green", "spr_cannon_blue");
            cannonColor.Position = new Vector2(52, 398);
            cannonColor.Color = Color.Blue; // Set the begin color to blue

            scoreText = new TextGameObject("GameFont");
            scoreText.Position = new Vector2(15, 12);
            scoreBar = new SpriteGameObject("spr_scorebar");
            scoreBar.Position = new Vector2(5, 5);

            can1 = new PaintCan(450f, Color.Red);
            can2 = new PaintCan(575f, Color.Green);
            can3 = new PaintCan(700f, Color.Blue);

            ball = new Ball();

            // Add a background to the game
            this.Add(background);

            // Add a cannon to the game with a Barrel and Color
            this.Add(cannonBarrel);
            this.Add(cannonColor);

            // Add the three cans to the game
            this.Add(can1);
            this.Add(can2);
            this.Add(can3);

            // Add a ball to the game
            this.Add(ball);

            // Initialize score and lives
            this.Score = 0;
            this.lives = 5;

            // Add lives
            livesSprites = new GameObjectList();
            for (int lifeNr = 0; lifeNr < lives; lifeNr++)
            {
                SpriteGameObject life = new SpriteGameObject("spr_lives", 0, lifeNr.ToString());
                life.Position = new Vector2((lifeNr * life.BoundingBox.Width + 10), 50);
                livesSprites.Add(life);
            }

            this.Add(livesSprites);
            
            // Add scoretext
            this.Add(scoreBar);
            this.Add(scoreText);

        }

        public int Score
        {
            get { return score; }
            set 
            { 
                score = value;
                if (scoreText != null)
                    scoreText.Text = "Score: " + value;
            }
        }

        public int Lives
        {
            get { return lives; }
            set 
            {
                if (value > lives)
                    return;

                for (int lifeNr = 0; lifeNr < lives; lifeNr++)
                {
                    SpriteGameObject sgo = (SpriteGameObject)livesSprites.Find(lifeNr.ToString());
                    sgo.Visible = lifeNr < value;
                }

                lives = value; 
            }
        }

        public bool IsOutsideWorld(Vector2 aPosition)
        {
            return aPosition.X < 0 || aPosition.X > Painter.Screen.X || aPosition.Y > Painter.Screen.Y;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.R))
                cannonColor.Color = Color.Red;
            else if (inputHelper.KeyPressed(Keys.G))
                cannonColor.Color = Color.Green;
            else if (inputHelper.KeyPressed(Keys.B))
                cannonColor.Color = Color.Blue;

            double opposite = inputHelper.MousePosition.Y - cannonBarrel.GlobalPosition.Y;
            double adjacent = inputHelper.MousePosition.X - cannonBarrel.GlobalPosition.X;
            cannonBarrel.Angle = (float)Math.Atan2(opposite, adjacent);

            if (inputHelper.MouseLeftButtonPressed() && !ball.Shooting)
                ball.Shoot(inputHelper, cannonColor, cannonBarrel);
        }

        public override void Update(GameTime gameTime)
        {
            if (ball.CollidesWith(can1))
            {
                can1.Color = ball.Color;
                ball.Reset();
            }

            if (ball.CollidesWith(can2))
            {
                can2.Color = ball.Color;
                ball.Reset();
            }

            if (ball.CollidesWith(can3))
            {
                can3.Color = ball.Color;
                ball.Reset();
            }

            if (lives <= 0)
            {
                Reset();
                Painter.GameStateManager.SwitchTo("gameOverState");
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            lives = 5;
            Score = 0;
            ball.Reset();
            can1.Reset();
            can2.Reset();
            can3.Reset();
            base.Reset();
        }
    }
}
