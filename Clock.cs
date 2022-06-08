using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    class Clock : StoryboardObjectGenerator
    {
        OsbSprite[] cadrant = new OsbSprite[60];
        OsbSprite bigHand;
        OsbSprite littleHand;
        OsbSprite center;
        OsbSprite background;
        int currentScale = 600;
        public override void Generate()
        {
            double beat = Beatmap.GetTimingPointAt(6693).BeatDuration;
            GenerateClock();
            ShowClock(6693, 28110, 92027, 97360, 0.2f);
            SetClockSpeed(6693, 48110, beat * 4);
            SetClockSpeed(49360, 81360, beat * 2);
            ModifyScale(6777, 28027, 250, true, currentScale);
            ModifyScale(28110, 33360, 200, true, 250);
            ModifyScale(70694, 80027, 150, true, 200);
            ModifyScale(80027, 81360, 200, true, 150);
            SetClockSpeed(81360, 92027, beat);
            SetClockSpeed(92027, 97360, beat * 2);
            SetClockSpeed(168027, 200027, beat);
            SetClockSpeed(200027, 203360, beat * 2);

            ShowClock(168027, 169360, 200027, 203360, 0.2f);

            bigHand.Rotate(203420, 0 - 16 * ((Math.PI * 2) / 60));
            beat = Beatmap.GetTimingPointAt(203420).BeatDuration;
            ShowClock(203420, 216661, 276247, 276247, 1, false);
            ShowClock(289488, 302730, 328799, 331695, 1, false);
            SetClockSpeed(203420, 216660, beat * 2);
            SetClockSpeed(289488, 302317, beat);
            ModifyScale(203420, 216661, 100, false);
            ChangeHour(216661, 217488, 1, OsbEasing.OutExpo);
            ChangeHour(217488, 217902, -0.5, OsbEasing.OutExpo);
            ChangeHour(217902, 218316, 1, OsbEasing.OutExpo);
            ChangeHour(218316, 219144, 1, OsbEasing.OutElastic);
            ChangeHour(219144, 219557, 0.5, OsbEasing.InExpo);
            ChangeHour(219557, 219971, -0.5, OsbEasing.InExpo);
            ChangeHour(219971, 220799, 1, OsbEasing.OutSine);
            ChangeHour(220799, 221213, 1, OsbEasing.OutExpo);
            ChangeHour(221213, 221626, -1, OsbEasing.OutExpo);
            ChangeHour(221626, 222454, -2, OsbEasing.OutExpo);
            ChangeHour(222454, 222661, 0.2, OsbEasing.OutExpo);
            ChangeHour(222661, 222868, 0.3, OsbEasing.OutExpo);
            ChangeHour(222868, 223075, 0.4, OsbEasing.OutExpo);
            ChangeHour(223075, 223282, 0.7, OsbEasing.OutExpo);
            ChangeHour(223282, 224109, -1, OsbEasing.InExpo);
            ChangeHour(224109, 224523, 1, OsbEasing.OutExpo);
            ChangeHour(224523, 224937, 1, OsbEasing.In);
            ChangeHour(224937, 225764, 1, OsbEasing.InExpo);
            ChangeHour(225764, 226075, 0.2, OsbEasing.OutExpo);
            ChangeHour(226075, 226385, 0.2, OsbEasing.OutExpo);
            ChangeHour(226385, 226592, 0.2, OsbEasing.OutExpo);
            ChangeHour(226592, 227420, 0.2, OsbEasing.InOutExpo);
            ChangeHour(227420, 227833, 1, OsbEasing.OutExpo);
            ChangeHour(227833, 228247, -1, OsbEasing.OutExpo);
            ChangeHour(228247, 229075, 2, OsbEasing.OutExpo);
            ChangeHour(229075, 229488, 1, OsbEasing.OutExpo);
            ChangeHour(229488, 229902, -0.755, OsbEasing.InExpo);

            ShowHours(229902, 272937, 120);
            SetClockSpeed(229902, 243144, beat * 4);
            SetClockSpeed(243144, 256385, beat * 2);

            ChangeHour(256385, 257213, 1, OsbEasing.InExpo);
            ChangeHour(258040, 258868, -1, OsbEasing.InExpo);
            ChangeHour(259695, 260109, 0.2, OsbEasing.OutExpo);
            ChangeHour(260109, 260523, 0.3, OsbEasing.OutExpo);
            ChangeHour(260523, 260937, 0.2, OsbEasing.OutExpo);
            ChangeHour(260937, 261351, 0.1, OsbEasing.OutExpo);
            ChangeHour(261351, 263006, 0.05, OsbEasing.OutExpo);
            ChangeHour(263006, 263833, 1, OsbEasing.InExpo);
            ChangeHour(264661, 265488, -1, OsbEasing.InExpo);
            ChangeHour(266316, 267144, 0.25, OsbEasing.InExpo);
            ChangeHour(267144, 267557, 0.75, OsbEasing.OutExpo);
            ChangeHour(267557, 267971, -0.5, OsbEasing.OutExpo);
            ChangeHour(267971, 268799, 0.5, OsbEasing.InExpo);
            ChangeHour(268799, 269213, 0.2, OsbEasing.OutExpo);
            ChangeHour(269213, 269626, 0.4, OsbEasing.OutExpo);
            ChangeHour(269626, 270454, 0.4, OsbEasing.OutSine);
            ChangeHour(270454, 270868, -0.4, OsbEasing.OutSine);
            ChangeHour(270868, 271282, 0.2, OsbEasing.OutSine);
            ChangeHour(271282, 272109, 0.2, OsbEasing.OutExpo);
            ChangeHour(272109, 272523, 0.4, OsbEasing.OutExpo);
            ChangeHour(272523, 272937, 0.011, OsbEasing.OutExpo);
            SetClockSpeed(272937, 274488, beat * 2);
            ChangeHour(274592, 276247, -1.5, OsbEasing.InExpo);

            ModifyScale(289488, 302730, 200, false);
            ChangeHour(302730, 309351, 6, OsbEasing.InSine);
            ChangeHour(309351, 315971, 8, OsbEasing.OutSine);
            SetClockSpeed(315971, 325902, beat);
            SetClockSpeed(325902, 328385, beat * 2);
            ChangeHour(328799, 332523, -0.1, OsbEasing.OutExpo);
            ModifyScale(328799, 332523, 100, false);
            ShowHours(302730, 325902, 230);

            beat = Beatmap.GetTimingPointAt(380555).BeatDuration;
            bigHand.Rotate(423221, 0);
            littleHand.Rotate(423221, 15 * Math.PI * 2 / 60);
            ShowClock(423222, 424555, 444555, 445889, 0.3f);
            ModifyScale(423222, 433889, 150);
            SetClockSpeed(423222, 445889, beat * 2);

            beat = Beatmap.GetTimingPointAt(473889).BeatDuration;

            ShowClock(501889, 503222, 527221, 528555, 1, false);
            SetClockSpeed(501889, 527221, beat * 2);
            ModifyScale(500555, 500555, 100);
            ShowHours(501889, 527221, 120);

            GearParts();
        }
        private void GenerateClock()
        {
            double angle = 0;
            for (int i = 0; i < 60; i++)
            {
                var position = new Vector2(
                    (float)(320 + Math.Cos(angle) * currentScale),
                    (float)(240 + Math.Sin(angle) * currentScale));

                var cadrantElement = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, position);
                cadrantElement.Rotate(6693, angle + Math.PI / 2);
                angle += (Math.PI * 2) / 60;
                cadrant[i] = cadrantElement;
            }
            center = GetLayer("").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(320, 240));
            background = GetLayer("").CreateSprite("sb/core.png", OsbOrigin.Centre, new Vector2(320, 240));
            bigHand = GetLayer("").CreateSprite("sb/ch1.png", OsbOrigin.BottomCentre, new Vector2(320, 240));
            littleHand = GetLayer("").CreateSprite("sb/ch2.png", OsbOrigin.BottomCentre, new Vector2(320, 240));
        }
        private void ShowClock(int startFade, int startTime, int endTime, int endFade, float fade, bool backgroundSprite = true)
        {
            if (backgroundSprite)
            {
                for (int i = 0; i < cadrant.Length; i++)
                {
                    cadrant[i].Fade(startFade, startTime, 0, fade);
                    if (endFade == endTime)
                    {
                        cadrant[i].Fade(endTime, 0);
                    }
                    else
                    {
                        cadrant[i].Fade(endTime, endFade, fade, 0);
                    }
                }
            }
            center.Fade(startFade, startTime, 0, 0.8);
            center.Fade(endTime, endFade, 0.8, 0);

            bigHand.Fade(startFade, startTime, 0, fade);
            bigHand.Fade(endTime, endFade, fade, 0);

            littleHand.Fade(startFade, startTime, 0, fade);
            littleHand.Fade(endTime, endFade, fade, 0);

            if (backgroundSprite)
            {
                background.Fade(startFade, startTime, 0, fade);
                background.Fade(endTime, endFade, fade, 0);
            }
        }
        private void SetClockSpeed(int startTime, int endTime, double speed)
        {
            double currentRotation = bigHand.RotationAt(startTime);
            double littleCurrent = littleHand.RotationAt(startTime);
            var end = endTime - 1;
            for (double i = startTime; i < end; i += speed)
            {
                bigHand.Rotate(OsbEasing.OutElastic, i, i + 100, currentRotation, currentRotation + (Math.PI * 2) / 60);
                currentRotation += (Math.PI * 2) / 60;

                littleHand.Rotate(OsbEasing.OutElastic, i, i + 100, littleCurrent, littleCurrent + (Math.PI * 2) / 3600);
                littleCurrent += (Math.PI * 2) / 3600;
            }
        }
        private void ModifyScale(int startTime, int endTime, int scale, bool ShowCadrant = true, int startScale = 100)
        {
            double angle = 0;
            for (int i = 0; i < 60; i++)
            {
                var newPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * scale),
                    (float)(240 + Math.Sin(angle) * scale));

                if (ShowCadrant)
                {
                    var pos = new Vector2(
                        (float)(320 + Math.Cos(angle) * startScale),
                        (float)(240 + Math.Sin(angle) * startScale));

                    cadrant[i].Move(OsbEasing.OutSine, startTime, endTime, pos, newPosition);
                    cadrant[i].ScaleVec(OsbEasing.OutSine, startTime, endTime, 1, startScale / 8, 1, scale / 8);
                }
                angle += (Math.PI * 2) / 60;
            }
            littleHand.Scale(OsbEasing.OutSine, startTime, endTime, littleHand.ScaleAt(startTime).X, scale * 0.0018);
            bigHand.Scale(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X, scale * 0.0018);
            center.Scale(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X / 10.86, scale * 0.00016);
            background.Scale(OsbEasing.OutSine, startTime, endTime, bigHand.ScaleAt(startTime).X / 2, scale * 0.0009);

            currentScale = scale;
        }
        private void ChangeHour(int startTime, int endTime, double hour, OsbEasing easing)
        {
            double angle = hour * ((Math.PI * 2) / 12);
            double currentRotation = bigHand.RotationAt(startTime);
            double littleCurrent = littleHand.RotationAt(startTime);
            bigHand.Rotate(easing, startTime, endTime, currentRotation, currentRotation + (angle + (Math.PI * 2)) * hour);
            littleHand.Rotate(easing, startTime, endTime, littleCurrent, littleCurrent + angle);
        }
        private void ShowHours(int startTime, int endTime, int radius)
        {
            double angle = 0;
            for (int i = 0; i < 60; i++)
            {
                var position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var cadrantElement = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, position);
                cadrantElement.Scale(startTime, i % 5 == 0 ? 4 : 1);
                cadrantElement.Fade(startTime + (i * 20), startTime + (i * 50) + 1000, 0, 1);
                cadrantElement.Fade(endTime + (i * 20), endTime + (i * 50) + 1000, 1, 0);
                cadrantElement.Rotate(startTime, angle + Math.PI / 4);
                angle += (Math.PI * 2) / 60;
            }
        }
        private void GearParts()
        {
            Scripts gears = new Scripts(this);
            gears.GenerateGears(423210, 444543, 40, "Gear 1");
            gears.GenerateGears(500543, 553877, 40, "Gear 2");

            var gear0 = GetLayer("Gear1").CreateSprite("sb/g/g6.png");
            gear0.Fade(500555, 501889, 0, 0.1);
            gear0.Rotate(500555, 527222, 0, Math.PI);
            gear0.Fade(527221, 0);
            gear0.Scale(500555, 0.7);

            var gear1 = GetLayer("Gear1").CreateSprite("sb/g/g4.png");
            gear1.Fade(500555, 501889, 0, 0.1);
            gear1.Rotate(500555, 527222, 0, -Math.PI);
            gear1.Fade(527221, 0);
            gear1.Scale(500555, 0.1);

            var gear2 = GetLayer("Gear1").CreateSprite("sb/g/g6.png");
            gear2.Fade(500555, 501889, 0, 0.1);
            gear2.Rotate(500555, 527222, 0, Math.PI * 2);
            gear2.Fade(527221, 0);
            gear2.Scale(500555, 0.4);

            Gear(552555, 2, 553555, 0.05);
            Gear(552721, 6, 553555, 0.12);
            Gear(552888, 5, 553555, 0.12);
            Gear(552971, 6, 553555, 0.35);
            Gear(553055, 6, 553555, 0.5);
            Gear(553138, 6, 553555, 0.7);
            Gear(553221, 5, 553555, 0.7);

            for (int i = 0; i < 10; i++)
            {
                var sprite = GetLayer("Foreground Circles").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 240));
                sprite.Fade(501803 + i * 70, 0.45);
                sprite.StartLoopGroup(501803 + i * 70, 4);
                sprite.ScaleVec(OsbEasing.InOutSine, 0, 3330, 0, 0.006, 0.006, 0.006);
                sprite.ScaleVec(OsbEasing.InOutSine, 3330, 6660, 0.006, 0.006, 0, 0.006);
                sprite.MoveX(OsbEasing.InOutQuart, 0, 6660, 755, -115);
                sprite.EndGroup();
            }
        }
        private void Gear(int startTime, int id, int endTime, double scale)
        {
            var sprite = GetLayer("Gear1").CreateSprite($"sb/g/g{id}.png");
            sprite.Fade(startTime, startTime + 100, 0, 1);
            sprite.Fade(OsbEasing.OutExpo, endTime, endTime + 1000, 1, 0.1);
            sprite.Scale(OsbEasing.OutBack, startTime, startTime + 100, scale - 0.1, scale);
            sprite.Rotate(OsbEasing.OutExpo, endTime, endTime + 1000, Random(-Math.PI, Math.PI), 0);
            sprite.Rotate(OsbEasing.InSine, endTime + 1000, 575220, 0, Random(-Math.PI, Math.PI));
        }
    }
}
