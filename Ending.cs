using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    class Ending : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Birds(20, 40, 587221, 608721, 40, false, 0.02, 0.05);
            Birds(20, 40, 587221, 608721, 40, true, 0.02, 0.05);
            GodRays(587221, 614000);
        }
        private void Birds(int FlyingSpeed, int Acceleration, int StartTime, int EndTime, int SpriteAmount, bool right, double ScaleMin, double ScaleMax)
        {
            var StartPosition = new Vector2(320, 260);
            var EndPosition = new Vector2(320, 380);
            double ParticleFade = 1;
            int FadeTimeIn = 2000;
            int FadeTimeOut = 700;

            var layer = GetLayer("Birds");
            using (var pool = new OsbSpritePool(layer, "sb/bird.png", OsbOrigin.Centre, (Sprite, startTime, endTime) =>
            {}))
            {
                var RealTravelTime = Random(3500, 7000);
                for (int i = StartTime; i < EndTime - 3500; i += RealTravelTime / SpriteAmount)
                {
                    var sprite = pool.Get(i, i + RealTravelTime);

                    var RandomScaling = Math.Round(Random(ScaleMin, ScaleMax), 3);
                    int FlipInterval = Random(FlyingSpeed * 12, Acceleration * 8);
                    float lastX = Random(StartPosition.X, EndPosition.X);
                    float lastY = Random(StartPosition.Y, EndPosition.Y);
                    var rVec = Random(Math.PI * 2f);
                    double sVec = FlyingSpeed * 2.5;
                    double vX = Math.Cos(rVec) * sVec;
                    double vY = Math.Sin(rVec) * sVec;
                    double lastAngle = 90;
                    var timeStep = RealTravelTime / 14 + 1;
                    sprite.Additive(i);

                    for (double t = i; t < i + RealTravelTime - 1; t += timeStep)
                    {
                        if (right)
                        {
                            var nextX = lastX + vX;
                            var nextY = lastY - (vY / 7.5);

                            double currentAngle = sprite.RotationAt(t);
                            double newAngle = Math.Atan2(nextY - lastY, nextX - lastX) + Math.PI / 2;

                            var startPosition = new Vector2(lastX, lastY);
                            var endPosition = new Vector2(lastX, lastY);

                            double angle = Math.Atan2(startPosition.Y - endPosition.Y, startPosition.X - endPosition.X) - Math.PI / 2;

                            sprite.Move(t, t + timeStep, (int)lastX, (int)lastY, (int)nextX, (int)nextY);
                            sprite.Rotate(t, t + timeStep, currentAngle, newAngle);

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = (float)nextX;
                            lastY = (float)nextY;
                        }
                        else
                        {
                            var nextX = lastX - vX;
                            var nextY = lastY - (vY / 15);

                            var currentAngle = sprite.RotationAt(t);
                            var newAngle = Math.Atan2((nextY - lastY), (nextX - lastX)) + (Math.PI / 2);

                            var startPosition = new Vector2(lastX, lastY);
                            var endPosition = new Vector2(lastX, lastY);

                            var angle = Math.Atan2((startPosition.Y - endPosition.Y), (startPosition.X - endPosition.X)) - Math.PI / 2;

                            sprite.Move(t, t + timeStep, (int)lastX, (int)lastY, (int)nextX, (int)nextY);
                            sprite.Rotate(t, t + timeStep, currentAngle, newAngle);

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = (float)nextX;
                            lastY = (float)nextY;
                            lastAngle = angle;
                        }
                    }
                    if (i < EndTime - (FadeTimeIn + FadeTimeOut))
                    {
                        sprite.Fade(i, i + FadeTimeIn, 0, ParticleFade);
                        if (i < EndTime - RealTravelTime)
                        {
                            sprite.Fade(i + RealTravelTime - FadeTimeOut, i + RealTravelTime, ParticleFade, 0);
                        }
                        else
                        {
                            sprite.Fade(EndTime - FadeTimeOut, EndTime, ParticleFade, 0);
                        }
                    }
                    if (ScaleMin != ScaleMax)
                    {
                        if (ScaleMin == ScaleMax && ScaleMin != 1) sprite.ScaleVec(i, ScaleMin, ScaleMin);

                        var loopcount = RealTravelTime / (FlipInterval * 2);
                        sprite.ScaleVec(i, RandomScaling, RandomScaling);
                        sprite.StartLoopGroup(i, loopcount);
                        sprite.ScaleVec(OsbEasing.InSine, 0, FlipInterval, RandomScaling - 0.005, RandomScaling, 0, RandomScaling / 2);
                        sprite.ScaleVec(OsbEasing.OutSine, FlipInterval, FlipInterval * 2, 0, RandomScaling / 2, RandomScaling - 0.005, RandomScaling);
                        sprite.EndGroup();
                        if (i + FlipInterval * 2 * loopcount < EndTime) sprite.ScaleVec(OsbEasing.In, i + FlipInterval * 2 * loopcount, i + RealTravelTime, RandomScaling - 0.005, RandomScaling, 0, RandomScaling / 2);
                    }
                    else
                    {
                        sprite.ScaleVec(i, ScaleMin, ScaleMax);
                        if (ScaleMin == ScaleMax && ScaleMin != 1) sprite.ScaleVec(i, ScaleMin, ScaleMin);
                    }
                }
            }
        }
        private void GodRays(int startTime, int endTime)
        {
            for (int i = 0; i < 15; i++)
            {
                var pos = new Vector2(Random(-57, 697), -25);
                var sprite = GetLayer("").CreateSprite("sb/light.png", OsbOrigin.CentreLeft, pos);
                var rotateStart = MathHelper.DegreesToRadians(Random(80, 100));
                var rotateEnd = MathHelper.DegreesToRadians(Random(75, 115));
                int RandomDuration = Random(4000, 7000);
                var Fade = Random(0.3f, 0.5f);

                sprite.StartLoopGroup(startTime + i * 120, (endTime - startTime - i * 90) / (RandomDuration * 2));
                sprite.Fade(0, 1500, 0, Fade);
                sprite.Rotate(OsbEasing.InOutSine, 0, RandomDuration, rotateStart, rotateEnd);
                sprite.Rotate(OsbEasing.InOutSine, RandomDuration, RandomDuration * 2, rotateEnd, rotateStart);
                sprite.Fade(RandomDuration * 2 - 1500, RandomDuration * 2 - 1000, Fade, 0);
                sprite.EndGroup();

                sprite.Scale(startTime + i * 120, 0.73);
                sprite.Additive(startTime + i * 120);
            }
        }
    }
}
