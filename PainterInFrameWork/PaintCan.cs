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
    class PaintCan : ThreeColorGameObject
    {
        protected Color targetColor;
        protected float minVelocity;
        protected float positionOffset;

        public PaintCan(float positionOffset, Color targetCol)
            : base("spr_can_red", "spr_can_green", "spr_can_blue")
        {
            this.positionOffset = positionOffset;
            this.targetColor = targetCol;

            minVelocity = 30;
            this.Reset();
        }

        public override void Reset()
        {
           base.Reset();
           position = new Vector2(this.positionOffset, -BoundingBox.Height);
           velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            // Dropping the PaintCan
            if (velocity.Y < 1f && GameEnvironment.Random.NextDouble() < 0.01)
            {
                velocity = CalculateRandomVelocity();
                Color = CalculateRandomColor();
            }

            PainterGameWorld pgw = GameWorld as PainterGameWorld;
            if (pgw.IsOutsideWorld(GlobalPosition))
            {
                if (color == targetColor)
                {
                    pgw.Score += 10;
                    Painter.AssetManager.PlaySound("snd_collect_points");
                }
                else
                    pgw.Lives--;

                Reset();
            }
            Angle = (float)Math.Sin(position.Y / 50.0f) * 0.1f;

            minVelocity += 0.001f;
            base.Update(gameTime);
        }

        public Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)GameEnvironment.Random.NextDouble() * 30 + minVelocity);
        }

        public Color CalculateRandomColor()
        {
            int randomVal = GameEnvironment.Random.Next(3);
            if (randomVal == 0)
                return Color.Red;
            else if (randomVal == 1)
                return Color.Green;
            else
                return Color.Blue;
        }
    }
}
