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

    public GameState(int pRadius, Vec2 pPos, bool pMoving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        myGame = (MyGame)game;
        radius = pRadius;
        moving = pMoving;

        
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void Update()
    {
        if(myGame.levelIndex == 2)
        {
            ballPos = new Vec2(779, 849);
            ballSpeed = new Vec2(0,-13);
        }
        else if(myGame.levelIndex == 5)
        {
            ballPos = new Vec2(588, 800);
            ballSpeed = new Vec2(0,-13);
        }
        else if (myGame.levelIndex == 8)
        {
            ballPos = new Vec2(500, 800);
            ballSpeed = new Vec2 (19,0);
        }
        switch (gamestate)
        {
            case 0:
                Draw(255, 0, 0);

                break;

            case 1:
                Draw(0, 255, 0);

                break;
        }

        

        if (newGolfball == null || MyGame.Approximate(newGolfball.velocity, new Vec2(0, 0), 0.1f))
        {
            if (gamestate == 1 || newGolfball == null)
            {
                newGolfball = new Golfball(ballPos);
                myGame.InstantiateBall(newGolfball);
                gamestate = 0;
            }
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            if (gamestate == 0)
            {
                gamestate = 1;
                myGame.score -= 500;
                newGolfball.velocity = ballSpeed;
            }
        }
    }

}


