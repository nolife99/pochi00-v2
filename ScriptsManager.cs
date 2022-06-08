using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    class ScriptsManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Circles();

            Scripts sprite = new Scripts(this);

            sprite.Fog(28027, 70694, 380, 20, Color4.White, 0.6);
            sprite.Fog(81360, 92027, 380, 20, Color4.White, 0.6);
            sprite.Fog(145360, 166027, 380, 20, Color4.White, 0.6);
            sprite.Fog(168027, 200027, 380, 20, Color4.White, 0.6, "fogKiai");
            sprite.Fog(332523, 358014, 380, 20, Color4.White, 0.6);
            sprite.Fog(401889, 422889, 380, 20, Color4.White, 0.6);
            sprite.Fog(444722, 464555, 380, 20, Color4.Orange, 0.6); 
            sprite.Fog(473889, 496555, 380, 20, Color4.Orange, 0.6);

            sprite.Danmaku(102694, 124027, 5000);

            sprite.SquareTransition(331695, 332522, false, 50, new Color4(10, 10, 10, 1), OsbEasing.InQuad, false, "foreground transition"); 
            sprite.SquareTransition(355695, 359006, true, 18.2f, new Color4(10, 10, 10, 1), OsbEasing.InQuad);
            sprite.SquareTransition(378913, 380555, false, 30, new Color4(33, 25, 25, 0), OsbEasing.InQuad, false);
            sprite.SquareTransition(401222, 401888, false, 50, new Color4(33, 25, 25, 0), OsbEasing.InSine, false, "foreground transition");
            sprite.SquareTransition(574888, 575555, false, 50, new Color4(10, 10, 10, 1), OsbEasing.InSine, true);

            sprite.TransitionLines(123360, 124027, 124277, "foreground transition");
            sprite.TransitionLines(144011, 145027, 145345);
            sprite.TransitionLines(166360, 166694, 167027, "foreground transition");
            sprite.TransitionLines(465222, 465555, 465889, "transition?");
            sprite.TransitionLines(628555, 629221, 631221, "transition end", true);

            sprite.Rain(380555, 433889, 12, 2);
            sprite.Rain(587221, 629471, 10, 3);
            sprite.Rain(608555, 629471, 12.5, 1);

            sprite.Highlight(608555, 631221);

            sprite.DiamondCross(27, 1276, 0, 100, false);
            sprite.DiamondCross(2526, 5776, 500, 0, true, "cross", OsbEasing.InQuint);
            sprite.DiamondCross(5276, 5776, 0, 300, false);
            sprite.DiamondCross(5526, 5776, 0, 200, false); 
            sprite.DiamondCross(28027, 29360, 0, 300, false);
            sprite.DiamondCross(80027, 81360, 0, 300, false);
            sprite.DiamondCross(80277, 81360, 0, 290, false);
            sprite.DiamondCross(80527, 81360, 0, 280, false);
            sprite.DiamondCross(80694, 81360, 0, 270, false);
            sprite.DiamondCross(81360, 82694, 0, 400, false);
            sprite.DiamondCross(166694, 168027, 0, 100, false, "foreground");
            sprite.DiamondCross(168027, 169360, 0, 450, false);
            sprite.DiamondCross(194027, 194694, 400, 0, true, "cross", OsbEasing.InQuint);
            sprite.DiamondCross(195027, 196360, 0, 400, false);
            sprite.DiamondCross(379461, 380556, 450, 0, true, "cross", OsbEasing.InQuint);
            sprite.DiamondCross(401555, 402222, 0, 350, false, "foreground");
            sprite.DiamondCross(421889, 423222, 450, 0, true, "cross", OsbEasing.InQuint);
            sprite.DiamondCross(423222, 424555, 0, 350, false, "foreground");
        }
        private void Circles()
        {
            int StartTime = 124027;
            int EndTime = 144881;
            int amount = 2;

            var travelTime = Beatmap.GetTimingPointAt(StartTime).BeatDuration * 8;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / amount;

            double rad;
            double angle = 0;
            double radius = 100;
            double Radius = 170;
            for (int i = 0; i < amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = radius * Math.Cos(rad) + Pos.X - 2;
                var y = radius * Math.Sin(rad) + Pos.Y;
                var position = new Vector2((int)x, (int)y);
                var duration = EndTime - StartTime;

                var circles = GetLayer("circle").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
                circles.Scale(124027, 144944, 0.2, 0.2);

                var circle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));
                circle.Fade(StartTime, StartTime + 500, 0, 1);
                circle.StartLoopGroup(StartTime, duration / ((int)travelTime) + 1);
                circle.Color(0, Color4.LightBlue);
                circle.Scale(travelTime / 8 - 50, 0.02);
                circle.Scale((travelTime / 8) * 7 + 50, 0.005);
                circle.Color(travelTime / 8 - 50, Color4.GreenYellow);
                circle.Color((travelTime / 8) * 7 + 50, travelTime, Color4.LightBlue, Color4.LightBlue);
                circle.EndGroup();

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.0982;

                    x = radius * Math.Cos(rad) + Pos.X - 2;
                    y = radius * Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2((int)x, (int)y);

                    circle.Move(time, time + timeStep, position, newPos);
                    position = newPos;
                }
                angle += ConnectionAngle / (amount / 2);
            }
            for (int l = 0; l < amount; l++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = Radius * Math.Cos(rad) + Pos.X - 2;
                var y = Radius * Math.Sin(rad) + Pos.Y;
                var position = new Vector2((int)x, (int)y);
                var duration = EndTime - StartTime;

                var outCircle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));
                outCircle.Fade(StartTime, StartTime + 500, 0, 1);
                outCircle.StartLoopGroup(StartTime, duration / ((int)travelTime) + 1);
                outCircle.Color(0, Color4.GreenYellow);
                outCircle.Scale(travelTime / 8 - 50, 0.005);
                outCircle.Scale((travelTime / 8) * 7 + 50, 0.02);
                outCircle.Color(travelTime / 8 - 50, Color4.LightBlue);
                outCircle.Color((travelTime / 8) * 7 + 50, travelTime, Color4.GreenYellow, Color4.GreenYellow);
                outCircle.EndGroup();

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double t = StartTime; t < EndTime; t += timeStep)
                {
                    rad += 0.0493;
                    x = Radius * Math.Cos(rad) + Pos.X - 2;
                    y = Radius * Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2((int)x, (int)y);

                    outCircle.Move(t, t + timeStep, position, newPos);
                    position = newPos;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }
    }
}
