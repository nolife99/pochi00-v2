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
		    Rings(289488, 302727);
        }
        public void Rings(int startTime, int endTime)
        {
            var blankBitmap = GetMapsetBitmap("sb/p.png");
            int amount = 20;
            double angle = 90;
            double radius = 150;
            for (var i = 0; i < amount; i++)
            {
                var Position = new Vector2(320, 240);
                double ConnectionAngle = Math.PI / amount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var lines = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                lines.ScaleVec(startTime, 1, 20);
                lines.Fade(startTime, startTime + 1000, 0, 0.15);
                lines.Fade(startTime + 1000, endTime, 0.15, 0.8);
                lines.Fade(endTime, endTime + 2000, 0.8, 0);

                int timeStep = 61;
                for (int time = startTime; time < endTime; time += timeStep)
                {
                    angle += -0.05;
                    radius += 0.01;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    var Rotation = Math.Atan2((position.Y - nPosition.Y), (position.X - nPosition.X)) - Math.PI / 2;

                    lines.Move(time, time + timeStep * 1.3, Position, nPosition);
                    lines.Rotate(time, time + timeStep * 1.3, Rotation, Rotation);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }
    }
}
