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
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public int Intensity;

        [Configurable]
        public bool Alternate = false;

        public override void Generate()
        {
            RainGenerator rainManager = new RainGenerator(this);

            rainManager.GenerateRainAlt(380555, 432809, 9);
            rainManager.GenerateRain(587221, 629138, 5);
            rainManager.GenerateRain(597888, 629138, 5);
            rainManager.GenerateRain(608555, 629138, 15);
        }
    }
}
