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
        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public bool Circle = true;

        [Configurable]
        public bool Highlight = false;

        [Configurable]
        public bool Beam = false;

        [Configurable]
        public bool Splash = false;

        public override void Generate()
        {
            if(Circle)
                GenerateRing(BeatDivisor, StartTime, EndTime, "sb/cf.png", 0.5f, 0.8f, 1000, 1, OsbEasing.OutExpo, true);
            
            if(Highlight)
                GenerateKiaiHighlight(StartTime, EndTime);

            if(Beam)
                GenerateBeam(StartTime, EndTime);
            
            List<double> t23 = new List<double>();
            foreach(var hitobject in Beatmap.HitObjects)
            {
                if(Splash)
                {
                    if(hitobject.StartTime >= StartTime && hitobject.StartTime <= EndTime)
                    GenerateSplash(hitobject.StartTime, hitobject.Position);
                }
            }
        }
        public void GenerateRing(int BeatDivisor, int StartTime, int EndTime, string SpritePath, float StartScale, float EndScale, int FadeTime, float Fade, OsbEasing Easing, bool UseHitobjectColor)
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) &&
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                sprite.Additive(hitobject.StartTime, hitobject.EndTime + FadeTime);
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
        private void GenerateKiaiHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime, hitobject.StartTime + 1000);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.25, 0);
                    sprite.Scale(hitobject.StartTime, 0.22);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 12;
                        var sTime = hitobject.StartTime;
                        while (true)
                        {
                            var stepSprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.PositionAtTime(sTime));
                            stepSprite.Additive(sTime, sTime + 1000);
                            stepSprite.Fade(sTime, sTime + 1000, 0.25, 0);
                            stepSprite.Scale(sTime, 0.22);

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
                if (hitobject.StartTime >= startTime && hitobject.StartTime <= endTime)
                {
                    if (hitobject.StartTime - lastObject > 1)
                    {
                        var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                        sprite.Rotate(hitobject.StartTime, (float)Random(-Math.PI / 8, Math.PI / 8));
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 5, 1000, 0, 1000);
                        sprite.Additive(hitobject.StartTime, hitobject.StartTime + 1000);
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
    }
}

