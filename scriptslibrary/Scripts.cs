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
    public void GenerateFog(int startTime, int endTime, int posY, int quantity, Color4 color, double fade, int stroke = 60, string layer = "Fog")
    {
        for (int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(2000, 20000);
            int posX = generator.Random(-157, 647);
            int endX = generator.Random(877, 907);
            int speed = generator.Random(7500, 20000);
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
                if (startTime + firstTimeDuration + 7500 > endTime)
                    particle.Scale(startTime + firstTimeDuration, 0);

                particleStartTime += firstTimeDuration;
                while (particleStartTime + 7500 < endTime)
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
            if (startTime + firstTimeDuration + 7500 > endTime)
                sprite.Scale(startTime + firstTimeDuration, 0);

            elementStartTime += firstTimeDuration;
            while (elementStartTime + 7500 < endTime)
            {
                int newDuration = generator.Random(7500, 30000);
                int elementEndTime = elementStartTime + newDuration;
                sprite.MoveX(elementStartTime, elementEndTime, generator.Random(-227, -220), endX);
                elementStartTime += newDuration;
            }
        }
    }
    public void GenerateLights(int startTime, int endTime, double fade)
    {
        for (int i = 0; i < 15; i++)
        {
            int speed = 5000;
            int duration = endTime - startTime - i * 50;
            double angle = generator.Random(0, Math.PI * 2);
            float radius = generator.Random(20, 400);
            var startPos = new Vector2(generator.Random(-77, 707), generator.Random(40, 440));
            var endPos = new Vector2((float)(startPos.X + Math.Cos(angle) * radius), (float)(startPos.Y + Math.Sin(angle) * radius));
            var sprite = generator.GetLayer("0").CreateSprite("sb/hl.png", OsbOrigin.Centre, startPos);

            sprite.Scale(startTime + i * 50, 0.4);
            sprite.StartLoopGroup(startTime + i * 50, duration / speed);
            sprite.Move(OsbEasing.InOutSine, 0, speed, startPos, endPos);
            sprite.Fade(0, 500, 0, fade);
            sprite.Fade(speed - 500, speed, fade, 0);
            sprite.EndGroup();
            sprite.Additive(startTime + i * 50);
        }
    }
    public void GenerateRain(int startTime, int endTime, double intensity, bool alt = false)
    {
        for (int i = 0; i < intensity * 10; i++)
        {
            int particleSpeed = generator.Random(275, 350);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 17, posX + 17);
            double angle = Math.Atan2(680, endX - posX);
            int delay = 20;
            int duration = endTime - startTime - i * delay;
            string layer = "rain";

            if (alt)
            {
                delay += 80;
                layer = "rain ";
                duration = endTime - startTime - i * (delay / 3);
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
    public void SquareTransition(int startTime, int endTime, bool In, float squareScale, Color4 color, OsbEasing easing)
    {
        float posX = -107;
        float posY = 40;

        while (posX < 747 + squareScale)
        {
            while (posY < 437 + squareScale)
            {
                var sprite = generator.GetLayer("transition").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, posY));

                if (In)
                {
                    sprite.Scale(easing, startTime, endTime, 0, squareScale);
                    sprite.Rotate(easing, startTime, endTime, Math.PI, 0);
                }
                else
                {
                    sprite.Scale(easing, startTime, endTime, squareScale, 0);
                    sprite.Rotate(easing, startTime, endTime, 0, -Math.PI);
                }
                sprite.Color(startTime, color);
                posY += squareScale;
            }
            posY = 40;
            posX += squareScale;
        }
    }
}