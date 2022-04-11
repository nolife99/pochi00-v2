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
    public class Lines : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    Rings(289488, 302730);
        }
        public void Rings(int startTime, int endTime)
        {
            var blankBitmap = GetMapsetBitmap("sb/p.png");
            var rotation = MathHelper.DegreesToRadians(90);
            int amount = 18;
            double angle = 45;
            double radius = 150;
            for (var i = 0; i < amount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / amount * 2;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var lines = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                lines.ScaleVec(startTime, 1, 20);
                lines.Fade(startTime, startTime + 1000, 0, 0.15);
                lines.Fade(startTime + 1000, endTime, 0.15, 0.8);
                lines.Fade(endTime, endTime + 2000, 0.6, 0);

                var timeStep = 67;
                for (int time = startTime; time < endTime; time += timeStep)
                {
                    angle += -0.06;
                    radius += 0.01;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 3;

                    lines.Move(time, time + timeStep, Position, nPosition);
                    lines.Rotate(time, time + timeStep, Rotation, Rotation);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }
    }
}
