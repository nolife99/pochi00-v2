using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    public class ScriptsManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Circles();
            GlitchSection();
            GearParts();

            Scripts particleManager = new Scripts(this);

            particleManager.GenerateFog(28027, 70694, 380, 20, Color4.White, 0.6);
            particleManager.GenerateFog(81360, 92027, 380, 20, Color4.White, 0.6);
            particleManager.GenerateFog(145360, 166027, 380, 20, Color4.White, 0.6);
            particleManager.GenerateFog(168027, 200027, 380, 20, Color4.White, 0.6, "fogKiai");
            particleManager.GenerateFog(332523, 358014, 380, 20, Color4.White, 0.6);
            particleManager.GenerateFog(401889, 422889, 380, 20, Color4.White, 0.6);
            particleManager.GenerateFog(444722, 464555, 380, 20, Color4.Orange, 0.6);
            particleManager.GenerateFog(473889, 496555, 380, 20, Color4.Orange, 0.6);

            particleManager.GenerateDanmaku(102694, 124027, 5000);

            particleManager.SquareTransition(331695, 332522, false, 50, new Color4(10, 10, 10, 1), OsbEasing.InExpo, false, "foreground transition"); 
            particleManager.SquareTransition(355695, 359006, true, 18.2f, new Color4(10, 10, 10, 1), OsbEasing.In);
            particleManager.SquareTransition(378913, 380555, false, 30, new Color4(33, 25, 25, 0), OsbEasing.InExpo, false, "tt");
            particleManager.SquareTransition(400889, 401888, false, 50, new Color4(33, 25, 25, 0), OsbEasing.In, false, "foreground transition");
            particleManager.SquareTransition(574888, 575555, false, 50, new Color4(10, 10, 10, 1), OsbEasing.InSine, true);

            particleManager.TransitionLines(123360, 124027, 124277, "foreground transition");
            particleManager.TransitionLines(144011, 145027, 145345);
            particleManager.TransitionLines(166360, 166694, 167027, "foreground transition");
            particleManager.TransitionLines(465222, 465555, 465889, "transition?");
            particleManager.TransitionLines(628555, 629221, 631221, "transition end", true);

            particleManager.GenerateRain(380555, 433889, 12.5, 2);
            particleManager.GenerateRain(587221, 629471, 10, 3);
            particleManager.GenerateRain(608555, 629471, 17.5);

            particleManager.Highlight(610555, 628555, 0, 420, true);
        }
        public void Circles()
        {
            int StartTime = 124027;
            int EndTime = 144881;
            int Amount = 2;

            var Beat = Beatmap.GetTimingPointAt(StartTime).BeatDuration / 1;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / Amount;

            double rad;
            double angle = 0;
            double radius = 100;
            double Rad;
            double Angle = 0;
            double Radius = 170;
            for (int i = 0; i < Amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = radius * Math.Cos(rad) + Pos.X - 2;
                var y = radius * Math.Sin(rad) + Pos.Y;
                var position = new Vector2((int)x, (int)y);

                var circle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));

                var TravelTime = Beat * 8;
                var duration = EndTime - StartTime;

                circle.Scale(StartTime, 0.005);
                circle.Color(StartTime, Color4.LightBlue);
                for (double t = StartTime; t < EndTime; t += TravelTime)
                {
                    circle.Scale(t + TravelTime / 8 - 50, 0.02);
                    circle.Scale(t + TravelTime / 8 * 7 + 50, 0.005);
                    circle.Color(t + TravelTime / 8 - 50, Color4.GreenYellow);
                    circle.Color(t + TravelTime / 8 * 7 + 50, Color4.LightBlue);
                }
                circle.Fade(StartTime, StartTime + 500, 0, 1);

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.0983;

                    x = radius * Math.Cos(rad) + Pos.X - 2f;
                    y = radius * Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2((int)x, (int)y);

                    circle.Move(time, time + timeStep, position, newPos);
                    position = newPos;
                }
                angle += ConnectionAngle / (Amount / 2);
            }
            for (int l = 0; l < Amount; l++)
            {
                Rad = Angle * Math.PI / ConnectionAngle;
                var X = Radius * Math.Cos(Rad) + Pos.X - 2;
                var Y = Radius * Math.Sin(Rad) + Pos.Y;
                var Position = new Vector2((int)X, (int)Y);

                var outCircle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));

                var travelTime = Beat * 8;
                var Duration = EndTime - StartTime;

                outCircle.Scale(StartTime, 0.02);
                outCircle.Color(StartTime, Color4.GreenYellow);
                for (double t = StartTime; t < EndTime; t += travelTime)
                {
                    outCircle.Scale(t + travelTime / 8 - 50, 0.005);
                    outCircle.Scale(t + travelTime / 8 * 7 + 50, 0.02);
                    outCircle.Color(t + travelTime / 8 - 50, Color4.LightBlue);
                    outCircle.Color(t + travelTime / 8 * 7 + 50, Color4.GreenYellow);
                }
                outCircle.Fade(StartTime, StartTime + 500, 0, 1);

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double t = StartTime; t < EndTime; t += timeStep)
                {
                    Rad += 0.0493;
                    X = Radius * Math.Cos(Rad) + Pos.X - 2;
                    Y = Radius * Math.Sin(Rad) + Pos.Y;

                    var NewPos = new Vector2((int)X, (int)Y);

                    outCircle.Move(t, t + timeStep, Position, NewPos);
                    Position = NewPos;
                }
                Angle += ConnectionAngle / (Amount / 2);
            }
        }
        public void RotatingLines()
        {
            int startTime = 289489;
            int endTime = 302730;
            var blankBitmap = GetMapsetBitmap("sb/p.png");
            int amount = 22;
            double angle = 90;
            double radius = 150;
            for (var i = 0; i < amount; i++)
            {
                double ConnectionAngle = Math.PI / amount * 1.5;

                var position = new Vector2(
                    (float)(318 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var lines = layer.CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(0, 0));

                lines.ScaleVec(startTime, 1, 20);
                lines.Fade(startTime, startTime + 1000, 0, 0.15);
                lines.Fade(startTime + 1000, endTime, 0.15, 0.8);
                lines.Fade(endTime, endTime + 2000, 0.8, 0);

                var timeStep = Beatmap.GetTimingPointAt((int)startTime).BeatDuration / 4;
                for (double time = startTime; time < endTime; time += timeStep)
                {
                    angle += -0.085;
                    radius += 0.022;

                    var nPosition = new Vector2(
                        (float)(318 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    lines.Move(time, time + timeStep, position, nPosition);
                    position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);

                var pos = new Vector2(
                    (float)(318 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var Rotation = Math.Atan2(position.Y - pos.Y, position.X - pos.X) - Math.PI / 1.2;

                lines.Rotate(startTime, endTime, Rotation, Rotation - Math.PI * 2.15);
            }
        }
        public void GlitchSection()
        {
            int startTime = 276247;
            int endTime = 289488;
            var ani = GetLayer("Glitch").CreateAnimation("sb/b/a/l.png", 5, 5.2, OsbLoopType.LoopForever);

            ani.Fade(startTime, 0.12);
            ani.Scale(startTime, 1.2);
            ani.Move(startTime, endTime, -404, 37, 1044, 37);

            Spectrum(startTime, endTime, 4, Color4.SkyBlue);
            Spectrum(startTime, endTime, 0, Color4.LightSteelBlue);
            Squares();
            Squares2();
            SquaresGlitch();
            Squares3();
            RotatingLines();
        }
        public void Squares()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 120;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (int)(320 + Math.Cos(angle) * radius),
                    (int)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("Glitch");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(100, 150);
                Vector2 skewedScale = new Vector2(30, -150);
                float standardRotation = MathHelper.DegreesToRadians(0);
                float skewedRotation = MathHelper.DegreesToRadians(60);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);
                sprite.Fade(276247, 0.07);

                var timeStep = Beatmap.GetTimingPointAt(276247).BeatDuration / 4;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.0623;

                    Vector2 nPosition = new Vector2(
                        (int)(320 + Math.Cos(angle) * radius),
                        (int)(240 + Math.Sin(angle) * radius));

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
            var Amount = 150;
            var StartTime = 228247;
            var EndTime = 230316;
            for (var i = 0; i < Amount; i++)
            {
                float Width = 854;
                float Height = (float)Random(-190, 190);
                float nHeight = (float)Random(50, 430);
                float nHeight2 = (float)Random(51, 429);

                var pos = new Vector2(320 - Width / 2, 240);
                var spriteSpace = Width / Amount;
                var position = new Vector2(
                    pos.X + i * spriteSpace + Random(-5, 5),
                    pos.Y + Height);

                var sprite = GetLayer("one part").CreateSprite("sb/s.png", OsbOrigin.Centre);

                var Delay = 2;
                var Fade = Random(0.1, 0.5);
                var FadeNew = Random(0.5, 1);
                var Scale = Random(0.03, 0.06);
                var Rotation = MathHelper.DegreesToRadians(100);

                sprite.Additive(StartTime, EndTime);
                sprite.Fade(StartTime + Delay * i, StartTime + 200 + Delay * i, 0, Fade);
                sprite.Fade(229075, 229126, Fade, FadeNew);
                sprite.Fade(229282, 229333, FadeNew, Fade);
                sprite.Fade(229488, 229489, Fade, FadeNew);
                sprite.Fade(229902 + Delay * i, EndTime + Delay * i, FadeNew, 0);

                sprite.Scale(StartTime, Scale);
                sprite.Scale(OsbEasing.Out, 229075, 229282, Scale + 0.08, Scale);
                sprite.Scale(229902, 230523, Scale, 0);
                sprite.Rotate(OsbEasing.OutBack, StartTime, 229075, 0, Random(-Rotation, Rotation));
                sprite.Rotate(OsbEasing.OutBack, 229075, 229282, Random(-Rotation, Rotation), 0);
                sprite.Move(OsbEasing.OutBack, StartTime + (Delay + 3) * i, 229075, new Vector2(800, position.Y), position);
                sprite.Move(229075, new Vector2(position.X, nHeight));
                sprite.Move(229282, new Vector2(position.X, nHeight2));
                sprite.Move(OsbEasing.OutBack, 229488 + Delay * i, 229902 + Delay * i, new Vector2(position.X, nHeight2), new Vector2(position.X, Random(230, 250)));
                sprite.Move(OsbEasing.Out, 229902 + Delay * i, EndTime + Delay * i, new Vector2(position.X, Random(230, 250)), new Vector2(position.X, 240));
            }
        }
        public void Squares2()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 33;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("Glitch");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(5, 100);
                Vector2 skewedScale = new Vector2(50, -100);
                float standardRotation = MathHelper.DegreesToRadians(60);
                float skewedRotation = MathHelper.DegreesToRadians(120);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);

                sprite.Fade(276247, 0.6);
                sprite.Color(276247, Color4.White);


                sprite.Fade(277902, 1);
                sprite.Color(277902, Color4.IndianRed);
                sprite.Fade(278316, 0.6);
                sprite.Color(278316, Color4.White);

                sprite.Fade(278730, 1);
                sprite.Color(278730, Color4.IndianRed);
                sprite.Fade(278833, 0.6);
                sprite.Color(278833, Color4.White);

                sprite.Fade(278937, 1);
                sprite.Color(278937, Color4.IndianRed);
                sprite.Fade(279040, 0.6);
                sprite.Color(279040, Color4.White);

                sprite.Fade(280385, 1);
                sprite.Color(280385, Color4.IndianRed);
                sprite.Fade(280799, 0.6);
                sprite.Color(280799, Color4.White);

                sprite.Fade(282868, 1);
                sprite.Color(282868, Color4.IndianRed);
                sprite.Fade(283282, 0.6);
                sprite.Color(283695, Color4.White);

                sprite.Fade(284523, 1);
                sprite.Color(284523, Color4.IndianRed);
                sprite.Fade(284575, 0.6);
                sprite.Color(284575, Color4.White);

                sprite.Fade(284626, 1);
                sprite.Color(284626, Color4.IndianRed);
                sprite.Fade(284678, 0);
                sprite.Color(284678, Color4.White);

                sprite.Fade(284730, 1);
                sprite.Color(284730, Color4.IndianRed);
                sprite.Fade(284782, 0.6);
                sprite.Color(284782, Color4.White);

                sprite.Fade(284833, 1);
                sprite.Color(284833, Color4.IndianRed);
                sprite.Fade(284885, 0.6);
                sprite.Color(284885, Color4.White);

                sprite.Fade(284937, 1);
                sprite.Color(284937, Color4.IndianRed);
                sprite.Fade(284988, 0.6);
                sprite.Color(284988, Color4.White);

                sprite.Fade(285040, 1);
                sprite.Color(285040, Color4.IndianRed);
                sprite.Fade(285092, 0.6);
                sprite.Color(285092, Color4.White);

                sprite.Fade(285144, 1);
                sprite.Color(285144, Color4.IndianRed);
                sprite.Fade(285195, 0.6);
                sprite.Color(285195, Color4.White);

                sprite.Fade(285247, 1);
                sprite.Color(285247, Color4.IndianRed);
                sprite.Fade(285299, 0.6);
                sprite.Color(285299, Color4.White);

                sprite.Fade(286178, 1);
                sprite.Color(286178, Color4.IndianRed);
                sprite.Fade(286488, 0.6);
                sprite.Color(286488, Color4.White);

                sprite.Fade(286592, 1);
                sprite.Color(286592, Color4.IndianRed);
                sprite.Fade(287006, 0.6);
                sprite.Color(287006, Color4.White);

                sprite.Fade(287833, 1);
                sprite.Color(287833, Color4.IndianRed);
                sprite.Fade(288144, 0.6);
                sprite.Color(288144, Color4.White);

                sprite.Fade(288247, 1);
                sprite.Color(288247, Color4.IndianRed);
                sprite.Fade(288661, 0.6);
                sprite.Color(288661, Color4.White);

                sprite.Additive(276247, 289488);

                var timeStep = Beatmap.GetTimingPointAt(276247).BeatDuration / 4;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.0625;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }
        public void SquaresGlitch()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 43;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("");
                var sprite = layer.CreateSprite("sb/p.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(5, 100);
                Vector2 skewedScale = new Vector2(50, -100);
                float standardRotation = MathHelper.DegreesToRadians(60);
                float skewedRotation = MathHelper.DegreesToRadians(120);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 287833, 289488, standardScale, standardScale.X, 0);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);

                sprite.Fade(276247, 0.6);
                sprite.Additive(276247, 289488);

                var timeStep = Beatmap.GetTimingPointAt(277902).BeatDuration / 4;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.0623;

                    Vector2 nPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }
        public void Squares3()
        {
            int sqAmount = 2;

            double angle = 0;
            double radius = 200;
            for (var i = 0; i < sqAmount; i++)
            {
                var Position = new Vector2(320, 240);
                var ConnectionAngle = Math.PI / sqAmount;

                Vector2 position = new Vector2(
                    (int)(320 + Math.Cos(angle) * radius),
                    (int)(240 + Math.Sin(angle) * radius));

                var layer = GetLayer("Glitch");
                var sprite = layer.CreateSprite("sb/c.png", OsbOrigin.Centre);

                Vector2 standardScale = new Vector2(0.4f, 0.4f);
                Vector2 skewedScale = new Vector2(0.01f, -0.4f);
                float standardRotation = MathHelper.DegreesToRadians(0);
                float skewedRotation = MathHelper.DegreesToRadians(60);

                sprite.ScaleVec(OsbEasing.InOutSine, 276247, 277902, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 277902, 283178, skewedScale, standardScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 283178, 286075, standardScale, skewedScale);
                sprite.ScaleVec(OsbEasing.InOutSine, 286075, 287833, skewedScale, standardScale);
                sprite.Rotate(OsbEasing.InOutSine, 276247, 277902, standardRotation, skewedRotation);
                sprite.Rotate(OsbEasing.InOutSine, 277902, 283178, skewedRotation, skewedRotation * 3);
                sprite.Rotate(OsbEasing.InOutSine, 283178, 289488, skewedRotation * 3, standardRotation);
                sprite.Fade(276247, 0.03);

                var timeStep = Beatmap.GetTimingPointAt(276247).BeatDuration / 5;
                for (double time = 276247; time < 289488; time += timeStep)
                {
                    angle += 0.0497;

                    Vector2 nPosition = new Vector2(
                        (int)(320 + Math.Cos(angle) * radius),
                        (int)(240 + Math.Sin(angle) * radius));

                    sprite.Move(time, time + timeStep, Position, nPosition);

                    Position = nPosition;
                }
                angle += ConnectionAngle / (sqAmount / 2);
            }
        }
        private void Spectrum(int startTime, int endTime, float offset, Color4 Color)
        {
            var MinimalHeight = 0.5f;
            Vector2 Scale = new Vector2(1, 70);
            float LogScale = 270;
            int Width = 287;
            Vector2 Position = new Vector2(337, 377);

            int BarCount = 10;
            var bitmap = GetMapsetBitmap("sb/p.png");

            var heightKeyframes = new KeyframedValue<float>[BarCount];
            for (var i = 0; i < BarCount; i++)
                heightKeyframes[i] = new KeyframedValue<float>(null);

            double fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / 12;
            double fftOffset = fftTimeStep * 0.2;
            for (var time = (double)startTime; time <= endTime + 1; time += fftTimeStep)
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
                keyframes.Simplify1dKeyframes(1, h => h);

                var bar = GetLayer("Glitch").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(positionX - offset, Position.Y - offset));

                bar.Color(startTime, Color);
                bar.Fade(endTime, endTime, 0.4, 0);

                var scaleX = Scale.X * barWidth / 1.75f / bitmap.Width;
                scaleX = (float)Math.Floor(scaleX * 10) / 10;

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
                    s => (float)Math.Round(s, 0)
                );
                if (!hasScale) bar.ScaleVec(startTime, scaleX, MinimalHeight);
            }
        }
        public void GearParts()
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
                sprite.Fade(527221, 527555, 0.45, 0);
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
