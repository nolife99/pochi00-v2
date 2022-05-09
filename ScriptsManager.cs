using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class ScriptsManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Circles();
            RotatingLines();

            GlitchSection(4, Color4.SkyBlue);
            GlitchSection(0, Color4.LightSteelBlue);

            Scripts particleManager = new Scripts(this);

            particleManager.GenerateFog(28027, 70694, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(81360, 92027, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(145260, 166027, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(168027, 200027, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(332523, 358014, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(401889, 422889, 400, 10, 25, Color4.White, 0.5);
            particleManager.GenerateFog(444722, 464555, 400, 10, 25, Color4.Orange, 0.5);
            particleManager.GenerateFog(473889, 496555, 400, 10, 25, Color4.Orange, 0.5);

            particleManager.SquareTransition(355695, 359006, true, 19, Color4.Black, OsbEasing.InSine);

            particleManager.GenerateLights(608555, 624555, 0.09);

            particleManager.GenerateRain(380555, 432555, 9, true);
            particleManager.GenerateRain(587221, 629138, 5);
            particleManager.GenerateRain(597888, 629138, 5);
            particleManager.GenerateRain(608555, 629138, 15);
        }
        public void Circles()
        {
            int StartTime = 124027;
            int EndTime = 145194;
            int Amount = 2;

            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / Amount;

            double rad;
            double angle = 0;
            double radius = 100;
            double Rad;
            double Angle = 0;
            double Radius = 170;
            for (int i = 0; i < Amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                var y = (int)radius * (float)Math.Sin(rad) + Pos.Y;
                var position = new Vector2(x, y);
                
                var x2 = (int)-radius * (float)Math.Cos(rad) + Pos.X - 2;
                var y2 = (int)-radius * (float)Math.Sin(rad) + Pos.Y;
                var nPos = new Vector2(x2, y2);

                var circle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre);

                var TravelTime = Beat * 8;
                var duration = EndTime - StartTime;

                circle.StartLoopGroup(StartTime, 8);
                circle.Scale(0, 0.05);
                circle.Scale(TravelTime / 8 - 50, 0.2);
                circle.Scale((TravelTime / 8) * 7 + 50, 0.05);
                circle.Scale(TravelTime, 0.05);
                circle.Color(0, Color4.LightBlue);
                circle.Color(TravelTime / 8 - 50, Color4.GreenYellow);
                circle.Color((TravelTime / 8) * 7 + 50, Color4.LightBlue);
                circle.EndGroup();

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 5;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.0786;

                    x = (int)radius * (float)Math.Cos(rad) + Pos.X - 2;
                    y = (int)radius * (float)Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2(x, y);

                    circle.Move(time, time + timeStep, position, newPos);

                    position = newPos;
                }
                angle += ConnectionAngle / (Amount / 2);
            }
            for (int l = 0; l < Amount; l++)
            {
                Rad = Angle * Math.PI / ConnectionAngle;
                var X = (int)Radius * (float)Math.Cos(Rad) + Pos.X - 2;
                var Y = (int)Radius * (float)Math.Sin(Rad) + Pos.Y;
                var Position = new Vector2(X, Y);

                var outCircle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre);

                var travelTime = Beat * 8;
                var Duration = EndTime - StartTime;

                outCircle.StartLoopGroup(StartTime, 8);
                outCircle.Scale(0, 0.2);
                outCircle.Scale(travelTime / 8 - 50, 0.05);
                outCircle.Scale((travelTime / 8) * 7 + 50, 0.2);
                outCircle.Scale(travelTime, 0.2);
                outCircle.Color(0, Color4.GreenYellow); 
                outCircle.Color(travelTime / 8 - 50, Color4.LightBlue);
                outCircle.Color((travelTime / 8) * 7 + 50, Color4.GreenYellow);
                outCircle.EndGroup();

                var TimeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 5;
                for (double t = StartTime; t < EndTime; t += TimeStep)
                {
                    Rad += 0.0393;
                    X = (int)Radius * (float)Math.Cos(Rad) + Pos.X - 2;
                    Y = (int)Radius * (float)Math.Sin(Rad) + Pos.Y;

                    var NewPos = new Vector2(X, Y);

                    outCircle.Move(t, t + TimeStep, Position, NewPos);

                    Position = NewPos;
                }
                Angle += ConnectionAngle / (Amount / 2);
            }
        }
        public void RotatingLines()
        {
            int startTime = 289489;
            int endTime = 302730;
            var blankBitmap = GetMapsetBitmap("sb/p.png");
            int amount = 22;
            double angle = 90;
            double radius = 150;
            for (var i = 0; i < amount; i++)
            {
                var Position = new Vector2(0, 0);
                double ConnectionAngle = Math.PI / amount * 1.5;

                var position = new Vector2(
                    (float)(318 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var lines = layer.CreateSprite("sb/p.png", OsbOrigin.Centre, Position);

                lines.ScaleVec(startTime, 1, 20);
                lines.Fade(startTime, startTime + 1000, 0, 0.15);
                lines.Fade(startTime + 1000, endTime, 0.15, 0.8);
                lines.Fade(endTime, endTime + 2000, 0.8, 0);

                var timeStep = Beatmap.GetTimingPointAt((int)startTime).BeatDuration / 5;
                for (double time = startTime; time < endTime; time += timeStep)
                {
                    angle += -0.068;
                    radius += 0.01;

                    var nPosition = new Vector2(
                        (float)(318 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    lines.Move(time, time + timeStep, Position, nPosition);
                    Position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);

                var pos = new Vector2(
                    (float)(318 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));
                    
                var Rotation = Math.Atan2(position.Y - pos.Y, position.X - pos.X) - Math.PI / 2;
                    
                lines.Rotate(startTime, endTime, Rotation, Rotation - Math.PI * 2.25);
            }
        }
        public void GlitchSection(float offset, Color4 Color)
        {
            int startTime = 276247;
            int endTime = 289488;
            var MinimalHeight = 0;
            Vector2 Scale = new Vector2(1, 70);
            float LogScale = 270;
            int Width = 287;
            Vector2 Position = new Vector2(337, 377);

            int BarCount = 10;
            var bitmap = GetMapsetBitmap("sb/p.png");

            var heightKeyframes = new KeyframedValue<float>[BarCount];
            for (var i = 0; i < BarCount; i++)
                heightKeyframes[i] = new KeyframedValue<float>(null);

            double fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / 11;
            double fftOffset = fftTimeStep * 0.2;
            for (var time = (double)startTime; time <= endTime; time += fftTimeStep)
            {
                var fft = GetFft(time + fftOffset, BarCount, null, OsbEasing.InExpo);
                for (var i = 0; i < BarCount; i++)
                {
                    var height = (float)Math.Log10(1 + fft[i] * LogScale) * Scale.Y / bitmap.Height;
                    if (height < MinimalHeight) height = MinimalHeight;

                    heightKeyframes[i].Add(time, height);
                }
            }

            var barWidth = Width / BarCount;
            var posX = Position.X - (Width / 2);

            for (var i = 0; i < BarCount; i++)
            {
                var positionX = posX + i * barWidth;
                var keyframes = heightKeyframes[i];
                var pos = new Vector2(positionX - offset, Position.Y - offset);
                keyframes.Simplify1dKeyframes(0.15, h => h);

                var bar = GetLayer("Glitch").CreateSprite("sb/p.png", OsbOrigin.Centre, pos);
                
                bar.Color(startTime, Color);
                bar.Fade(endTime, endTime, 0.4, 0);

                var scaleX = Scale.X * barWidth / 1.75f / bitmap.Width;
                scaleX = (float)Math.Floor(scaleX * 10) / 10;

                var hasScale = false;
                keyframes.ForEachPair(
                    (start, end) =>
                    {
                        hasScale = true;
                        bar.ScaleVec(start.Time, end.Time,
                            scaleX, start.Value,
                            scaleX, end.Value);
                    },
                    MinimalHeight,
                    s => (float)Math.Round(s, 1)
                );
                if (!hasScale) bar.ScaleVec(startTime, scaleX, MinimalHeight);
            }
        }
    }
}
