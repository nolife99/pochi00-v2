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
    public class MovingLightsManager : StoryboardObjectGenerator
    {
        [Configurable]
        public int startTime;

        [Configurable]
        public int endTime;

        [Configurable]
        public double fade = 0.8;

        public override void Generate()
        {
            MovingLights particleManager = new MovingLights(this);

            particleManager.GenerateMovingLights(startTime, endTime, fade);
        }
    }
}
