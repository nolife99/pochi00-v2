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
        public int posY = 420;

        [Configurable]
        public int stroke = 10;

        [Configurable]
        public int quantity;

        [Configurable]
        public Color4 color;

        [Configurable]
        public double fade = 0.5;

        public override void Generate()
        {
            if (startTime >= endTime)
            {
                startTime = (int)Beatmap.HitObjects.First().StartTime;
                endTime = (int)Beatmap.HitObjects.Last().EndTime;
            }
            endTime = Math.Min(endTime, (int)AudioDuration);
            startTime = Math.Min(startTime, endTime);
            if(quantity == 0)
            {
                quantity = Random(10, 20);
            }
            FogGenerator fogManager = new FogGenerator(this);

            fogManager.GenerateFog(startTime, endTime, posY, stroke, quantity, color, fade);
        }
    }
}
