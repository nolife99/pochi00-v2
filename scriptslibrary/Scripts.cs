using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

public class Scripts
{
    private StoryboardObjectGenerator generator;
    public Scripts(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void Fog(int startTime, int endTime, int posY, int quantity, Color4 color, double fade, string layer = "Fog", int stroke = 60)
    {
        for (int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(1000, 20000);
            if (firstTimeDuration >= 20000)
                firstTimeDuration = firstTimeDuration * i / 10;
                        
            int posX = generator.Random(-157, 647);
            int endX = generator.Random(877, 907);
            int elementStartTime = startTime;
            int particleStartTime = startTime;

            for (int p = 0; p < 2; p++)
            {
                var particle = generator.GetLayer(layer).CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(0, generator.Random(posY - stroke, posY + stroke)));
                particle.MoveX(startTime, startTime + firstTimeDuration, generator.Random(posX - 25, posX + 25), endX);
                particle.Fade(startTime, startTime + 1000, 0, 1);
                particle.Fade(endTime, endTime + 1000, 1, 0);
                particle.Scale(startTime, Math.Round(generator.Random(0.01, 0.02), 3));
                if (color != Color4.White)
                    particle.Color(startTime, color);
                    
                particle.Additive(startTime);

                particleStartTime += firstTimeDuration;
                while (particleStartTime + 4000 < endTime)
                {
                    int NewDuration = generator.Random(7500, 30000);
                    if (startTime + firstTimeDuration + 5000 > endTime)
                        NewDuration -= 1000;

                    int particleEndTime = particleStartTime + NewDuration;
                    particle.MoveX(particleStartTime, particleEndTime, generator.Random(-127, -107), endX);
                    particleStartTime += NewDuration;
                }
            }
            var sprite = generator.GetLayer(layer).CreateSprite($"sb/s/s{generator.Random(0, 9)}.png", OsbOrigin.Centre, new Vector2(0, generator.Random(posY - stroke, posY + stroke)));
            sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
            sprite.Fade(startTime, startTime + 1000, 0, fade);
            sprite.Fade(endTime, endTime + 1000, fade, 0);
            if (color != Color4.White)
                sprite.Color(startTime, color);

            sprite.Scale(startTime, Math.Round(generator.Random(0.5, 1), 2));

            elementStartTime += firstTimeDuration;
            while (elementStartTime + 4000 < endTime)
            {
                int newDuration = generator.Random(7500, 30000);
                if (startTime + firstTimeDuration + 5000 > endTime)
                    newDuration -= 1000;
                
                int elementEndTime = elementStartTime + newDuration;
                sprite.MoveX(elementStartTime, elementEndTime, generator.Random(-227, -220), endX);
                elementStartTime += newDuration;

                if (elementEndTime + 5000 > endTime)
                    sprite.Scale(elementEndTime, 0);
            }
        }
    }
    public void Danmaku(int startTime, int endTime, int speed)
    {
        Vector2 basePosition = new Vector2(320, 240);
        for (int i = 0; i < 4; i++)
        {
            double angle = (Math.PI / 2) * i;
            for (int l = 0; l < 50; l++)
            {
                var endPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * 450),
                    (float)(240 + Math.Sin(angle) * 450));

                var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/p.png");
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
                if (scaleAtEnd.Y == 0 | posAtEnd.X <= -115 | posAtEnd.X >= 755 | posAtEnd.Y <= 30 | posAtEnd.Y >= 450){}
                else sprite.Fade(endTime, endTime + 200, 1, 0);

                angle += Math.PI / 50;
            }
        }
    }
    public void Highlight(int startTime, int endTime)
    {
        for (int i = 0; i < 7; i++)
        {
            var fadeTime = generator.Random(1000, 2500);
            var sprite = generator.GetLayer("Highlight").CreateSprite("sb/hl.png");
            var delay = generator.Beatmap.GetTimingPointAt(startTime).BeatDuration * 1.5;

            for (double t = startTime + delay * i; t < endTime; t += fadeTime * 2)
            {
                var pos = new Vector2(generator.Random(0, 747), generator.Random(40, 440));
                var newPos = new Vector2(generator.Random(-107, 854), generator.Random(0, 480));
                var fade = Math.Round(generator.Random(0.05, 0.1), 2);
                sprite.Move(OsbEasing.Out, t, t + fadeTime * 2, pos, newPos);
                sprite.Fade(OsbEasing.Out, t, t + fadeTime / 4, 0, fade);
                sprite.Fade(OsbEasing.In, t + fadeTime, t + fadeTime * 2, fade, 0);
                sprite.Scale(OsbEasing.Out, t, t + fadeTime * 2, Math.Round(generator.Random(0.4, 1), 2), 0.2);
            }
        }
    }
    public void Rain(int startTime, int endTime, double intensity, int type)
    {
        for (int i = 0; i < intensity * 10; i++)
        {
            int particleSpeed = generator.Random(300, 400);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 15, posX + 15);
            double angle = Math.Atan2(680, endX - posX);
            int delay = 20;
            int duration = endTime - startTime - i * delay;
            string layer = "rain";

            var sprite = generator.GetLayer(layer).CreateSprite("sb/pl.png", OsbOrigin.Centre, new Vector2(posX, 20));
            if (type == 2)
            {
                delay += 80;
                layer = "rain ";
                duration = endTime - startTime - i * (int)(delay / 1.6);
                sprite.Additive(startTime + i * delay);
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
            sprite.Fade(startTime + i * delay, Math.Round(generator.Random(0.15, 0.5), 2));
            sprite.Scale(startTime + i * delay, Math.Round(generator.Random(0.03, 0.05), 2));

            var splash = generator.GetLayer(layer).CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(posX, 460));
            splash.StartLoopGroup(startTime + i * delay + particleSpeed, duration / particleSpeed);
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 450, generator.Random(390, 430));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, Math.Round(generator.Random(0.03, 0.04), 3), 0);
            splash.EndGroup();
        }
    }
    public void SquareTransition(int startTime, int endTime, bool In, float squareScale, Color4 color, OsbEasing easing, bool Full = false, string layer = "transition")
    {
        float posX = -107;
        float posY = 40;
        int duration = endTime - startTime;

        while (posX < 737 + squareScale)
        {
            while (posY < 437 + squareScale)
            {
                var sprite = generator.GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, posY));

                if (Full == false)
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
    public void TransitionLines(int startTransition, int endTransition, int endTime, string layer = "transition", bool fullscreen = false)
    {
        var transitionColor = new Color4(0.05f, 0.05f, 0.05f, 1);
        int transitionDuration = endTransition - startTransition;
        int delay = 0;
        int posX = -120;
        int scaleY = 406;
        if (fullscreen)
            scaleY = 484;
        for(int i = 0; i < 60; i++)
        {
            var sprite = generator.GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, 240));
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
        for(int i = 0; i < gearNumber; i ++)
        {  
            int baseYPos = generator.Random(0, 480);
            int duration = generator.Random(15000, 30000);
            bool isLeft = generator.Random(0,2) == 0 ? true : false;
            var sprite = generator.GetLayer(layer).CreateSprite($"sb/g/g{generator.Random(1, 7)}.png", OsbOrigin.Centre, new Vector2(i % 2 == 0 ? -107 : 747, baseYPos));
            sprite.Fade(startTime + (i * 50), startTime + (i * 50) + 300, 0, 1);
            sprite.Fade(endTime - 1000, endTime, 1, 0);
            sprite.Scale(startTime, generator.Random(0.1, maxScale));
            sprite.Color(startTime, colorDark, colorDark, colorDark);
            sprite.Rotate(startTime, endTime, 0, generator.Random(0, 2) == 0 ? generator.Random(-5, -1) : generator.Random(1, 5));
            sprite.MoveY(startTime, endTime, baseYPos, baseYPos + generator.Random(-100, 100));
            colorDark += 0.5f / gearNumber;
            maxScale -= 0.3 / gearNumber;
        }
    }
    public void DiamondCross(int startTime, int endTime, double startScale, double endScale, bool upScale, string layer = "cross", OsbEasing easing = OsbEasing.OutQuint)
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

            double startBorderScale = Math.Sqrt(startScale * startScale + startScale * startScale);
            double endBorderScale = Math.Sqrt(endScale * endScale + endScale * endScale);

            var sprite = generator.GetLayer(layer).CreateSprite("sb/p.png", OsbOrigin.BottomCentre);
            sprite.ScaleVec(easing, startTime, endTime, upScale ? 0 : 50, startBorderScale + (upScale ? 0 : 25), upScale ? 50 : 0, endBorderScale + (upScale ? 25 : 0));
            sprite.Rotate(startTime, angle - Math.PI / 4);
            sprite.Move(easing, startTime, endTime, startPosition, endPosition);
            
            if (!upScale)
                sprite.Fade(endTime - duration / 2, endTime - duration / 5, 1, 0);

            angle += Math.PI / 2;
        }
    }
}