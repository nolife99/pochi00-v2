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
    public class HitObjectGen : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GenerateRing(8, 6693, 27444);
            GenerateRing(12, 113360, 123944);
            GenerateRing(8, 189360, 199945);
            GenerateRing(8, 256385, 272833);
            GenerateRing(8, 325902, 332316);
            GenerateRing(8, 433889, 444555);
            GenerateHighlight(81150, 92694);
            GenerateHighlight(194694, 203360);
            GenerateHighlight(484555, 494972);
            GenerateBeam(81194, 82527);
            GenerateBeam(82694, 92027);
            GenerateBeam(412389, 423139);
            GenerateVerticalBar(575555, 585805);
            List<double> t23 = new List<double>();
            foreach(var hitobject in Beatmap.HitObjects)
            {
                if(hitobject.StartTime >= 380555 && hitobject.StartTime <= 401888)
                    GenerateSplash(hitobject.StartTime, hitobject.Position);
            }
        }
        public void GenerateRing(int BeatDivisor, int StartTime, int EndTime)
        {
            float StartScale = 0.5f;
            float EndScale = 0.8f;
            int FadeTime = 1000;
            float Fade = 1;
            OsbEasing Easing = OsbEasing.OutExpo;
            bool UseHitobjectColor = true;

            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) &&
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = hitobjectLayer.CreateSprite("sb/cf.png", OsbOrigin.Centre, hitobject.Position);
                sprite.Additive(hitobject.StartTime);
                sprite.Color(hitobject.StartTime, UseHitobjectColor ? hitobject.Color : Color4.White);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = sprite.PositionAt(startTime);
                        sprite.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));
                        sprite.Fade(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, Fade, 0);
                        sprite.Scale(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, StartScale, EndScale);

                        if (complete) break;
                        startTime += timestep;
                    }
                }
                else
                {
                    sprite.Fade(Easing, hitobject.StartTime, hitobject.EndTime + FadeTime, Fade, 0);
                    sprite.Scale(Easing, hitobject.StartTime, hitobject.EndTime + FadeTime, StartScale, EndScale);
                }
            }
        }
        private void GenerateHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.4, 0);
                    sprite.Scale(hitobject.StartTime, 0.25);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 11;
                        var sTime = hitobject.StartTime;
                        while (true)
                        {
                            var stepSprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.PositionAtTime(sTime));
                            stepSprite.Additive(sTime);
                            stepSprite.Fade(sTime, sTime + 1000, 0.3, 0);
                            stepSprite.Scale(sTime, 0.25);

                            if (sTime > hitobject.EndTime)
                                break;

                            sTime += timestep;
                        }
                    }
                }
            }
        }
        private void GenerateBeam(int startTime, int endTime)
        {
            double lastObject = 0;
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= startTime - 1 && hitobject.StartTime <= endTime + 1)
                {
                    if (hitobject.StartTime - lastObject > 1)
                    {
                        var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                        sprite.Rotate(hitobject.StartTime, (double)Random(-Math.PI / 8, Math.PI / 8));
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 5, 1000, 0, 1000);
                        sprite.Additive(hitobject.StartTime);
                        sprite.Fade(hitobject.StartTime, 0.5);
                    }
                    lastObject = hitobject.StartTime;
                }
            }
        }
        private void GenerateSplash(double startTime, Vector2 position)
        {
            var scaleY = Random(0.5, 1.5);
            var sprite = GetLayer("").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
            sprite.Fade(startTime, startTime + 1000, 0.5, 0);
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + 500, 0, position.Y/5000 * scaleY, position.Y/1000 * scaleY, position.Y/5000 * scaleY);
        }
        private void GenerateVerticalBar(int startTime, int endTime)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if (hitobject.StartTime >= startTime && hitobject.StartTime <= endTime)
                    {
                        var position = new Vector2(Random(0, 640), 240);
                        var sprite = GetLayer("Random Piano").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                        sprite.ScaleVec(hitobject.StartTime, 40, 400);
                        sprite.Fade(hitobject.StartTime, hitobject.StartTime + 500, 0.1, 0);
                        sprite.Additive(hitobject.StartTime);
                        sprite.Color(hitobject.StartTime, hitobject.Color);
                    }
                }
            }
        }
    }
}

