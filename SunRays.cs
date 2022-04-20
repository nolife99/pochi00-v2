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
    public class SunRays : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        public override void Generate()
        {
            if (StartTime >= EndTime)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }
            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

		    GodRays(StartTime, EndTime);
        }
        private void GodRays(int startTime, int endTime)
        {
            for (int i = 0; i < 15 ; i++)
            {
                var sprite = GetLayer("").CreateSprite("sb/light.png", OsbOrigin.CentreLeft);
                var rotateStart = MathHelper.DegreesToRadians(Random(80, 100));
                var rotateEnd = MathHelper.DegreesToRadians(Random(75, 115));
                int RandomDuration = Random(4000, 7000);
                int loopCount = (endTime - startTime) / (RandomDuration * 2);

                sprite.StartLoopGroup(startTime, (int)loopCount);
                sprite.Rotate(0, RandomDuration, rotateStart, rotateEnd);
                sprite.Rotate(RandomDuration, RandomDuration * 2, rotateEnd, rotateStart);
                sprite.EndGroup();

                double Fade = Random(0.25, 0.4);
                sprite.StartLoopGroup(startTime, loopCount);
                sprite.Move(0, new Vector2(Random(0, 690), -25));
                sprite.Fade(0, 1500, 0, Fade);
                sprite.Fade(RandomDuration * 2 - 1500, RandomDuration * 2 - 1000, Fade, 0);
                sprite.EndGroup();

                sprite.Scale(startTime, 0.73);
                sprite.Additive(startTime, endTime);
            }
        }
    }
}
