using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;

public class Scripts
{
    private StoryboardObjectGenerator generator;
    public Scripts(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateFog(int startTime, int endTime, int posY, int quantity, Color4 color, double fade, string layer = "Fog", int stroke = 60)
    {
        for (int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(1000, 20000) + i * generator.Random(1, 200);
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
                particle.Scale(startTime, generator.Random(0.02, 0.025));
                particle.Color(startTime, color);
                particle.Additive(startTime);
                if (startTime + firstTimeDuration + 5000 > endTime)
                    particle.Scale(startTime + firstTimeDuration, 0);

                particleStartTime += firstTimeDuration;
                while (particleStartTime + 4000 < endTime)
                {
                    int NewDuration = generator.Random(7500, 30000);
                    int particleEndTime = particleStartTime + NewDuration;
                    particle.MoveX(particleStartTime, particleEndTime, generator.Random(-127, -107), endX);
                    particleStartTime += NewDuration;
                }
            }
            var sprite = generator.GetLayer(layer).CreateSprite($"sb/s/s{generator.Random(0, 9)}.png", OsbOrigin.Centre, new Vector2(0, generator.Random(posY - stroke, posY + stroke)));
            sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
            sprite.Fade(startTime, startTime + 1000, 0, fade);
            sprite.Fade(endTime, endTime + 1000, fade, 0);
            sprite.Color(startTime, color);
            sprite.Scale(startTime, generator.Random(0.5, 1.1));
            if (startTime + firstTimeDuration + 5000 > endTime)
                sprite.Scale(startTime + firstTimeDuration, 0);

            elementStartTime += firstTimeDuration;
            while (elementStartTime + 4000 < endTime)
            {
                int newDuration = generator.Random(7500, 30000);
                int elementEndTime = elementStartTime + newDuration;
                sprite.MoveX(elementStartTime, elementEndTime, generator.Random(-227, -220), endX);
                elementStartTime += newDuration;
            }
        }
    }
    public void GenerateDanmaku(int startTime, int endTime, int speed)
    {
        Vector2 basePosition = new Vector2(320, 240);
        for (int i = 0; i < 4; i++)
        {
            double angle = (Math.PI / 2) * i;
            for (int l = 0; l < 50; l++)
            {
                var endPosition = new Vector2(
                    (float)(320 + Math.Cos(angle) * 450),
                    (float)(240 + Math.Sin(angle) * 450)
                );

                var sprite = generator.GetLayer("PARTICLES").CreateSprite("sb/p.png", OsbOrigin.Centre);
                sprite.StartLoopGroup(startTime + l * 100, (endTime - startTime - l * 35) / speed);
                sprite.Move(OsbEasing.OutSine, 0 + l, speed, basePosition, endPosition);
                sprite.Fade(0, 0);
                sprite.Fade(speed / 6, speed / 2, 0, 1);
                sprite.ScaleVec(0, speed, 10, 1, 10, 0);
                sprite.Rotate(OsbEasing.InSine, 0, speed, angle, angle - 1.5);
                sprite.EndGroup();
                sprite.Fade(endTime, endTime + 150, 1, 0);

                angle += Math.PI / 50;
            }
        }
    }
    public void GenerateLights(int startTime, int endTime, double fade)
    {
        for (int i = 0; i < 15; i++)
        {
            int speed = 5000;
            int duration = endTime - startTime - i * 100;
            double angle = generator.Random(0, Math.PI * 2);
            float radius = generator.Random(20, 400);
            var startPos = new Vector2(generator.Random(-77, 707), generator.Random(40, 440));
            var endPos = new Vector2((float)(startPos.X + Math.Cos(angle) * radius), (float)(startPos.Y + Math.Sin(angle) * radius));
            var sprite = generator.GetLayer("0").CreateSprite("sb/hl.png", OsbOrigin.Centre, startPos);

            sprite.Scale(startTime + i * 100, 0.4);
            sprite.StartLoopGroup(startTime + i * 100, duration / speed);
            sprite.Move(OsbEasing.InOutSine, 0, speed, startPos, endPos);
            sprite.Fade(0, 500, 0, fade);
            sprite.Fade(speed - 500, speed, fade, 0);
            sprite.EndGroup();
            sprite.Additive(startTime + i * 100);
        }
    }
    public void GenerateRain(int startTime, int endTime, double intensity, bool alt = false)
    {
        for (int i = 0; i < intensity * 10; i++)
        {
            int particleSpeed = generator.Random(275, 350);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 15, posX + 15);
            double angle = Math.Atan2(680, endX - posX);
            int delay = 15;
            int duration = endTime - startTime - i * delay;
            string layer = "rain";

            if (alt)
            {
                delay += 85;
                layer = "rain ";
                duration = endTime - startTime - i * (delay / 2);
            }

            var sprite = generator.GetLayer(layer).CreateSprite("sb/pl.png", OsbOrigin.Centre, new Vector2(posX, 20));
            sprite.StartLoopGroup(startTime + i * delay, duration / particleSpeed);
            sprite.MoveY(0, particleSpeed, 20, 460);
            sprite.MoveX(0, particleSpeed, posX, endX);
            sprite.Rotate(0, particleSpeed, Math.PI / 2, angle);
            sprite.EndGroup();
            sprite.Fade(startTime + i * delay, generator.Random(0.15, 0.5));
            sprite.Scale(startTime + i * delay, generator.Random(0.03, 0.05));
            sprite.Additive(startTime + i * delay);

            var splash = generator.GetLayer(layer).CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(posX, 460));
            splash.StartLoopGroup(startTime + (i * delay) + particleSpeed, duration / particleSpeed);
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 460, generator.Random(400, 450));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, generator.Random(0.045, 0.055), 0);
            splash.EndGroup();
        }
    }
    public void SquareTransition(int startTime, int endTime, bool In, float squareScale, Color4 color, OsbEasing easing, bool Full = false)
    {
        float posX = -107;
        float posY = 40;
        int duration = endTime - startTime;

        while (posX < 737 + squareScale)
        {
            while (posY < 437 + squareScale)
            {
                var sprite = generator.GetLayer("transition!").CreateSprite("sb/0.png", OsbOrigin.Centre, new Vector2(posX, posY));

                if (Full == false)
                {
                    sprite.Scale(easing, startTime, endTime, 0, squareScale / 2);
                    sprite.Rotate(easing, startTime, endTime, Math.PI, 0);
                }
                else
                {
                    sprite.Scale(OsbEasing.InSine, startTime, endTime - duration / 2, 0, squareScale / 2);
                    sprite.Scale(OsbEasing.OutSine, endTime - duration / 2, endTime + 1000, squareScale / 2, 0);
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
            var sprite = generator.GetLayer(layer).CreateSprite("sb/0.png", OsbOrigin.Centre, new Vector2(posX, 240));
            sprite.ScaleVec(startTransition + delay, startTransition + delay + 300, 0, scaleY / 2, 7.465, scaleY / 2);
            sprite.Fade(endTime, endTime + 1000, 1, 0);
            sprite.Rotate(startTransition, 0.1);
            sprite.Color(startTransition, transitionColor);
            
            delay += transitionDuration / 60;
            posX += 15;
        }
    }
    public void GenerateGears(int startTime, int endTime, int gearNumber, string layer)
    {   
        float colorDark = 0.12f;
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
}