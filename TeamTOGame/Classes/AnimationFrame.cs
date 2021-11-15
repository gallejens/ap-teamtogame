using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace TeamTOGame
{
    class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }

        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }
    }

}
