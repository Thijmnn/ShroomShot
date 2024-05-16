using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameState : EasyDraw
{
    public readonly int radius;
    public readonly bool moving;
    public static int gamestate;
    
    Golfball newGolfball;
    MyGame myGame;
    Vec2 ballPos;
    Vec2 ballSpeed;
    AnimationSprite GarryTopDownSprite;

    Sound GolferHit;

    bool GarryAnimate;
    public GameState(int pRadius, Vec2 pPos, bool pMoving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        myGame = (MyGame)game;
        radius = pRadius;
        moving = pMoving;
        GarryTopDownSprite = new AnimationSprite("GarryTopDownSprite.png", 7, 1);
    }

    void Draw(byte red, byte green, byte blue, byte alpha)
    {
        Fill(red, green, blue, alpha);
        Stroke(red, green, blue,alpha);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void Update()
    {
        if (GarryAnimate)
        {
            GarryTopDownSprite.Animate(0.1f);
        }

        if (GarryTopDownSprite.currentFrame == 3)
        {
            if (gamestate == 0)
            {
                GolferHit = new Sound("Golf_Hit1.wav");
                GolferHit.Play();
                gamestate = 1;
                myGame.score -= 500;
                newGolfball.velocity = ballSpeed;
            }
        }
        if (GarryTopDownSprite.currentFrame == 6)
        {
            GarryAnimate = false;
            GarryTopDownSprite.currentFrame = 0;
        }

        if (myGame.levelIndex == 2)
        {
            ballPos = new Vec2(767, 831);
            ballSpeed = new Vec2(0,-13);
            AddChild(GarryTopDownSprite);
            GarryTopDownSprite.SetXY(607, 751);
        }
        else if(myGame.levelIndex == 5)
        {
            ballPos = new Vec2(576, 802);
            ballSpeed = new Vec2(0.6f,-12);
            AddChild(GarryTopDownSprite);
            GarryTopDownSprite.SetXY(407, 751);
        }
        else if (myGame.levelIndex == 8)
        {
            ballPos = new Vec2(493, 638);
            ballSpeed = new Vec2 (19,0);
            AddChild(GarryTopDownSprite);
            GarryTopDownSprite.SetXY(500, 500);
            GarryTopDownSprite.rotation = 90;

        }
        switch (gamestate)
        {
            case 0:
                Draw(255, 0, 0,0);
                Stroke(255, 0, 0, 0);
                break;

            case 1:
                Draw(0, 255, 0,0);
                Stroke(0, 255, 0, 0);
                break;
        }

        if (newGolfball == null || MyGame.Approximate(newGolfball.velocity, new Vec2(0, 0), 0.1f))
        {
            Console.WriteLine(" 111111111111");
            if (gamestate == 1 || newGolfball == null)
            {
                Console.WriteLine(" 22222222222222222222");
                if (newGolfball != null)
                {
                    Console.WriteLine(" 3333333333333333");
                    newGolfball.sprite.color = 0x55555555;
                    newGolfball.collidable = false;
                }
                newGolfball = new Golfball(ballPos);
                myGame.InstantiateBall(newGolfball);
                gamestate = 0;
            }
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            if (gamestate == 0)
            {
                GarryAnimate = true;
            }
            
        }
    }

}


