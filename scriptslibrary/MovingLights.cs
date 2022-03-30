using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;

public class MovingLights
{
    private StoryboardObjectGenerator generator;
    public MovingLights(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateMovingLights(int startTime, int endTime, double fade)
    {
        for(int i = startTime; i < endTime; i += 475)
        {
            int duration = 5000;
            double angle = generator.Random(0, Math.PI * 2);
            float radius = generator.Random(20, 400);
            var startPos = new Vector2(generator.Random(-77, 707), generator.Random(0, 400));
            var endPos = new Vector2((float)(startPos.X + Math.Cos(angle) * radius), (float)(startPos.Y + Math.Sin(angle) * radius));
            var sprite = generator.GetLayer("Particles").CreateSprite("sb/hl.png", OsbOrigin.Centre, startPos);
                
            sprite.Move(OsbEasing.InOutSine, i, i + duration, startPos, endPos);
            sprite.Scale(i, 0.425);
            sprite.Fade(i, i + 1000, 0, fade);
            sprite.Fade(i + duration - 1000, i + duration, fade, 0);
            sprite.Additive(i, i + duration);
        }
    }
}