using OpenTK;
using OpenTK.Graphics;
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
            GenerateSplash(380555, 401888);
            var d = 134678 - 124011;
            var PianoHits = new int[]{
                124011, 124178, 124345, 124511, 124845, 125178, 125345, 125511, 125678, 125845, 126178, 126511,
                126678, 127011, 127178, 127511, 127678, 127845, 128011, 128345, 128511, 128845, 129345, 129511,
                129678, 129845, 130178, 130345, 130511, 130678, 131011, 131178, 131678, 131845, 132011, 132178,
                132345, 132511, 132845, 133011, 133178, 133345, 133511, 133678, 133845,
                124011 + d, 124178 + d, 124345 + d, 124511 + d, 124845 + d, 125178 + d, 125345 + d, 125511 + d, 125678 + d, 125845 + d, 126178 + d, 126511 + d,
                126678 + d, 127011 + d, 127178 + d, 127511 + d, 127678 + d, 127845 + d, 128011 + d, 128345 + d, 128511 + d, 128845 + d, 129345 + d, 129511 + d,
                129678 + d, 129845 + d, 130178 + d, 130345 + d, 130511 + d, 130678 + d, 131011 + d, 131178 + d, 131678 + d, 131845 + d, 132011 + d, 132178 + d,
                132345 + d, 132511 + d, 132845 + d, 133011 + d, 133178 + d, 133345 + d, 133511 + d, 133678 + d, 133845 + d,
            };
            var DumHits = new int[]{
                134345, 134428, 134511, 134595
            };
            GenerateVerticalBar(PianoHits, DumHits);
        }
        public void GenerateRing(int BeatDivisor, int StartTime, int EndTime)
        {
            var Map = GetBeatmap("Chronostasis");
            float StartScale = 0.5f;
            float EndScale = 0.8f;
            int FadeTime = 1000;
            float Fade = 1;
            OsbEasing Easing = OsbEasing.OutExpo;
            bool UseHitobjectColor = true;

            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Map.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) &&
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = hitobjectLayer.CreateSprite("sb/cf.png", OsbOrigin.Centre, hitobject.Position);
                sprite.Additive(hitobject.StartTime);
                sprite.Color(hitobject.StartTime, UseHitobjectColor ? hitobject.Color : Color4.White);

                if (hitobject is OsuSlider)
                {
                    var timestep = Map.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
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
            var Map = GetBeatmap("Chronostasis");
            foreach (var hitobject in Map.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.4, 0);
                    sprite.Scale(hitobject.StartTime, 0.232);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Map.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 13;
                        var sTime = hitobject.StartTime;
                        while (true)
                        {
                            var stepSprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.PositionAtTime(sTime));
                            stepSprite.Additive(sTime);
                            stepSprite.Fade(sTime, sTime + 1000, 0.3, 0);
                            stepSprite.Scale(sTime, 0.232);

                            if (sTime > hitobject.EndTime)
                                break;

                            sTime += timestep;
                        }
                    }
                }
            }
        }
        private void GenerateVerticalBar(int[] pianoHits, int[] DumHits)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (var hit in pianoHits)
                {
                    var position = new Vector2(Random(-77, 727), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(hit, 40, 400);
                    sprite.Fade(hit, hit + 500, 0.025, 0);
                    sprite.Additive(hit);
                    sprite.Color(hit, Color4.LightBlue);
                }
            }

            for (double i = 0; i < 17.5; i++)
            {
                foreach (var Hit in DumHits)
                {
                    var position = new Vector2(Random(-77, 727), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(Hit, 10, 400);
                    sprite.Fade(Hit, Hit + 300, 0.03, 0);
                    sprite.Additive(Hit);
                    sprite.Color(Hit, Color4.GreenYellow);
                }
            }
        }
        private void GenerateBeam(int startTime, int endTime)
        {
            double lastObject = 0;
            var Map = GetBeatmap("Chronostasis");
            foreach (var hitobject in Map.HitObjects)
            {
                if (hitobject.StartTime >= startTime - 1 && hitobject.StartTime <= endTime + 1)
                {
                    if (hitobject.StartTime - lastObject > 1)
                    {
                        int scaleY = 810;
                        if (startTime > 380555)
                        {
                            scaleY = 730;
                        }
                        var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                        sprite.Rotate(hitobject.StartTime, Random(-Math.PI / 8, Math.PI / 8));
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 5, scaleY, 0, scaleY);
                        sprite.Additive(hitobject.StartTime);
                        sprite.Fade(hitobject.StartTime, 0.5);
                    }
                    lastObject = hitobject.StartTime;
                }
            }
        }
        private void GenerateSplash(int startTime, int endTime)
        {
            var Map = GetBeatmap("Chronostasis");
            foreach (var hitobject in Map.HitObjects)
            {
                if (hitobject.StartTime >= startTime && hitobject.StartTime <= endTime)
                {
                    var position = hitobject.Position;
                    double scaleY = Random(0.5, 1.3);
                    var sprite = GetLayer("").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                    sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 500, 0, position.Y / 5000 * scaleY, position.Y / 1000 * scaleY, position.Y / 5000 * scaleY);
                }
            }
        }
    }
}

