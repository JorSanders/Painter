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
    class ThreeColorGameObject : RotatableSpriteGameObject
    {
        protected SpriteSheet colorRedSprite, colorGreenSprite, colorBlueSprite;
        protected Color color;

        public ThreeColorGameObject(string redAssetName, string greenAssetName, string blueAssetName)
            : base("")
        {
            colorRedSprite = new SpriteSheet(redAssetName);
            colorGreenSprite = new SpriteSheet(greenAssetName);
            colorBlueSprite = new SpriteSheet(blueAssetName);

            color = Color.Blue;
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value != Color.Red && value != Color.Green && value != Color.Blue)
                    return;
                color = value;
                if (color == Color.Red)
                    this.sprite = colorRedSprite;
                else if (color == Color.Green)
                    this.sprite = colorGreenSprite;
                else if (color == Color.Blue)
                    this.sprite = colorBlueSprite;
            }
        }

        public override void Reset()
        {
            base.Reset();

            Color = Color.Blue;
        }
        
    }
}
