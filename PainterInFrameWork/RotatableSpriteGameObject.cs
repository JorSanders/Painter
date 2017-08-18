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
    class RotatableSpriteGameObject : SpriteGameObject
    {
        protected float angle;

        public RotatableSpriteGameObject(string assetname, int layer = 0, string id = "", int sheetIndex = 0)
            : base(assetname, layer, id, sheetIndex)
        {
            angle = -0.5f;
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible || sprite == null)
                return;

            spriteBatch.Draw(sprite.Sprite, this.GlobalPosition, null, Color.White, angle, this.Origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
