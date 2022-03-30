using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class FogManager : StoryboardObjectGenerator
    {
        [Configurable]
        public int startTime;

        [Configurable]
        public int endTime;

        [Configurable]
        public int posY;

        [Configurable]
        public int stroke;

        [Configurable]
        public int quantity;

        [Configurable]
        public Color4 color;

        [Configurable]
        public double fade = 0.5;

        public override void Generate()
        {
            FogGenerator fogManager = new FogGenerator(this);

            fogManager.GenerateFog(startTime, endTime, posY, stroke, quantity, color, fade);
        }
    }
}
