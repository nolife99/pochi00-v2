using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    class HitObjectGen : StoryboardObjectGenerator
    {
        OsbSpritePools pool;
        public override void Generate()
        {
            int[] linesTimes = {
                145345, 146678, 147345, 148011, 149345, 150011, 150678, 151345, 152011, 152678, 153011, 153345, 154678, 156011, 157345, 158011, 158678,
                159345, 160011, 160678, 161345, 162011, 162678, 163011, 163345, 164011, 164678, 165011, 165345, 148678, 149011, 155345, 157011, 157178,
                159678, 146345, 146511, 155345
            };
            using (pool = new OsbSpritePools(GetLayer("")))
            {
                pool.MaxPoolDuration = (int)AudioDuration;
                GenerateRing(6693, 27444);
                GenerateRing(113360, 123944);
                GenerateRing(189360, 199945);
                GenerateRing(256385, 272833);
                GenerateRing(325902, 332316);
                GenerateRing(433889, 444472);
                GenerateHighlight(81150, 92694);
                GenerateHighlight(194694, 203360);
                GenerateHighlight(484555, 494972);
                GenerateBeam(81194, 82527);
                GenerateBeam(82694, 92027);
                GenerateBeam(412389, 423139);
                PianoGenerator();
                ParticleBurst(linesTimes);
            }
            GenerateSplash(380555, 401888);
        }
        void GenerateRing(int StartTime, int EndTime)
        {
            float StartScale = 0.125f;
            float EndScale = 0.2f;
            int FadeTime = 1000;
            float Fade = 1;

            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= StartTime && hitobject.StartTime <= EndTime)
                {
                    var sprite = pool.Get(hitobject.StartTime, hitobject.EndTime + FadeTime, "sb/cf.png", OsbOrigin.Centre, true);
                    sprite.Color(hitobject.StartTime, hitobject.Color);
                    sprite.Move(hitobject.StartTime, hitobject.Position);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 12;
                        var startTime = hitobject.StartTime;
                        var s = hitobject as OsuSlider;

                        if (s.ControlPointCount > 1)
                        {
                            timestep = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 8;
                        }
                        else if (s.TravelDuration > 80 && s.RepeatCount > 0)
                        {
                            timestep = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 4;
                        }
                        else if (s.RepeatCount == 0 & s.ControlPointCount == 1)
                        {
                            sprite.Move(startTime, hitobject.EndTime, hitobject.Position, hitobject.PositionAtTime(hitobject.EndTime));
                        }
                        while (true && s.ControlPointCount > 1 | s.RepeatCount > 0)
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
                    if (hitobject is OsuSlider)
                    {
                        sprite.Fade(hitobject.StartTime, Fade);
                        sprite.Scale(hitobject.StartTime, StartScale);
                    }
                    sprite.Fade(OsbEasing.OutExpo, hitobject.EndTime, hitobject.EndTime + FadeTime, Fade, 0);
                    sprite.Scale(OsbEasing.OutExpo, hitobject.EndTime, hitobject.EndTime + FadeTime, StartScale, EndScale);
                }
            }
        }
        void GenerateHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime > startTime - 5 && hitobject.StartTime < endTime + 5)
                {
                    var sprite = pool.Get(hitobject.StartTime, hitobject.StartTime + 1000, "sb/hl.png", OsbOrigin.Centre, true);
                    sprite.Move(hitobject.StartTime, hitobject.Position);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                    sprite.Scale(hitobject.StartTime, 0.13);

                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                        var sTime = hitobject.StartTime + timestep;
                        while (true)
                        {
                            var stepSprite = pool.Get(sTime - 50, sTime + 1000, "sb/hl.png", OsbOrigin.Centre, true);
                            stepSprite.Move(sTime - 50, hitobject.PositionAtTime(sTime));
                            stepSprite.Fade(sTime - 50, sTime, 0, 0.4);
                            stepSprite.Fade(sTime, sTime + 1000, 0.4, 0);
                            stepSprite.Scale(sTime - 50, 0.13);

                            if (sTime > hitobject.EndTime) break;
                            sTime += timestep;
                        }
                    }
                }
            }
        }
        void GenerateVerticalBar(int[] a, int[] b, int[] c)
        {
            using (var pooling = new OsbSpritePool(GetLayer("PianoHighlights"), "sb/p.png", OsbOrigin.Centre, true))
            {
                pooling.MaxPoolDuration = (int)AudioDuration;
                for (int i = 0; i < Random(2, 4); i++)
                {
                    foreach (var hit in a)
                    {
                        var position = new Vector2(Random(-77, 727), 240);
                        var sprite = pooling.Get(hit, hit + 500);

                        sprite.Move(hit, position);
                        sprite.ScaleVec(hit, 40, 400);
                        sprite.Fade(hit, hit + 500, 0.025, 0);
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
                        var piano = pooling.Get(hit, hit + 500);

                        piano.ScaleVec(hit, 60, 400);
                        piano.Fade(hit, hit + 500, 0.1, 0);
                        piano.Move(hit, position);

                        if (hit >= 575721 & hit <= 577555 | hit == 579221 | hit == 579721 | hit == 580138 | hit >= 580555 & hit <= 581138 | hit == 581471 | hit >= 581888 & hit <= 582471 | hit == 582805 | hit >= 583221 & hit <= 583805 | hit == 584138 | hit >= 584555 & hit <= 585555)
                        {
                            piano.Color(hit, color1);
                        }
                        else if (hit >= 577888 & hit <= 578888 | hit == 579471 | hit == 579888 | hit == 580388 | hit == 580543 | hit == 581221 | hit == 581555 | hit == 581721 | hit == 582555 | hit == 583055 | hit == 583888 | hit == 584388)
                        {
                            piano.Color(hit, color2);
                        }
                        else if (hit == 585888)
                        {
                            piano.Color(hit, color3);
                        }
                        else if (hit == 586221)
                        {
                            piano.Color(hit, color4);
                        }
                        else
                        {
                            piano.Color(hit, color5);
                        }
                    }
                }
                for (double i = 0; i < 15; i++)
                {
                    foreach (var Hit in b)
                    {
                        var position = new Vector2(Random(-77, 727), 240);
                        var sprite = pooling.Get(Hit, Hit + 300);

                        sprite.ScaleVec(Hit, 10, 400);
                        sprite.Fade(Hit, Hit + 300, 0.03, 0);
                        sprite.Color(Hit, Color4.GreenYellow);
                        sprite.Move(Hit, position);
                    }
                }
            }
        }
        void GenerateBeam(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime >= startTime - 1 && hitobject.StartTime <= endTime + 1)
                {
                    int scaleY = 810;
                    if (startTime > 380555) scaleY = 730;

                    var sprite = pool.Get(hitobject.StartTime, hitobject.StartTime + 1000, "sb/p.png", OsbOrigin.Centre, true);
                    sprite.Move(hitobject.StartTime, hitobject.Position);
                    sprite.Rotate(hitobject.StartTime, Random(-Math.PI / 8, Math.PI / 8));
                    sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, 3, scaleY, 0, scaleY);
                    sprite.Fade(hitobject.StartTime, 0.8);
                }
            }
        }
        void GenerateSplash(int startTime, int endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer(""), "sb/c2.png", OsbOrigin.Centre, false))
            {
                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if (hitobject.StartTime >= startTime && hitobject.StartTime <= endTime)
                    {
                        var position = hitobject.Position;
                        double scaleY = Random(0.5, 1.3);
                        var sprite = pool.Get(hitobject.StartTime, hitobject.StartTime + 1000);
                        sprite.Move(hitobject.StartTime, position);
                        sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                        sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 500, 0, position.Y / 5000 * scaleY, position.Y / 1000 * scaleY, position.Y / 5000 * scaleY);
                    }
                }
            }
        }
        void PianoGenerator()
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
                575721, 575805, 575888, 576221, 576388, 576555, 576805, 577055, 577221, 577555, 577888, 578221,
                578305, 578388, 578471, 578555, 578888, 579221, 579471, 579721, 579888, 580138, 580388, 580555,
                580888, 580971, 581055, 581138, 581221, 581471, 581721, 581888, 582221, 582388, 582471, 582555,
                582805, 583055, 583221, 583555, 583721, 583805, 583888, 584138, 584388, 584555, 584805, 585055,
                585221, 585555, 585888, 586221, 586555, 586888
            };
            GenerateVerticalBar(a, b, c);
            GeneratePiano(359006, 376310);
        }
        void GeneratePiano(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((startTime != 0 || endTime != 0) &&
                    (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                    continue;

                var sprite = pool.Get(hitobject.StartTime, hitobject.StartTime + 3000, "sb/grad.png", OsbOrigin.CentreLeft, false);
                var sprite2 = pool.Get(hitobject.StartTime, hitobject.StartTime + 3000, "sb/grad.png", OsbOrigin.CentreLeft, false);

                sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite.Move(hitobject.StartTime, hitobject.Position.X, 450);
                sprite.Rotate(hitobject.StartTime, -Math.PI / 2);
                sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);

                sprite2.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 0.5, 0);
                sprite2.Move(hitobject.StartTime, hitobject.Position.X, 30);
                sprite2.Rotate(hitobject.StartTime, Math.PI / 2);
                sprite2.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 3000, 0.3, 0.5, 0.3, 0);
            }
        }
        void Piano(int time)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime <= time + 1 && hitobject.EndTime >= time - 1)
                {
                    var pos = hitobject.PositionAtTime(time);
                    var sprite = pool.Get(time, time + 3000, "sb/grad.png", OsbOrigin.CentreLeft, false);
                    var sprite2 = pool.Get(time, time + 3000, "sb/grad.png", OsbOrigin.CentreLeft, false);

                    sprite.Fade(time, time + 1000, 0.5, 0);
                    sprite.Move(time, pos.X, 450);
                    sprite.ScaleVec(OsbEasing.OutExpo, time, time + 3000, 0.3, 0.5, 0.3, 0);

                    sprite2.Fade(time, time + 1000, 0.5, 0);
                    sprite2.Move(time, pos.X, 30);
                    sprite2.ScaleVec(OsbEasing.OutExpo, time, time + 3000, 0.3, 0.5, 0.3, 0);

                    sprite.Rotate(time, -Math.PI / 2);
                    sprite2.Rotate(time, Math.PI / 2);
                }
            }
        }
        void ParticleBurst(int[] times)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                foreach (var time in times)
                {
                    if (hitobject.StartTime >= time && hitobject.StartTime <= time + 20)
                    {
                        GenerateLinesPlane((int)hitobject.StartTime, hitobject.Position, Random(0, 10) > 5 ? true : false);
                    }
                }
            }
        }
        void GenerateLinesPlane(int startTime, Vector2 position, bool direction)
        {
            var line = pool.Get(startTime, startTime + 2000, "sb/pl.png", OsbOrigin.CentreRight, true);
            line.Fade(startTime, startTime + 2000, 1, 0);
            line.ScaleVec(OsbEasing.OutExpo, startTime, startTime + 2000, 30, 2, 0, 0);
            line.MoveY(startTime, position.Y);
            line.MoveX(OsbEasing.OutExpo, startTime, startTime + 500, direction ? -250 : 1000, direction ? 1000 : -250);
            
            if (!direction) line.Rotate(startTime, Math.PI);
            else line.Rotate(startTime, 0);

            var hl = pool.Get(startTime, startTime + 1000, "sb/hl.png", OsbOrigin.Centre, true);
            hl.Fade(startTime, startTime + 1000, 1, 0);
            hl.Scale(OsbEasing.OutExpo, startTime, startTime + 1000, 0.1, 0.125);
            hl.Move(startTime, position);

            var circle = pool.Get(startTime, startTime + 1000, "sb/c2.png", OsbOrigin.Centre, true);
            circle.Fade(startTime, startTime + 1000, 1, 0);
            circle.Scale(OsbEasing.OutExpo, startTime, startTime + 1000, 0.3, 0.35);
            circle.Move(startTime, position);

            GenerateFairy(startTime, position, 1000, 3000);
        }
        void GenerateFairy(double startTime, Vector2 position, int durationMin, int durationMax)
        {
            for (int i = 0; i < 20; i++)
            {
                double angle = Random(0, Math.PI * 2);
                var radius = Random(10, 50);

                var endPosition = new Vector2(
                    (int)(position.X + Math.Cos(angle) * radius),
                    (int)(position.Y + Math.Sin(angle) * radius));

                var particleDuration = Random(durationMin, durationMax);
                var sprite = pool.Get(startTime, startTime + particleDuration, "sb/d.png", OsbOrigin.Centre, true);
                sprite.Fade(startTime, startTime + particleDuration, 1, 0);
                sprite.Scale(startTime, startTime + particleDuration, radius * 0.001, 0);
                sprite.Move(OsbEasing.OutExpo, startTime, startTime + particleDuration, position, endPosition);
            }
        }
    }
}

