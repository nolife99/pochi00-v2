using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Birds : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            FlyingBirds(3500, 7000, 20, 30, 587210, 608543, 25, false, true, 0.02, 0.05);
            FlyingBirds(3500, 7000, 20, 30, 587210, 608543, 25, true, false, 0.02, 0.05);
        }
        public void FlyingBirds(int MinDuration, int MaxDuration, int FlyingSpeed, int Acceleration, int StartTime, int EndTime, int SpriteAmount, bool right, bool left, double ScaleMin, double ScaleMax)
        {
            Vector2 StartPosition = new Vector2(320, 260);
            Vector2 EndPosition = new Vector2(320, 380);
            OsbOrigin ParticleOrigin = OsbOrigin.Centre;
            string ParticlePath = "sb/bird.png";
            bool RandomParticleFade = false;
            double ParticleFadeMin = 1;
            double ParticleFadeMax = 1;
            int FadeTimeIn = 2000;
            int FadeTimeOut = 500;
            bool RandomScale = true;
            bool RandomDuration = true;
            bool Additive = false;
            int NewColorEvery = 1;
            Color4 Color = Color4.White;
            Color4 Color2 = Color4.White;

            if (StartTime == EndTime)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }

            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

            var layer = GetLayer("");
            using (var pool = new OsbSpritePool(layer, ParticlePath, ParticleOrigin, (sprite, startTime, endTime) =>
            { }))
            {
                var RealTravelTime = RandomDuration ? Random(MinDuration, MaxDuration) : MinDuration;
                for (int i = StartTime; i < EndTime; i += RealTravelTime / SpriteAmount)
                {
                    var sprite = pool.Get(i, i + RealTravelTime);

                    double RandomScaling = Random(ScaleMin, ScaleMax);
                    int FlipInterval = Random(FlyingSpeed * 12, Acceleration * 8);
                    float lastX = Random(StartPosition.X, EndPosition.X);
                    float lastY = Random(StartPosition.Y, EndPosition.Y);
                    float rVec = MathHelper.DegreesToRadians(Random(360));
                    int sVec = FlyingSpeed * 2;
                    double vX = Math.Cos(rVec) * sVec;
                    double vY = Math.Sin(rVec) * sVec;
                    double lastAngle = 0;
                    float timeStep = FlipInterval * Random(0.45f, 4);

                    for (var t = i; t < i + RealTravelTime; t += (int)timeStep)
                    {
                        if (right)
                        {
                            var nextX = lastX + vX;
                            var nextY = lastY - (vY / 10);

                            double currentAngle = sprite.RotationAt(t);
                            double newAngle = Math.Atan2((nextY - lastY), (nextX - lastX)) + (Math.PI / 2);

                            var startPosition = new Vector2d(lastX, lastY);
                            var endPosition = new Vector2d(lastX, lastY);

                            double angle = Math.Atan2((startPosition.Y - endPosition.Y), (startPosition.X - endPosition.X)) - Math.PI / 2f;

                            sprite.Move(t, t + timeStep, lastX, lastY, nextX, nextY);
                            sprite.Rotate(t, newAngle);

                            if (currentAngle > MathHelper.RadiansToDegrees(0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            if (currentAngle < MathHelper.RadiansToDegrees(-0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = (float)nextX;
                            lastY = (float)nextY;
                            lastAngle = angle;
                        }
                        else if (left)
                        {
                            var nextX = lastX - vX;
                            var nextY = lastY - (vY / 15);

                            var currentAngle = sprite.RotationAt(t);
                            var newAngle = Math.Atan2((nextY - lastY), (nextX - lastX)) + (Math.PI / 2);

                            var startPosition = new Vector2d(lastX, lastY);
                            var endPosition = new Vector2d(lastX, lastY);

                            var angle = Math.Atan2((startPosition.Y - endPosition.Y), (startPosition.X - endPosition.X)) - Math.PI / 2f;

                            sprite.Move(t, t + timeStep, lastX, lastY, nextX, nextY);
                            sprite.Rotate(t, newAngle);
                            sprite.FlipH(t, t + timeStep);

                            if (currentAngle > MathHelper.RadiansToDegrees(0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            if (currentAngle < MathHelper.RadiansToDegrees(-0.05))
                            {
                                sprite.Rotate(OsbEasing.OutQuad, t, t + timeStep, currentAngle, newAngle);
                            }

                            else
                            {
                                sprite.Rotate(t + timeStep, newAngle);
                            }

                            vX += Random(FlyingSpeed) * timeStep / 1000;
                            vY += Random(FlyingSpeed) * timeStep / 1000;

                            lastX = (float)nextX;
                            lastY = (float)nextY;
                            lastAngle = angle;
                        }
                    }
                    var ParticleFade = RandomParticleFade ? Random(ParticleFadeMin, ParticleFadeMax) : ParticleFadeMin;

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

                    else
                    {
                        sprite.Fade(i, 0);
                    }

                    if (i % NewColorEvery == 1)
                    {
                        sprite.Color(i, Color);
                    }

                    else
                    {
                        sprite.Color(i, Color2);
                    }

                    if (Additive)
                    {
                        sprite.Additive(i, i + RealTravelTime);
                    }

                    if (ScaleMin != ScaleMax)
                    {
                        if (RandomScale)
                        {
                            if (ScaleMin != ScaleMax) 
                            {
                                if (RandomScale) 
                                {
                                    if (ScaleMin == ScaleMax && ScaleMin != 1)
                                        sprite.ScaleVec(i, ScaleMin, ScaleMin);

                                    var loopcount = RealTravelTime / (FlipInterval * 2);

                                    sprite.ScaleVec(i, RandomScaling, RandomScaling);
                                    sprite.StartLoopGroup(i, loopcount);
                                    sprite.ScaleVec(OsbEasing.In, 0, FlipInterval, RandomScaling - 0.005, RandomScaling, 0, RandomScaling / 2);
                                    sprite.ScaleVec(OsbEasing.Out, FlipInterval, FlipInterval * 2, 0, RandomScaling / 2, RandomScaling - 0.005, RandomScaling);
                                    sprite.EndGroup();
        
                                    sprite.ScaleVec(OsbEasing.In, i + FlipInterval * 2 * loopcount, i + RealTravelTime, RandomScaling - 0.005, RandomScaling, 0, RandomScaling / 2); // Workaround to prevent the sprite stopping
                                    } 
                                    else 
                                    {
                                        sprite.ScaleVec(i, ScaleMin, ScaleMax);

                                        if (ScaleMin == ScaleMax && ScaleMin != 1)
                                            sprite.ScaleVec(i, ScaleMin, ScaleMin);
                                    }
                                }
                            }
                            else
                            {
                                sprite.ScaleVec(i, ScaleMin, ScaleMax);

                                if (ScaleMin == ScaleMax && ScaleMin != 1)
                                    sprite.ScaleVec(i, ScaleMin, ScaleMin);
                        }
                    }
                }
            }
        }
    }
}
