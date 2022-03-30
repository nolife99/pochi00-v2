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
    public class RainManager : StoryboardObjectGenerator
    {
        [Configurable]
        public int startTime;

        [Configurable]
        public int endTime;

        [Configurable]
        public int intensity;

        [Configurable]
        public bool Alternate = false;

        public override void Generate()
        {
            RainGenerator rainManager = new RainGenerator(this);

            if(Alternate)
                rainManager.GenerateRainAlt(startTime, endTime, intensity);
            
            else
                rainManager.GenerateRain(startTime, endTime, intensity);
        }
    }
}
