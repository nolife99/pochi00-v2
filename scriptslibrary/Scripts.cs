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
    public void GenerateFog(int startTime, int endTime, int posY, int quantity, Color4 color, double fade, string layer = "Fog", int stroke = 60)
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
                particle.Scale(startTime, Math.Round(generator.Random(0.015, 0.025), 3));
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

            sprite.Scale(startTime, Math.Round(generator.Random(0.5, 1.1), 2));

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
    public void GenerateDanmaku(int startTime, int endTime, int speed)
    {   
        //overlapped commands in sbrew but not in osu 
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
                sprite.Fade(0, 0);
                sprite.Move(OsbEasing.OutSine, 0, speed, basePosition, endPosition);
                sprite.Fade(speed / 6, speed / 2, 0, 1);
                sprite.ScaleVec(OsbEasing.In, 0, speed, 10, 1, 10, 0);
                sprite.Rotate(OsbEasing.InSine, 0, speed, angle, angle - 1.5);
                sprite.EndGroup();
                sprite.Fade(endTime, endTime + 200, 1, 0);

                angle += Math.PI / 50;
            }
        }
    }
    public void Highlight(int startTime, int endTime, double Fade, int timeStep, bool RandomFade = false)
    {
        for (int i = startTime; i < endTime - 1000; i += timeStep)
        {
            var fadeTime = generator.Random(1000, 2500);
            var sprite = generator.GetLayer("Highlight").CreateSprite("sb/hl.png");
            var pos = new Vector2(generator.Random(0, 727), generator.Random(10, 380));
            var newPos = new Vector2(generator.Random(-107, 854), generator.Random(-17, 480));
            double fade = 0;

            if (RandomFade)
            {
                fade = Math.Round(generator.Random(0.03 + Fade, 0.07 + Fade), 2);
                sprite.Move(OsbEasing.OutSine, i, i + fadeTime * 2, pos, newPos);
            }
            else
            {
                fade = Fade;
                sprite = generator.GetLayer("Highlight").CreateSprite("sb/hl.png", OsbOrigin.Centre, newPos);
            }
            sprite.Fade(i, i + 250, 0, fade);
            if (fade > 0.01)
            {
                sprite.Fade(i + fadeTime, i + fadeTime * 2, fade, 0);
            }
            sprite.Scale(OsbEasing.InOutSine, i, i + fadeTime * 2, Math.Round(generator.Random(0.8, 2), 2), 0);
        }
    }
    public void GenerateRain(int startTime, int endTime, double intensity, int type = 1)
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
            splash.MoveY(OsbEasing.OutExpo, 0, particleSpeed, 460, generator.Random(400, 450));
            splash.Fade(OsbEasing.OutExpo, 0, particleSpeed, 1, 0);
            splash.Scale(OsbEasing.OutExpo, 0, particleSpeed, Math.Round(generator.Random(0.045, 0.055), 3), 0);
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
                var sprite = generator.GetLayer(layer).CreateSprite("sb/0.png", OsbOrigin.Centre, new Vector2(posX, posY));

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
}