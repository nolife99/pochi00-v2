using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    public class HitObjectGen : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GenerateRing(8, 6693, 27444);
            //danmaku section
            GenerateRing(8, 113360, 115277);
            GenerateRing(12, 115360, 115944);
            GenerateRing(8, 116027, 117277);
            GenerateRing(12, 117360, 118277);
            GenerateRing(8, 118360, 118944);
            GenerateRing(12, 119027, 120444);
            GenerateRing(8, 120527, 123444);
            GenerateRing(12, 123527, 123860);

            GenerateRing(8, 189360, 199945);
            GenerateRing(8, 256385, 272833);
            GenerateRing(8, 325902, 332316);
            GenerateRing(8, 433889, 444472);
            GenerateHighlight(81150, 92694);
            GenerateHighlight(194694, 203360);
            GenerateHighlight(484555, 494972);
            GenerateBeam(81194, 82527);
            GenerateBeam(82694, 92027);
            GenerateBeam(412389, 423139);
            GenerateSplash(380555, 401888);

            PianoGenerator();
        }
        public void GenerateRing(int BeatDivisor, int StartTime, int EndTime)
        {
            float StartScale = 0.125f;
            float EndScale = 0.2f;
            int FadeTime = 1000;
            float Fade = 1;
            OsbEasing Easing = OsbEasing.OutExpo;

            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= StartTime && hitobject.StartTime <= EndTime)
                {
                    var sprite = hitobjectLayer.CreateSprite("sb/cf.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime);
                    sprite.Color(hitobject.StartTime, hitobject.Color);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                        var startTime = hitobject.StartTime;
                        var s = hitobject as OsuSlider;
                        if (s.ControlPointCount > 1 | s.TravelDuration < 30)
                        {
                            while (hitobject is OsuSlider)
                            {
                                var endTime = startTime + timestep;

                                var complete = hitobject.EndTime - endTime < 5;
                                if (complete) endTime = hitobject.EndTime;

                                var startPosition = sprite.PositionAt(startTime);
                                sprite.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));

                                if (complete) break;
                                startTime += timestep;
                            }
                        }
                        else if (s.TravelDuration > 50 & s.RepeatCount != 0)
                        {
                            while (hitobject is OsuSlider)
                            {
                                var tStep = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 4;
                                var end = startTime + tStep;

                                var complete = hitobject.EndTime - end < 5;
                                if (complete) end = hitobject.EndTime;

                                var startPosition = sprite.PositionAt(startTime);
                                sprite.Move(startTime, end, startPosition, hitobject.PositionAtTime(end));

                                if (complete) break;
                                startTime += tStep;
                            }
                        }
                        else
                        {
                            sprite.Move(hitobject.StartTime, hitobject.EndTime, hitobject.PositionAtTime(startTime), hitobject.PositionAtTime(hitobject.EndTime));
                        }
                    }
                    sprite.Fade(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, Fade, 0);
                    sprite.Scale(Easing, hitobject.EndTime, hitobject.EndTime + FadeTime, StartScale, EndScale);
                }
            }
        }
        public void GenerateHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Additive(hitobject.StartTime);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.4, 0);
                    sprite.Scale(hitobject.StartTime, 0.1175);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 14;
                        var sTime = hitobject.StartTime;
                        while (true)
                        {
                            var stepSprite = GetLayer("").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitobject.PositionAtTime(sTime));
                            stepSprite.Additive(sTime);
                            stepSprite.Fade(sTime, sTime + 1000, 0.35, 0);
                            stepSprite.Scale(sTime, 0.1125);

                            if (sTime > hitobject.EndTime)
                                break;

                            sTime += timestep;
                        }
                    }
                }
            }
        }
        private void GenerateVerticalBar(int[] a, int[] b, int[] c)
        {
            for (int i = 0; i < Random(2, 4); i++)
            {
                foreach (var hit in a)
                {
                    var position = new Vector2(Random(-77, 727), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(hit, 40, 400);
                    sprite.Fade(hit, hit + 500, 0.025, 0);
                    sprite.Additive(hit);
                    sprite.Color(hit, Color4.LightBlue);
                }
                foreach (var hit in c)
                {
                    var color1 = new Color4(91, 206, 164, 1);
                    var color2 = new Color4(129, 87, 86, 1);
                    var color3 = new Color4(204, 49, 96, 1);
                    var color4 = new Color4(250, 169, 80, 1);
                    var color5 = new Color4(63, 220, 248, 1);
                    var position = new Vector2(Random(-77, 727), 240);
                    var piano = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    piano.ScaleVec(hit, 60, 400);
                    piano.Fade(hit, hit + 500, 0.1, 0);
                    piano.Additive(hit);

                    if (hit > 575638 & hit < 577805 | hit > 579138 & hit < 579305 | hit > 579638 & hit < 579805 | hit > 580055 & hit < 580221 | hit > 580471 & hit < 581217 | hit > 581388 & hit < 581555 | hit > 581805 & hit < 582467 | hit > 582721 & hit < 582888 | hit > 583138 & hit < 583845 | hit > 584055 & hit < 584221 | hit > 584471 & hit < 585638)
                    {
                        piano.Color(hit, color1);
                    }
                    else if (hit > 577805 & hit < 579138 | hit > 579305 & hit < 579638 | hit > 579805 & hit < 580055 | hit > 580221 & hit < 580471 | hit > 581217 & hit < 581388 | hit > 581555 & hit < 581805 | hit > 582477 & hit < 582638 | hit > 582888 & hit < 583138 | hit > 583846 & hit < 583971 | hit > 584221 & hit < 584471)
                    {
                        piano.Color(hit, color2);
                    }
                    else if (hit > 585805 & hit < 585971)
                    {
                        piano.Color(hit, color3);
                    }
                    else if (hit > 586138 & hit < 586305)
                    {
                        piano.Color(hit, color4);
                    }
                    else
                    {
                        piano.Color(hit, color5);
                    }
                }
            }
            for (double i = 0; i < 17; i++)
            {
                foreach (var Hit in b)
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
        public void GenerateBeam(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= startTime - 1 && hitobject.StartTime <= endTime + 1)
                {
                    int scaleY = 810;
                    if (startTime > 380555)
                    {
                        scaleY = 730;
                    }
                    var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                    sprite.Rotate(hitobject.StartTime, Random(-Math.PI / 8, Math.PI / 8));
                    sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 3, scaleY, 0, scaleY);
                    sprite.Additive(hitobject.StartTime);
                    sprite.Fade(hitobject.StartTime, 0.8);
                }
            }
        }
        public void GenerateSplash(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
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
        public void PianoGenerator()
        {
            var times = new int[]{
                484555, 485055, 485139, 485222, 485555, 485722, 485889, 486139, 486389, 486555, 487222, 487722,
                487805, 487889, 488222, 488389, 488555, 488805, 489055, 489222, 489555, 489722, 489889,
                490389, 490472, 490555, 490889, 491222, 491472, 491722, 491889, 492555, 493055, 493139, 493222,
                493555, 493722, 493889, 494139, 494389, 494555, 494889
            };
            foreach (var time in times)
            {
                Piano(time);
            }
            var d = 134678 - 124011;
            var a = new int[]{
                124011, 124178, 124345, 124511, 124845, 125178, 125345, 125511, 125678, 125845, 126178, 126511,
                126678, 127011, 127178, 127511, 127678, 127845, 128011, 128345, 128511, 128845, 129345, 129511,
                129678, 129845, 130178, 130345, 130511, 130678, 131011, 131178, 131678, 131845, 132011, 132178,
                132345, 132511, 132845, 133011, 133178, 133345, 133511, 133678, 133845,
                124011 + d, 124178 + d, 124345 + d, 124511 + d, 124845 + d, 125178 + d, 125345 + d, 125511 + d, 125678 + d, 125845 + d, 126178 + d, 126511 + d,
                126678 + d, 127011 + d, 127178 + d, 127511 + d, 127678 + d, 127845 + d, 128011 + d, 128345 + d, 128511 + d, 128845 + d, 129345 + d, 129511 + d,
                129678 + d, 129845 + d, 130178 + d, 130345 + d, 130511 + d, 130678 + d, 131011 + d, 131178 + d, 131678 + d, 131845 + d, 132011 + d, 132178 + d,
                132345 + d, 132511 + d, 132845 + d, 133011 + d, 133178 + d, 133345 + d, 133511 + d, 133678 + d, 133845 + d,
            };
            var b = new int[]{
                134345, 134428, 134511, 134595
            };
            var c = new int[]{
                575710, 575793, 575877, 576210, 576377, 576543, 576793, 577043, 577210, 577543, 577877, 578210,
                578293, 578377, 578460, 578543, 578877, 579210, 579460, 579710, 579877, 580127, 580377, 580543,
                580877, 580960, 581043, 581127, 581210, 581460, 581710, 581877, 582210, 582377, 582460, 582543,
                582793, 583043, 583210, 583543, 583710, 583793, 583877, 584127, 584377, 584543, 584793, 585043,
                585210, 585555, 585888, 586221, 586555, 586888
            };
            GenerateVerticalBar(a, b, c);
            GeneratePiano(359006, 376310);
        }
        public void GeneratePiano(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((startTime != 0 || endTime != 0) &&
                    (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(hitobject.Position.X, 450));
                var sprite2 = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(hitobject.Position.X, 30));

                sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite.Rotate(hitobject.StartTime, -Math.PI / 2);
                sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);

                sprite2.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite2.Rotate(hitobject.StartTime, Math.PI / 2);
                sprite2.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);
            }
        }
        private void Piano(int time)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime <= time + 1 && hitobject.EndTime >= time - 1)
                {
                    var pos = hitobject.PositionAtTime(time);
                    var sprite = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(pos.X, 450));
                    var sprite2 = GetLayer("").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(pos.X, 30));

                    sprite.Fade(time, time + 1000, 0.5, 0);
                    sprite.ScaleVec(OsbEasing.OutExpo, time, time + 3000, 0.3, 0.5, 0.3, 0);

                    sprite2.Fade(time, time + 1000, 0.5, 0);
                    sprite2.ScaleVec(OsbEasing.OutExpo, time, time + 3000, 0.3, 0.5, 0.3, 0);

                    sprite.Rotate(time, -Math.PI / 2);
                    sprite2.Rotate(time, Math.PI / 2);
                }
            }
        }
    }
}

