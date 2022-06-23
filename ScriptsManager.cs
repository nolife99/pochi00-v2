using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    public class ScriptsManager : StoryboardObjectGenerator
    {
        OsbSpritePools pool;
        public override void Generate()
        {
            Fog(28027, 70694, 380, 20, Color4.White, 0.6);
            Fog(81360, 92027, 380, 20, Color4.White, 0.6);
            Fog(145360, 166027, 380, 20, Color4.White, 0.6);
            Fog(168027, 200027, 380, 20, Color4.White, 0.6);
            Fog(332523, 358014, 380, 20, Color4.White, 0.6);
            Fog(401889, 422889, 380, 20, Color4.White, 0.6);
            Fog(444722, 464555, 380, 20, Color4.Orange, 0.6); 
            Fog(473889, 496555, 380, 20, Color4.Orange, 0.6);

            Danmaku(102694, 124027, 5000);

            SquareTransition(331695, 332522, 50, new Color4(10, 10, 10, 1), OsbEasing.InQuad, false, "foreground transition"); 
            SquareTransition(355695, 359006, 18.2f, new Color4(10, 10, 10, 1), OsbEasing.InQuad);
            SquareTransition(378913, 380555, 30, new Color4(33, 25, 25, 0), OsbEasing.InQuad, false);
            SquareTransition(401222, 401888, 50, new Color4(33, 25, 25, 0), OsbEasing.InSine, false, "foreground transition");
            SquareTransition(574888, 575555, 50, new Color4(10, 10, 10, 1), OsbEasing.InSine, true);

            TransitionLines(123360, 124027, 124277, "foreground transition");
            TransitionLines(144011, 145027, 145345);
            TransitionLines(166360, 166694, 167027, "foreground transition");
            TransitionLines(465222, 465555, 465889, "transition?");
            TransitionLines(628555, 629221, 631221, "transition end", true);

            GenerateGears(423210, 444543, 40, "Gear 1");
            GenerateGears(500543, 553877, 40, "Gear 2");

            using (pool = new OsbSpritePools(GetLayer("rain")))
            {
                pool.MaxPoolDuration = (int)AudioDuration;
                Rain(380555, 433889, 12, 2);
                Rain(587221, 629471, 10, 3);
                Rain(608555, 629471, 12.5, 1);
            }

            Highlight(611221, 628555);

            DiamondCross(27, 1276, 0, 100, false);
            DiamondCross(2526, 5776, 500, 0, true, "cross", OsbEasing.InQuint);
            DiamondCross(5276, 5776, 0, 300, false);
            DiamondCross(5526, 5776, 0, 200, false); 
            DiamondCross(28027, 29360, 0, 300, false);
            DiamondCross(80027, 81360, 0, 300, false);
            DiamondCross(80277, 81360, 0, 290, false);
            DiamondCross(80527, 81360, 0, 280, false);
            DiamondCross(80694, 81360, 0, 270, false);
            DiamondCross(81360, 82694, 0, 400, false);
            DiamondCross(166694, 168027, 0, 100, false, "foreground");
            DiamondCross(168027, 169360, 0, 450, false);
            DiamondCross(194027, 194694, 400, 0, true, "cross", OsbEasing.InQuint);
            DiamondCross(195027, 196360, 0, 400, false);
            DiamondCross(379461, 380556, 450, 0, true, "cross", OsbEasing.InQuint);
            DiamondCross(401555, 402222, 0, 350, false, "foreground");
            DiamondCross(421889, 423222, 450, 0, true, "cross", OsbEasing.InQuint);
            DiamondCross(423222, 424555, 0, 350, false, "foreground");
        }
        private void Fog(int startTime, int endTime, int posY, int quantity, Color4 color, double fade, string layer = "Fog", int stroke = 60)
        {
            for (int i = 0; i < quantity; i++)
            {
                int firstTimeDuration = Random(1000, 20000);
                if (firstTimeDuration >= 20000) firstTimeDuration = firstTimeDuration * i / 10;
    
                int posX = Random(-157, 647);
                int endX = Random(877, 907);
                int elementStartTime = startTime;
                int particleStartTime = startTime;
    
                for (int p = 0; p < 2; p++)
                {
                    var particle = GetLayer(layer).CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(0, Random(posY - stroke, posY + stroke)));
                    particle.MoveX(startTime, startTime + firstTimeDuration, Random(posX - 25, posX + 25), endX);
                    particle.Fade(startTime, startTime + 1000, 0, 1);
                    particle.Scale(startTime, Math.Round(Random(0.01, 0.02), 3));
                    if (color != Color4.White) particle.Color(startTime, color);
                    particle.Additive(startTime);
    
                    particleStartTime += firstTimeDuration;
                    while (particleStartTime + 4000 < endTime)
                    {
                        int NewDuration = Random(7500, 30000);
                        if (startTime + firstTimeDuration + 5000 > endTime) NewDuration -= 1000;
    
                        int particleEndTime = particleStartTime + NewDuration;
                        particle.MoveX(particleStartTime, particleEndTime, Random(-127, -107), endX);
                        particleStartTime += NewDuration;
                    }
                    var posStart = particle.PositionAt(particleStartTime);
                    if (posStart.X >= 757) particle.Scale(particleStartTime, 0);
                    var scaleEnd = particle.ScaleAt(endTime);
                    if (scaleEnd.X != 0) particle.Fade(endTime, endTime + 1000, 1, 0);
                }
                var sprite = GetLayer(layer).CreateSprite($"sb/s/s{Random(0, 9)}.png", OsbOrigin.Centre, new Vector2(0, Random(posY - stroke, posY + stroke)));
                sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
                sprite.Fade(startTime, startTime + 1000, 0, fade);
                if (color != Color4.White) sprite.Color(startTime, color);
                sprite.Scale(startTime, Math.Round(Random(0.4, 1), 2));
    
                elementStartTime += firstTimeDuration;
                while (elementStartTime + 4000 < endTime)
                {
                    int newDuration = Random(7500, 30000);
                    if (startTime + firstTimeDuration + 5000 > endTime) newDuration -= 1000;
    
                    int elementEndTime = elementStartTime + newDuration;
                    sprite.MoveX(elementStartTime, elementEndTime, Random(-227, -220), endX);
                    elementStartTime += newDuration;
                }
                var startPos = sprite.PositionAt(elementStartTime);
                if (startPos.X >= 848) sprite.Scale(elementStartTime, 0);
                var endScale = sprite.ScaleAt(endTime);
                if (endScale.X != 0) sprite.Fade(endTime, endTime + 1000, fade, 0);
            }
        }
        private void Danmaku(int startTime, int endTime, int speed)
        {
            var basePosition = new Vector2(320, 240);
            for (int i = 0; i < 4; i++)
            {
                double angle = (Math.PI / 2) * i;
                for (int l = 0; l < 50; l++)
                {
                    var endPosition = new Vector2(
                        (float)(320 + Math.Cos(angle) * 450),
                        (float)(240 + Math.Sin(angle) * 450));
    
                    var sprite = GetLayer("PARTICLES").CreateSprite("sb/p.png");
                    sprite.StartLoopGroup(startTime + l * 100, (endTime - startTime - l * 35) / speed);
                    sprite.Move(OsbEasing.OutSine, 0, speed, basePosition, endPosition);
                    sprite.ScaleVec(OsbEasing.In, speed / 6, speed, 10, 1, 10, 0);
                    sprite.Rotate(OsbEasing.InSine, 0, speed, angle, angle - 1.5);
                    sprite.EndGroup();
    
                    for (double t = startTime + l * 100; t < endTime - 2500; t += speed)
                    {
                        sprite.Fade(t + speed / 6, t + speed / 2, 0, 1);
                        sprite.Fade(t, 0);
                    }
                    var scaleAtEnd = sprite.ScaleAt(endTime);
                    var posAtEnd = sprite.PositionAt(endTime);
                    if (scaleAtEnd.Y == 0 | posAtEnd.X <= -115 | posAtEnd.X >= 755 | posAtEnd.Y <= 30 | posAtEnd.Y >= 450) { }
                    else sprite.Fade(endTime, endTime + 200, 1, 0);
    
                    angle += Math.PI / 50;
                }
            }
        }
        private void Highlight(int startTime, int endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer("Highlight"), "sb/hl.png", OsbOrigin.Centre, true))
            {
                for (int i = startTime; i < endTime; i += 530)
                {
                    var fade = Math.Round(Random(0.02, 0.03), 2);
                    var fadeTime = Random(1000, 2500);
                    var sprite = pool.Get(i, i + fadeTime * 2);
                    var pos = new Vector2(Random(0, 727), Random(10, 380));
                    var newPos = new Vector2(Random(-107, 854), Random(-17, 480));
    
                    sprite.Move(OsbEasing.OutSine, i, i + fadeTime * 2, pos, newPos);
                    sprite.Fade(i, i + 250, 0, fade);
                    if (fade > 0.01)
                    {
                        sprite.Fade(OsbEasing.InSine, i + fadeTime, i + fadeTime * 2, fade, 0);
                    }
                    sprite.Scale(OsbEasing.InOutSine, i, i + fadeTime * 2, Math.Round(Random(0.85, 2), 2), 0);
                }
            }
        }
        private void Rain(int startTime, int endTime, double intensity, int type)
        {
            for (int i = 0; i < intensity * 10; i++)
            {
                int particleSpeed = Random(300, 400);
                int posX = Random(-106, 747);
                int endX = Random(posX - 15, posX + 15);
                double angle = Math.Atan2(680, endX - posX);
                int delay = 20;
                int duration = endTime - startTime - i * delay;
     
                var sprite = pool.Get(startTime + i * delay, endTime + i * delay, "sb/pl.png", OsbOrigin.Centre, true);
                if (type == 2)
                {
                    delay += 80;
                    duration = endTime - startTime - i * (int)(delay / 1.6);
                }
                if (type == 3)
                {
                    delay += 130;
                    duration = endTime - startTime - i * delay;
                }
     
                sprite.StartLoopGroup(startTime + i * delay, duration / particleSpeed);
                sprite.MoveY(0, particleSpeed, 20, 460);
                sprite.MoveX(0, particleSpeed, posX, endX);
                sprite.Rotate(0, particleSpeed, Math.PI / 2, angle);
                sprite.EndGroup();
                sprite.Fade(startTime + i * delay, Math.Round(Random(0.15, 0.5), 2));
                sprite.Scale(startTime + i * delay, Math.Round(Random(0.3, 0.5), 2));
     
                var splash = pool.Get(startTime + i * delay, endTime + i * delay, "sb/d.png", OsbOrigin.Centre, false);
                splash.StartLoopGroup(startTime + i * delay + particleSpeed, duration / particleSpeed);
                splash.MoveX(OsbEasing.OutExpo, 0, particleSpeed, endX, endX + Random(-5, 5));
                splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 450, Random(390, 430));
                splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
                splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, Math.Round(Random(0.03, 0.04), 3), 0);
                splash.EndGroup();
            }
        }
        private void SquareTransition(int startTime, int endTime, float squareScale, Color4 color, OsbEasing easing, bool Full = false, string layer = "transition")
        {
            float posX = -100;
            float posY = 40;
            int duration = endTime - startTime;
    
            while (posX < 737 + squareScale)
            {
                while (posY < 450)
                {
                    var sprite = GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, posY));
    
                    if (!Full)
                    {
                        sprite.Scale(easing, startTime, endTime, 0, squareScale);
                        sprite.Rotate(easing, startTime, endTime, Math.PI, 0);
                    }
                    else
                    {
                        sprite.Scale(OsbEasing.InSine, startTime, endTime - duration / 2, 0, squareScale);
                        sprite.Scale(OsbEasing.OutSine, endTime - duration / 2, endTime + 1000, squareScale, 0);
                        sprite.Rotate(OsbEasing.InSine, startTime, endTime - duration / 2, 0, -Math.PI);
                        sprite.Rotate(OsbEasing.OutSine, endTime - duration / 2, endTime + 1000, Math.PI, 0);
                    }
                    sprite.Color(startTime, color);
                    posY += squareScale;
                }
                posY = 40;
                posX += squareScale;
            }
        }
        private void TransitionLines(int startTransition, int endTransition, int endTime, string layer = "transition", bool fullscreen = false)
        {
            var transitionColor = new Color4(0.05f, 0.05f, 0.05f, 1);
            int transitionDuration = endTransition - startTransition;
            int delay = 0;
            int posX = -120;
            int scaleY = fullscreen ? 484 : 406;
    
            for (int i = 0; i < 60; i++)
            {
                var sprite = GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, 240));
                sprite.ScaleVec(startTransition + delay, startTransition + delay + 300, 0, scaleY, 14.93, scaleY);
                sprite.Fade(endTime, endTime + 1000, 1, 0);
                sprite.Rotate(startTransition + delay, 0.1);
                sprite.Color(startTransition + delay, transitionColor);
    
                delay += transitionDuration / 60;
                posX += 15;
            }
        }
        public void GenerateGears(int startTime, int endTime, int gearNumber, string layer)
        {
            float colorDark = 0.15f;
            double maxScale = 0.35;
            for (int i = 0; i < gearNumber; i++)
            {
                int baseYPos = Random(20, 460);
                int duration = Random(15000, 30000);
    
                var sprite = GetLayer(layer).CreateSprite($"sb/g/g{Random(1, 7)}.png", OsbOrigin.Centre, new Vector2(i % 2 == 0 ? -107 : 747, baseYPos));
                sprite.Fade(startTime + (i * 50), startTime + (i * 50) + 300, 0, 1);
                sprite.Fade(endTime - 1000, endTime, 1, 0);
                sprite.Scale(startTime, Random(0.1, maxScale));
                sprite.Color(startTime, colorDark, colorDark, colorDark);
                sprite.Rotate(startTime, endTime, 0, Random(0, 2) == 0 ? Random(-5, -1) : Random(1, 5));
                sprite.MoveY(startTime, endTime, baseYPos, baseYPos + Random(-100, 100));
    
                colorDark += 0.5f / gearNumber;
                maxScale -= 0.3 / gearNumber;
            }
        }
        private void DiamondCross(int startTime, int endTime, double startScale, double endScale, bool noFade, string layer = "cross", OsbEasing easing = OsbEasing.OutQuint)
        {
            double angle = 0;
            int duration = endTime - startTime;
            var position = new Vector2(320, 240);
            for (int i = 0; i < 4; i++)
            {
                var startPosition = new Vector2(
                    (float)(position.X + Math.Cos(angle) * startScale),
                    (float)(position.Y + Math.Sin(angle) * startScale));
    
                var endPosition = new Vector2(
                    (float)(position.X + Math.Cos(angle) * endScale),
                    (float)(position.Y + Math.Sin(angle) * endScale));
    
                double scaleStart = Math.Sqrt(2 * Math.Pow(startScale, 2));;
                double scaleEnd = Math.Sqrt(2 * Math.Pow(endScale, 2));
    
                var sprite = GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.BottomCentre);
                sprite.ScaleVec(easing, startTime, endTime, noFade ? 0 : 50, scaleStart + (noFade ? 0 : 25), noFade ? 50 : 0, scaleEnd + (noFade ? 25 : 0));
                sprite.Rotate(startTime, angle - Math.PI / 4);
                sprite.Move(easing, startTime, endTime, startPosition, endPosition);
    
                if (!noFade) sprite.Fade(endTime - duration / 2, endTime - duration / 5, 1, 0);
    
                angle += Math.PI / 2;
            }
        }
    }
}
