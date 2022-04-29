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
    public void GenerateFog(int startTime, int endTime, int posY, int stroke, int quantity, Color4 color, double fade, string layer = "PARTICLES")
    {
        for(int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(5000, 30000);
            int posX = generator.Random(-200, 530);
            int endX = generator.Random(850, 857);
            int elementStartTime = startTime;
            int particleStartTime = startTime;

            for(int p = 0; p < 2; p++)
            {
                var particle = generator.GetLayer(layer).CreateSprite("sb/d.png");
                particle.MoveX(startTime, startTime + firstTimeDuration, generator.Random(posX - 25, posX + 25), endX);
                particle.MoveY(startTime, generator.Random(posY - stroke, posY + stroke));
                particle.Fade(startTime, startTime + 1000, 0, 1);
                particle.Fade(endTime, endTime + 1000, 1, 0);
                particle.Scale(startTime, generator.Random(0.01, 0.02));
                particle.Color(startTime, color);
                particle.Additive(startTime);

                particleStartTime += firstTimeDuration;
                while (particleStartTime + 4000 < endTime)
                {
                    int NewDuration = generator.Random(10000, 30000);
                    int particleEndTime = particleStartTime + NewDuration;
                    particle.MoveX(particleStartTime, particleEndTime, generator.Random(-127, -107), endX);
                    particleStartTime += NewDuration;
                    particle.Scale(particleEndTime, 0);
                }
            }
            var sprite = generator.GetLayer(layer).CreateSprite($"sb/s/s{generator.Random(0, 9)}.png");
            sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
            sprite.Fade(startTime, startTime + 1000, 0, fade);
            sprite.Fade(endTime, endTime + 1000, fade, 0);
            sprite.Color(startTime, color);
            sprite.Scale(startTime, generator.Random(0.5, 1.1));

            elementStartTime += firstTimeDuration;
            while(elementStartTime + 4000 < endTime)
            {          
                int newDuration = generator.Random(10000, 30000);
                int elementEndTime = elementStartTime + newDuration;
                sprite.MoveX(elementStartTime, elementEndTime, generator.Random(-217, -207), endX);
                sprite.MoveY(elementStartTime, generator.Random(posY - stroke, posY + stroke));
                elementStartTime += newDuration;
                sprite.Scale(elementEndTime, 0);
            }
        }
    }
    public void GenerateLights(int startTime, int endTime, double fade)
    {
        for(int i = startTime; i < endTime; i += 475)
        {
            int duration = 5000;
            double angle = generator.Random(0, Math.PI * 2);
            float radius = generator.Random(20, 400);
            var startPos = new Vector2(generator.Random(-77, 707), generator.Random(40, 440));
            var endPos = new Vector2((float)(startPos.X + Math.Cos(angle) * radius), (float)(startPos.Y + Math.Sin(angle) * radius));
            var sprite = generator.GetLayer("0").CreateSprite("sb/hl.png", OsbOrigin.Centre, startPos);
                
            sprite.Move(OsbEasing.InOutSine, i, i + duration, startPos, endPos);
            sprite.Scale(i, 0.425);
            sprite.Fade(i, i + 1000, 0, fade);
            sprite.Fade(i + duration - 1000, i + duration, fade, 0);
            sprite.Additive(i, i + duration);
        }
    }
    public void GenerateRain(int startTime, int endTime, int intensity, bool alt = false)
    {
        for(int i = 0; i < intensity * 10; i++)
        {
            int duration = endTime - startTime;
            int particleSpeed = generator.Random(215, 310);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 10, posX + 10);
            double angle = Math.Atan2(680, endX - posX);
            int delay = 3;
            string layerx = "rain";

            if (alt)
            {
                delay += 72;
                layerx = "rain ";
            }

            var sprite = generator.GetLayer(layerx).CreateSprite("sb/pl.png", OsbOrigin.Centre, new Vector2(posX, 20));
            sprite.StartLoopGroup(startTime + (i * delay), duration/particleSpeed);
            sprite.MoveY(0, particleSpeed, 20, 460);
            sprite.MoveX(0, particleSpeed, posX, endX);
            sprite.Rotate(0, particleSpeed, Math.PI/2, angle);
            sprite.EndGroup();
            sprite.Fade(startTime, generator.Random(0.15, 0.5));
            sprite.Scale(startTime, generator.Random(0.04, 0.05));

            var splash = generator.GetLayer(layerx).CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(posX, 460));
            splash.StartLoopGroup(startTime + (i * delay) + particleSpeed, duration/particleSpeed);
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 460, generator.Random(400, 450));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, generator.Random(0.04, 0.05), 0);
            splash.EndGroup();
        }
    }
}