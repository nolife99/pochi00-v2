using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;

public class RainGenerator
{
    private StoryboardObjectGenerator generator;
    public RainGenerator(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateRain(int startTime, int endTime, int intensity)
    {
        for(int i = 0; i < intensity * 10; i++)
        {
            int duration = endTime - startTime;
            int particleSpeed = generator.Random(215, 310);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 10, posX + 10);
            double angle = Math.Atan2(680 - 0, endX - posX);

            var sprite = generator.GetLayer("Particles").CreateSprite("sb/pl.png", OsbOrigin.Centre, new Vector2(posX, 20));
            sprite.StartLoopGroup(startTime + (i * 2.5), duration/particleSpeed);
            sprite.MoveY(0, particleSpeed, 20, 460);
            sprite.MoveX(0, particleSpeed, posX, endX);
            sprite.Rotate(0, particleSpeed, Math.PI/2, angle);
            sprite.EndGroup();
            sprite.Fade(startTime, generator.Random(0.15, 0.5));
            sprite.Scale(startTime, generator.Random(0.04, 0.05));

            var splash = generator.GetLayer("Particles").CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(posX, 460));
            splash.StartLoopGroup(startTime + (i * 2.5) + particleSpeed, duration/particleSpeed);
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 460, generator.Random(400, 450));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, generator.Random(0.04, 0.05), 0);
            splash.EndGroup();
        }
    }
    public void GenerateRainAlt(int startTime, int endTime, int intensity)
    {
        for(int i = 0; i < intensity * 10; i++)
        {
            int duration = endTime - startTime;
            int particleSpeed = generator.Random(215, 310);
            int posX = generator.Random(-106, 747);
            int endX = generator.Random(posX - 10, posX + 10);
            double angle = Math.Atan2(680 - 0, endX - posX);

            var sprite = generator.GetLayer("Particles").CreateSprite("sb/pl.png", OsbOrigin.Centre, new Vector2(posX, 20));
            sprite.StartLoopGroup(startTime + (i * 50), duration/particleSpeed);
            sprite.MoveY(0, particleSpeed, 20, 460);
            sprite.MoveX(0, particleSpeed, posX, endX);
            sprite.Rotate(0, particleSpeed, Math.PI/2, angle);
            sprite.EndGroup();
            sprite.Fade(startTime, generator.Random(0.15, 0.5));
            sprite.Scale(startTime, generator.Random(0.04, 0.05));

            var splash = generator.GetLayer("Particles").CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(posX, 460));
            splash.StartLoopGroup(startTime + (i * 50) + particleSpeed, duration/particleSpeed);
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 460, generator.Random(400, 450));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, generator.Random(0.04, 0.05), 0);
            splash.EndGroup();
        }
    }
}