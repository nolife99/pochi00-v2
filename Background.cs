using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    class Background : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            ColorBackground();
            Section1();
            Section2();
            Section3();
        }
        private void ColorBackground()
        {
            var box1 = GetLayer("Letterbox").CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            box1.Color(6694, Color4.Black);
            box1.ScaleVec(6694, 7693, 854, 0, 854, 40);
            box1.ScaleVec(629888, 630888, 854, 40, 854, 0);

            var box2 = GetLayer("Letterbox").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            box2.Color(6694, Color4.Black);
            box2.ScaleVec(6694, 7693, 854, 0, 854, 40);
            box2.ScaleVec(629888, 630888, 854, 40, 854, 0);
            
            var b = GetLayer("Back").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, 240));
            b.ScaleVec(27, 854, 480);
            b.Color(27, new Color4(.1f, 0.04f, 0.1f, 1));
            b.Fade(27, 1026, 0, 1);
            b.Fade(6693, 7693, 1, 0);
            b.ScaleVec(70694, 854, 400);
            b.Color(70694, new Color4(.05f, 0.1f, 0.2f, 1));
            b.Fade(70694, 71694, 0, 1);
            b.Fade(81360, 81694, 1, 0);
            b.Color(102694, new Color4(20, 0, 20, 1));
            b.Fade(102694, 103694, 0, 1);
            b.Fade(123694, 124027, 1, 0.5);
            b.Color(124027, 126694, new Color4(20, 0, 20, 1), new Color4(173, 216, 230, 1));
            b.Fade(124027, 126694, 0.5, 0.15);
            b.Fade(145360, 0);
            b.Color(203419, new Color4(15, 2, 15, 1));
            b.Fade(203419, 204419, 0, 1);
            b.Color(216661, 217661, new Color4(15, 2, 15, 1), new Color4(25, 2, 2, 1));
            b.Color(229902, 230902, new Color4(25, 2, 2, 1), new Color4(2, 2, 15, 1));
            b.Color(256385, 257385, new Color4(2, 2, 15, 1), new Color4(2, 15, 2, 1));
            b.Fade(272937, 273937, 1, 0);
            b.Color(289488, new Color4(224, 255, 255, 1));
            b.Fade(289488, 0.2);
            b.Fade(302730, 304730, 0.2, 0);
            b.Color(315971, new Color4(25, 25, 25, 1));
            b.Fade(315971, 316971, 0, 1);
            b.Fade(332523, 0);
            b.Color(359006, new Color4(5, 12, 12, 1));
            b.Fade(359006, 360006, 0, 1);
            b.Fade(380543, 0);
            b.Color(423222, new Color4(25, 25, 25, 1));
            b.Fade(423222, 424222, 0, 1);
            b.Color(433889, 434889, new Color4(25, 25, 25, 1), new Color4(38, 30, 25, 1));
            b.Fade(444555, 445555, 1, 0);
            b.Color(465889, new Color4(128, 128, 128, 1));
            b.Fade(465889, 0.2);
            b.Fade(473889, 0);
            b.Color(527221, Color4.White);
            b.Fade(527221, 0.1);
            b.Fade(548555, 549888, 0.125, 0.1);
            b.Fade(553877, 0);

            var h = GetLayer("Back1").CreateSprite("sb/hl.png", OsbOrigin.Centre, new Vector2(320, 240));
            h.Scale(27, 0.5);
            h.Color(27, Color4.Black);
            h.Fade(27, 1);
            h.Fade(5776, 6692, 1, 0);
            h.Fade(70694, 71694, 0, 1);
            h.Fade(81360, 82360, 1, 0);
            h.Fade(102694, 103527, 0, 1);
            h.Fade(123527, 124026, 1, 0);
            h.Fade(203420, 204420, 0, 1);
            h.Fade(272937, 273937, 1, 0);
            h.Fade(315971, 316971, 0, 1);
            h.Fade(332523, 0);
            h.Fade(423222, 424222, 0, 1);
            h.Fade(444555, 445555, 1, 0);

            var f = GetLayer("Flash").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, 240));
            f.ScaleVec(28027, 854, 400);
            f.Additive(28027);
            f.Fade(28027, 29027, 0.8, 0);
            f.Fade(70694, 72694, 0.8, 0);
            f.Fade(134694, 135694, 0.3, 0);
            f.Fade(145360, 146360, 0.8, 0);
            f.Fade(156027, 157027, 0.8, 0);
            f.Fade(168027, 169027, 0.8, 0);
            f.Fade(178694, 179694, 0.8, 0);
            f.Fade(189360, 190360, 0.8, 0);
            f.Fade(195027, 196027, 0.8, 0);
            f.Fade(200027, 202027, 0.8, 0);
            f.Fade(277902, 278902, 0.3, 0);
            f.Fade(280385, 281385, 0.3, 0);
            f.Fade(332523, 333523, 0.8, 0);
            f.Fade(345764, 346764, 0.8, 0);
            f.Fade(380555, 383222, 0.8, 0);
            f.Fade(401889, 403222, 0.8, 0);
            f.Fade(473889, 474889, 0.8, 0);
            f.Fade(484555, 485555, 0.8, 0);
            f.Fade(527221, 530221, 0.8, 0);
            f.Fade(553888, 555221, 0.8, 0);

            var bMap = GetMapsetBitmap("sb/vig.png");
            var v = GetLayer("Vignette").CreateSprite("sb/vig.png", OsbOrigin.Centre, new Vector2(320, 240));
            v.Scale(124027, 854f / bMap.Width);
            v.Fade(124027, 124360, 0, 1);
            v.Fade(144027, 145360, 1, 0);
            v.Fade(276247, 1);
            v.Fade(289488, 0);
            v.Fade(465805, 466222, 0, 1);
            v.Fade(473889, 0);
            v.Fade(500555, 1);
            v.Fade(553888, 0);

            var gradient = GetLayer("gradient").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new Vector2(320, 480));
            gradient.Color(380556, 25, 25, 25);
            gradient.Fade(380556, 391222, 0, 0.12);
            gradient.Rotate(391222, -Math.PI / 2);
            gradient.ScaleVec(391222, 0.7, 9);
            gradient.Fade(401555, 401868, 0.12, 0);
        }
        private void Section1()
        {
            var grey = GetLayer("Layered1").CreateSprite("sb/b/0/w.jpg", OsbOrigin.Centre, new Vector2(320, 240));
            grey.Additive(6693);
            grey.Fade(OsbEasing.InSine, 6693, 28027, 0, 1);
            grey.Scale(OsbEasing.InSine, 6693, 28027, 0.24, 0.23076925);
            grey.Fade(OsbEasing.OutSine, 28027, 29360, 1, 0);

            var l0 = GetLayer("Layered1").CreateSprite("sb/b/0/l0.png", OsbOrigin.Centre, new Vector2(320, 240));
            l0.Scale(28110, 0.23076925);
            l0.Fade(28110, 29110, 0, 1);
            l0.Fade(70694, 0);

            var l1 = GetLayer("Layered1").CreateSprite("sb/b/0/l1.png", OsbOrigin.Centre, new Vector2(320, 0));
            l1.Scale(28110, 0.23076925);
            l1.Fade(28110, 29110, 0, 1);
            l1.MoveY(OsbEasing.OutExpo, 28110, 70694, 220, 250);
            l1.Fade(70694, 0);

            var l2 = GetLayer("Layered01").CreateSprite("sb/b/0/l2.png", OsbOrigin.Centre, new Vector2(320, 0));
            l2.Scale(28110, 0.23076925);
            l2.Fade(28110, 29110, 0, 1);
            l2.MoveY(OsbEasing.OutExpo, 28110, 70694, 220, 305);
            l2.Fade(70694, 0);

            var l3 = GetLayer("Layered01").CreateSprite("sb/b/0/l3.png", OsbOrigin.Centre, new Vector2(320, 0));
            l3.Scale(28110, 0.4571429);
            l3.Fade(28110, 29110, 0, 1);
            l3.MoveY(OsbEasing.OutExpo, 28110, 70694, 400, 460);
            l3.Fade(70694, 0);

            var l4 = GetLayer("Layered01").CreateSprite("sb/b/0/l4.png", OsbOrigin.Centre, new Vector2(320, 0));
            l4.Scale(28110, 0.4571429);
            l4.Fade(28110, 29110, 0, 1);
            l4.MoveY(OsbEasing.OutExpo, 28110, 70694, 400, 520);
            l4.Fade(34110, 0);

            var bg = GetLayer("Layered1").CreateSprite("sb/b/0/b.jpg", OsbOrigin.Centre, new Vector2(0, 240));
            bg.Scale(81360, 0.23076925);
            bg.Additive(81360);
            bg.Fade(81360, 81660, 0, 1);
            bg.MoveX(81360, 92027, 335, 305);
            bg.Fade(92027, 102694, 1, 0);
        }
        private void Section2()
        {
            var b = GetLayer("before kiai").CreateSprite("sb/b/1/b.jpg", OsbOrigin.Centre, new Vector2(0, 240));
            b.Scale(145360, 0.23076925);
            b.Additive(145360);
            b.Fade(145360, 145660, 0, 1);
            b.MoveX(145360, 166943, 305, 335);
            b.Fade(166943, 0);

            var l0 = GetLayer("s2").CreateSprite("sb/b/2/l0.png", OsbOrigin.Centre, new Vector2(320, 240));
            l0.Scale(168027, 0.23076925);
            l0.Fade(168027, 169027, 0, 1);
            l0.Fade(200027, 0);

            var l1 = GetLayer("s2").CreateSprite("sb/b/2/l1.png", OsbOrigin.Centre, new Vector2(320, 0));
            l1.Scale(168027, 0.23076925);
            l1.Fade(168027, 169027, 0, 1);
            l1.MoveY(OsbEasing.OutExpo, 168027, 200027, 265, 285);
            l1.Fade(200027, 0);

            var l2 = GetLayer("s2").CreateSprite("sb/b/2/l2.png", OsbOrigin.Centre, new Vector2(320, 0));
            l2.Scale(168027, 0.23076925);
            l2.Fade(168027, 169027, 0, 1);
            l2.MoveY(OsbEasing.OutExpo, 168027, 200027, 320, 360);
            l2.Fade(200027, 0);

            var l3 = GetLayer("s02").CreateSprite("sb/b/2/l3.png", OsbOrigin.Centre, new Vector2(320, 0));
            l3.Scale(168027, 0.23076925);
            l3.Fade(168027, 169027, 0, 1);
            l3.MoveY(OsbEasing.OutExpo, 168027, 200027, 360, 400);
            l3.Fade(200027, 0);

            var l4 = GetLayer("s02").CreateSprite("sb/b/2/l4.png", OsbOrigin.Centre, new Vector2(358, 0));
            l4.Scale(168027, 0.23076925);
            l4.Fade(168027, 169027, 0, 1);
            l4.MoveY(OsbEasing.OutExpo, 168027, 200027, 290, 365);
            l4.Fade(200027, 0);

            var l5 = GetLayer("s02").CreateSprite("sb/b/2/l5.png", OsbOrigin.Centre, new Vector2(320, 0));
            l5.Scale(168027, 0.23076925);
            l5.Fade(168027, 169027, 0, 1);
            l5.MoveY(OsbEasing.OutExpo, 168027, 200027, 345, 450);
            l5.Fade(200027, 0);
        }
        private void Section3()
        {
            var delete = GetLayer("-").CreateSprite("bg.jpg");
            delete.Scale(0, 0);
            
            var l0 = GetLayer("Layered3").CreateSprite("sb/b/5/l0.png", OsbOrigin.Centre, new Vector2(320, 240));
            l0.Scale(332523, 0.23076925);
            l0.Fade(332523, 333523, 0, 1);
            l0.Fade(359005, 0);

            var l1 = GetLayer("Layered3").CreateSprite("sb/b/5/l1.png", OsbOrigin.Centre, new Vector2(320, 0));
            l1.Scale(332523, 0.23076925);
            l1.Fade(332523, 333523, 0, 1);
            l1.MoveY(OsbEasing.OutExpo, 332523, 359005, 330, 355);
            l1.Fade(359005, 0);

            var l2 = GetLayer("Layered3").CreateSprite("sb/b/5/l2.png", OsbOrigin.Centre, new Vector2(320, 0));
            l2.Scale(332523, 0.23076925);
            l2.Fade(332523, 333523, 0, 1);
            l2.MoveY(OsbEasing.OutExpo, 332523, 359005, 257, 290);
            l2.Fade(359005, 0);

            var l3 = GetLayer("Layered3").CreateSprite("sb/b/5/l3.png", OsbOrigin.Centre, new Vector2(320, 0));
            l3.Scale(332523, 0.23076925);
            l3.Fade(332523, 333523, 0, 1);
            l3.MoveY(OsbEasing.OutExpo, 332523, 359005, 340, 410);
            l3.Fade(359005, 0);

            var l4 = GetLayer("Layered3").CreateSprite("sb/b/5/l4.png", OsbOrigin.Centre, new Vector2(115, 0));
            l4.Scale(332523, 0.4571429);
            l4.Fade(332523, 333523, 0, 1);
            l4.MoveY(OsbEasing.OutExpo, 332523, 359005, 195, 280);
            l4.Fade(359005, 0);

            var mainBg = GetLayer("Base").CreateSprite("sb/b/6/b.jpg", OsbOrigin.Centre, new Vector2(0, 240));
            mainBg.Scale(401889, 0.23076925);
            mainBg.Additive(401889);
            mainBg.MoveX(401889, 423222, 335, 305);
            mainBg.Fade(423222, 423305, 1, 0);

            var desert = GetLayer("Desert").CreateSprite("sb/b/7/b.jpg", OsbOrigin.Centre, new Vector2(0, 240));
            desert.Scale(444555, 0.23076925);
            desert.Additive(444555);
            desert.Fade(444555, 445889, 0, 1);
            desert.MoveX(444555, 465804, 305, 335);
            desert.Fade(465804, 0);
            desert.Fade(473889, 1);
            desert.MoveX(473889, 320);
            desert.Scale(473889, 500555, 0.24, 0.223);
            desert.Fade(496555, 500555, 1, 0);

            var flare1 = GetLayer("Desert").CreateSprite("sb/flare.png", OsbOrigin.Centre, new Vector2(0, 40));
            flare1.Scale(444555, 0.6);
            flare1.Additive(444555);
            flare1.Fade(444555, 445555, 0, 0.7);
            flare1.MoveX(444555, 465222, 727, 747);
            flare1.Fade(464889, 465222, 0.7, 0);
            flare1.Fade(473889, 0.7);
            flare1.MoveX(473889, 497222, 727, 747);
            flare1.Fade(496555, 497222, 0.7, 0);

            var flare2 = GetLayer("Desert").CreateSprite("sb/flare2.png", OsbOrigin.TopLeft, new Vector2(0, 40));
            flare2.Scale(444555, 0.68);
            flare2.Additive(444555);
            flare2.Fade(444555, 445555, 0, 0.8);
            flare2.MoveX(444555, 465222, 727, 747);
            flare2.Rotate(444555, 465222, Math.PI / 2, Math.PI / 1.6);
            flare2.Fade(464889, 465222, 0.8, 0);
            flare2.Fade(473889, 0.8);
            flare2.MoveX(473889, 497222, 727, 747);
            flare2.Rotate(473889, 497222, Math.PI / 2, Math.PI / 1.6);
            flare2.Fade(496555, 497222, 0.8, 0);

            var forest = GetLayer("Forest").CreateSprite("sb/b/8/b.jpg");
            forest.Scale(553888, 0.23076925);
            forest.Additive(553888);
            forest.Fade(553888, 1);
            forest.Move(553888, 575209, 320, 250, 320, 230);
            forest.Fade(575220, 0);
            forest.Fade(587221, 1);
            forest.Move(587221, 629512, 305, 240, 335, 240);
            forest.Fade(629512, 0);

            var forestBlur = GetLayer("Forest").CreateSprite("sb/b/8/f.jpg");
            forestBlur.Additive(575221);
            forestBlur.MoveX(575221, 320);
            forestBlur.Scale(575221, 587221, 0.24, 0.223);
            forestBlur.Fade(587221, 588555, 1, 0);
            forestBlur.Scale(587221, 0.23);
            forestBlur.MoveX(587221, 310);
        }
    }
}
