using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    class GlitchKiai : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Circles();
		    int startTime = 276247;
            int endTime = 289488;
            var ani = GetLayer("").CreateAnimation("sb/b/a/l.png", 5, 10, OsbLoopType.LoopForever);

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
        private void Circles()
        {
            int StartTime = 124027;
            int EndTime = 144881;
            int amount = 2;

            var travelTime = Beatmap.GetTimingPointAt(StartTime).BeatDuration * 8;
            var Pos = new Vector2(320, 240);
            var ConnectionAngle = Math.PI / amount;

            double rad;
            double angle = 0;
            double radius = 100;
            double Radius = 170;
            for (int i = 0; i < amount; i++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = radius * Math.Cos(rad) + Pos.X - 2;
                var y = radius * Math.Sin(rad) + Pos.Y;
                var position = new Vector2((int)x, (int)y);
                var duration = EndTime - StartTime;

                var circles = GetLayer("circle").CreateSprite("sb/c2.png", OsbOrigin.Centre, position);
                circles.Scale(124027, 144944, 0.2, 0.2);

                var circle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));
                circle.Fade(StartTime, StartTime + 500, 0, 1);
                circle.StartLoopGroup(StartTime, duration / ((int)travelTime) + 1);
                circle.Color(0, Color4.LightBlue);
                circle.Scale(travelTime / 8 - 50, 0.02);
                circle.Scale((travelTime / 8) * 7 + 50, 0.005);
                circle.Color(travelTime / 8 - 50, Color4.GreenYellow);
                circle.Color((travelTime / 8) * 7 + 50, travelTime, Color4.LightBlue, Color4.LightBlue);
                circle.EndGroup();

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double time = StartTime; time < EndTime; time += timeStep)
                {
                    rad += 0.0982;

                    x = radius * Math.Cos(rad) + Pos.X - 2;
                    y = radius * Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2((int)x, (int)y);

                    circle.Move(time, time + timeStep, position, newPos);
                    position = newPos;
                }
                angle += ConnectionAngle / (amount / 2);
            }
            for (int l = 0; l < amount; l++)
            {
                rad = angle * Math.PI / ConnectionAngle;
                var x = Radius * Math.Cos(rad) + Pos.X - 2;
                var y = Radius * Math.Sin(rad) + Pos.Y;
                var position = new Vector2((int)x, (int)y);
                var duration = EndTime - StartTime;

                var outCircle = GetLayer("circle").CreateSprite("sb/c.png", OsbOrigin.Centre, new Vector2(0, 0));
                outCircle.Fade(StartTime, StartTime + 500, 0, 1);
                outCircle.StartLoopGroup(StartTime, duration / ((int)travelTime) + 1);
                outCircle.Color(0, Color4.GreenYellow);
                outCircle.Scale(travelTime / 8 - 50, 0.005);
                outCircle.Scale((travelTime / 8) * 7 + 50, 0.02);
                outCircle.Color(travelTime / 8 - 50, Color4.LightBlue);
                outCircle.Color((travelTime / 8) * 7 + 50, travelTime, Color4.GreenYellow, Color4.GreenYellow);
                outCircle.EndGroup();

                var timeStep = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration / 4;
                for (double t = StartTime; t < EndTime; t += timeStep)
                {
                    rad += 0.0493;
                    x = Radius * Math.Cos(rad) + Pos.X - 2;
                    y = Radius * Math.Sin(rad) + Pos.Y;

                    var newPos = new Vector2((int)x, (int)y);

                    outCircle.Move(t, t + timeStep, position, newPos);
                    position = newPos;
                }
                angle += ConnectionAngle / (amount / 2);
            }
        }
        private void RotatingLines()
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
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var lines = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(0, 0));

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
                        (float)(320 + Math.Cos(angle) * radius),
                        (float)(240 + Math.Sin(angle) * radius));

                    lines.Move(time, time + timeStep, new Vector2((int)position.X, (int)position.Y), new Vector2((int)nPosition.X, (int)nPosition.Y));
                    position = nPosition;
                }
                angle += ConnectionAngle / (amount / 2);

                var pos = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius));

                var Rotation = Math.Atan2(position.Y - pos.Y, position.X - pos.X) - Math.PI / 1.2;

                lines.Rotate(startTime, endTime, Rotation, Rotation - Math.PI * 2.15);
            }
        }
        private void Squares()
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

                var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);

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
            var Amount = 130;
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

                var sprite = GetLayer("").CreateSprite("sb/s.png", OsbOrigin.Centre);

                var Delay = 2;
                var Fade = Random(0.1, 0.5);
                var FadeNew = Random(0.5, 1);
                var Scale = Random(0.03, 0.06);
                var Rotation = MathHelper.DegreesToRadians(100);

                sprite.Additive(StartTime);
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
        private void Squares2()
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

                var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);

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

                sprite.Additive(276247);

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
        private void SquaresGlitch()
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

                var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre);

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
                sprite.Additive(276247);

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
        private void Squares3()
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

                var sprite = GetLayer("").CreateSprite("sb/c.png", OsbOrigin.Centre);

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

            double fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / 8;
            double fftOffset = fftTimeStep * 0.1;
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

                var bar = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(positionX - offset, Position.Y - offset));

                bar.Color(startTime, Color);
                bar.Fade(endTime, endTime, 0.4, 0);

                var scaleX = Scale.X * barWidth / bitmap.Width / 1.6f;
                scaleX = (int)Math.Floor(scaleX * 10) / 10;

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
                    s => (int)s
                );
                if (!hasScale) bar.ScaleVec(startTime, scaleX, MinimalHeight);
            }
        }
    }
}
