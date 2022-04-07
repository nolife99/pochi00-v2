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
            if (StartTime >= EndTime)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }
            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

            if (Intensity <= 0)
            {
                Intensity = Random(2, 5);
            }
            RainGenerator rainManager = new RainGenerator(this);

            if(Alternate)
                rainManager.GenerateRainAlt(StartTime, EndTime, Intensity);
            
            else
                rainManager.GenerateRain(StartTime, EndTime, Intensity);
        }
    }
}
