using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
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
    public class ScriptsManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Rings(289489, 302730);

            Spectrum(0, Color4.LightSteelBlue);
            Spectrum(4, Color4.SteelBlue);
            //for overlappable objects
            FogGenerator fogManager = new FogGenerator(this);

            fogManager.GenerateFog(28027, 70694, 410, 48, 20, Color4.White, 0.5);
            fogManager.GenerateFog(81360, 92027, 400, 16, 15, Color4.White, 0.5);
            fogManager.GenerateFog(145260, 166360, 410, 18, 20, Color4.White, 0.3);
            fogManager.GenerateFog(168027, 200027, 360, 50, 17, Color4.White, 0.5);
            fogManager.GenerateFog(332523, 358014, 380, 15, 18, Color4.White, 0.45);
            fogManager.GenerateFog(401889, 422889, 400, 15, 20, Color4.White, 0.5);
            fogManager.GenerateFog(444722, 464555, 400, 15, 20, Color4.Orange, 0.5);
            fogManager.GenerateFog(473889, 496555, 380, 18, 15, Color4.Orange, 0.5);

            MovingLights lightsManager = new MovingLights(this);

            lightsManager.GenerateMovingLights(608555, 624555, 0.09);
        }
        public void Rings(int startTime, int endTime)
        {
            var blankBitmap = GetMapsetBitmap("sb/p.png");
            int amount = 22;
            double angle = 90;
            double radius = 150;
            for (var i = 0; i < amount; i++)
            {
                var Position = new Vector2(320, 240);
                double ConnectionAngle = Math.PI / amount * 1.5;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var lines = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                lines.ScaleVec(startTime, 1, 20);
                lines.Fade(startTime, startTime + 1000, 0, 0.15);
                lines.Fade(startTime + 1000, endTime, 0.15, 0.8);
                lines.Fade(endTime, endTime + 2000, 0.8, 0);

                var timeStep = 80;
                for (double time = startTime; time < endTime; time += timeStep)
                {
                    angle += -0.069;
                    radius += 0.01;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    double Rotation = Math.Atan2(position.Y - nPosition.Y, position.X - nPosition.X) - Math.PI / 2;

                    lines.Move(time, time + timeStep, Position, nPosition);
                    lines.Rotate(time, time + timeStep, Rotation, Rotation - 0.03405413);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }
        private Vector2 transform(Vector2 position)
        {
            Vector2 Position = new Vector2(250, 260);
            var Offset = new Vector2(Position.X, Position.Y);
            int Rotation = 0;

            position = new Vector2(position.X - Offset.X, position.Y - Offset.Y);
            return Vector2.Transform(position, Quaternion.FromEulerAngles((float)(MathHelper.DegreesToRadians(Rotation)), 0, 0)) + Offset;
        }
        public void Spectrum(float offset, Color4 Color)
        {
            var MinimalHeight = 0.1f;
            Vector2 Scale = new Vector2(1, 70);
            float LogScale = 600;
            int Width = 250;
            Vector2 Position = new Vector2(340, 360);
            int Rotation = 0;

            int StartTime = 276247;
            int EndTime = 289488;
            int endTime = Math.Min(EndTime, (int)AudioDuration);
            int startTime = Math.Min(StartTime, endTime);
            int BarCount = 10;
            var bitmap = GetMapsetBitmap("sb/p.png");

            var heightKeyframes = new KeyframedValue<float>[BarCount];
            for (var i = 0; i < BarCount; i++)
                heightKeyframes[i] = new KeyframedValue<float>(null);

            var fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / 11;
            var fftOffset = fftTimeStep * 0.2;
            for (var time = (double)startTime; time < endTime; time += fftTimeStep)
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
                keyframes.Simplify1dKeyframes(0.2, h => h);

                var bar = GetLayer("Spectrum").CreateSprite("sb/p.png", OsbOrigin.Centre);
                
                bar.Color(startTime, Color);
                bar.Fade(endTime, endTime, 0.5f, 0);

                bar.Rotate(OsbEasing.OutBack, startTime + BarCount * i, startTime + 1000 + BarCount * i,
                    (float)(MathHelper.DegreesToRadians(Rotation)),
                    (float)(MathHelper.DegreesToRadians(Rotation)));

                bar.Move(OsbEasing.OutBack, startTime + BarCount * i, startTime + 1000 + BarCount * i,
                    transform(new Vector2(positionX - offset, Position.Y - offset)),
                    transform(new Vector2(positionX - offset, Position.Y - offset)));

                var scaleX = Scale.X * barWidth / 2.5f / bitmap.Width;
                scaleX = (float)Math.Floor(scaleX * 10) / 10.0f;

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
