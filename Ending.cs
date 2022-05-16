using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Ending : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Birds(3500, 7000, 20, 30, 587221, 608555, 35, false, true, 0.02, 0.05);
            Birds(3500, 7000, 20, 30, 587221, 608555, 35, true, false, 0.02, 0.05);

            GodRays(587221, 614000);

            Scripts particleManager = new Scripts(this);
            particleManager.GenerateLights(608555, 628888, 0.08);

            var p = new int[]{
                575710, 575793, 575877, 576210, 576377, 576543, 576793, 577043, 577210, 577543, 577877, 578210,
                578293, 578377, 578460, 578543, 578877, 579210, 579460, 579710, 579877, 580127, 580377, 580543,
                580877, 580960, 581043, 581127, 581210, 581460, 581710, 581877, 582210, 582377, 582460, 582543,
                582793, 583043, 583210, 583543, 583710, 583793, 583877, 584127, 584377, 584543, 584793, 585043,
                585210, 585543, 585877, 586210, 586543, 586877
            };
            GenerateVerticalBar(p);
        }
        public void Birds(int MinDuration, int MaxDuration, int FlyingSpeed, int Acceleration, int StartTime, int EndTime, int SpriteAmount, bool right, bool left, double ScaleMin, double ScaleMax)
        {
            Vector2 StartPosition = new Vector2(320, 260);
            Vector2 EndPosition = new Vector2(320, 380);
            OsbOrigin ParticleOrigin = OsbOrigin.Centre;
            string ParticlePath = "sb/bird.png";
            bool RandomParticleFade = false;
            double ParticleFadeMin = 1;
            double ParticleFadeMax = 1;
            int FadeTimeIn = 2000;
            int FadeTimeOut = 700;
            bool RandomScale = true;
            bool RandomDuration = true;
            int NewColorEvery = 1;
            Color4 Color = Color4.White;
            Color4 Color2 = Color4.LightGray;

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
                    int sVec = FlyingSpeed * 5;
                    double vX = Math.Cos(rVec) * sVec;
                    double vY = Math.Sin(rVec) * sVec;
                    double lastAngle = 90;
                    var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 0.5;
                    sprite.Additive(i);

                    for (var t = i; t < i + RealTravelTime; t += (int)timeStep)
                    {
                        if (right)
                        {
                            var nextX = lastX + vX;
                            var nextY = lastY - (vY / 7.5);

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
        private void GodRays(int startTime, int endTime)
        {
            for (int i = 0; i < 15; i++)
            {
                var pos = new Vector2(Random(-57, 697), -25);
                var sprite = GetLayer("").CreateSprite("sb/light.png", OsbOrigin.CentreLeft, pos);
                var rotateStart = MathHelper.DegreesToRadians(Random(80, 100));
                var rotateEnd = MathHelper.DegreesToRadians(Random(75, 115));
                int RandomDuration = Random(4000, 7000);
                var Fade = Random(0.3, 0.5);

                sprite.StartLoopGroup(startTime + i * 120, (endTime - startTime - i * 90) / (RandomDuration * 2));
                sprite.Fade(0, 1500, 0, Fade);
                sprite.Rotate(0, RandomDuration, rotateStart, rotateEnd);
                sprite.Rotate(RandomDuration, RandomDuration * 2, rotateEnd, rotateStart);
                sprite.Fade(RandomDuration * 2 - 1500, RandomDuration * 2 - 1000, Fade, 0);
                sprite.EndGroup();

                sprite.Scale(startTime + i * 120, 0.73);
                sprite.Additive(startTime + i * 120);
            }
        }
        private void GenerateVerticalBar(int[] l)
        {
            for (int i = 0; i < Random(1, 3); i++)
            {
                foreach (var hit in l)
                {
                    var position = new Vector2(Random(-77, 727), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(hit, 60, 400);
                    sprite.Fade(hit, hit + 500, 0.1, 0);
                    sprite.Additive(hit);

                    foreach (var hitobject in Beatmap.HitObjects)
                    {
                        if ((hit != 0 || hit + 500 != 0) &&
                        (hitobject.StartTime < hit - 5 || hit + 500 - 5 <= hitobject.StartTime))
                            continue;

                        sprite.Color(hit, hitobject.Color);
                    }
                }
            }
        }
    }
}
