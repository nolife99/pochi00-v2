using System;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;

public class FogGenerator
{
    private StoryboardObjectGenerator generator;
    public FogGenerator(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateFog(int startTime, int endTime, int posY, int stroke, int quantity, Color4 color, double fade, string layer = "PARTICLES")
    {
        for(int i = 0; i < quantity; i++)
        {
            int firstTimeDuration = generator.Random(5000, 30000);
            int posX = generator.Random(-200, 530);
            int endX = generator.Random(830, 835);
            int elementStartTime = startTime;
            int particleEndTime = startTime + firstTimeDuration + generator.Random(15000, 50000);

            for(int p = 0; p < 2; p++)
            {
                var particle = generator.GetLayer(layer).CreateSprite("sb/d.png");
                particle.MoveX(startTime, startTime + firstTimeDuration, generator.Random(posX - 25, posX + 25), endX);
                particle.MoveX(startTime + firstTimeDuration, particleEndTime, generator.Random(-127, -107), endX);
                particle.MoveY(startTime, generator.Random(posY - stroke, posY + stroke));     
                particle.Fade(startTime, startTime + 1000, 0, 1);
                particle.Fade(endTime, endTime + 1000, 1, 0);
                particle.Scale(startTime, generator.Random(0.01, 0.02));
                particle.Color(startTime, color);
                particle.Additive(startTime, endTime);
            }
            var sprite = generator.GetLayer(layer).CreateSprite($"sb/s/s{generator.Random(0, 9)}.png");
            sprite.MoveX(startTime, startTime + firstTimeDuration, posX, endX);
            sprite.Fade(startTime, startTime + 1000, 0, fade);
            sprite.Fade(endTime, endTime + 1000, fade, 0);
            sprite.Color(startTime, color);
            sprite.Scale(startTime, generator.Random(0.5, 1.1));

            elementStartTime += firstTimeDuration;
            while(elementStartTime < endTime)
            {          
                int newDuration = generator.Random(15000, 50000);
                int elementEndTime = elementStartTime + newDuration;
                sprite.MoveX(elementStartTime, elementEndTime, generator.Random(-217, -207), endX);
                sprite.MoveY(elementStartTime, generator.Random(posY - stroke, posY + stroke));
                elementStartTime += newDuration;
            }
        }
    }
}