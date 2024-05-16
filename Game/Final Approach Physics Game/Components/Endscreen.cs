using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Endscreen : AnimationSprite
{

    MyGame myGame;
    public Endscreen() : base("Endscreen.png",1,4,-1,false,false) 
    {
        myGame = (MyGame)game;
        LevelButton nextlevel = new LevelButton(new Vec2(679, 732), "NextButtonFinal (2).png", 1);
        game.AddChild(nextlevel);
        nextlevel.SetScaleXY(0.7f);

        LevelButton retryButton = new LevelButton(new Vec2(681, 786), "NextButtonFinal (2).png", myGame.levelIndex);
        game.AddChild(retryButton);
        retryButton.SetScaleXY(0.7f);

        AnimationSprite scoreText = new AnimationSprite("ScoreTextClear.png", 4, 6);
        AddChild(scoreText);

        scoreText.SetOrigin(scoreText.width/2, scoreText.height/2);
        scoreText.SetXY(width/2,height/2 + 80);
        scoreText.SetScaleXY(1.5f);

        if (scoreText == null)
        {
            scoreText = new AnimationSprite("ScoreText.png", 4, 6);
        }
        switch (myGame.score)
        {
            case 10500:
                scoreText.SetCycle(1, 1);
                break;
            case 10000:
                scoreText.SetCycle(1, 1);
                break;
            case 9500:
                scoreText.SetCycle(2, 1);
                break;
            case 9000:
                scoreText.SetCycle(3, 1);
                break;
            case 8500:
                scoreText.SetCycle(4, 1);
                break;
            case 8000:
                scoreText.SetCycle(5, 1);
                break;
            case 7500:
                scoreText.SetCycle(6, 1);
                break;
            case 7000:
                scoreText.SetCycle(7, 1);
                break;
            case 6500:
                scoreText.SetCycle(8, 1);
                break;
            case 6000:
                scoreText.SetCycle(9, 1);
                break;
            case 5500:
                scoreText.SetCycle(10, 1);
                break;
            case 5000:
                scoreText.SetCycle(11, 1);
                break;
            case 4500:
                scoreText.SetCycle(12, 1);
                break;
            case 4000:
                scoreText.SetCycle(13, 1);
                break;
            case 3500:
                scoreText.SetCycle(14, 1);
                break;
            case 3000:
                scoreText.SetCycle(15, 1);
                break;
            case 2500:
                scoreText.SetCycle(16, 1);
                break;
            case 2000:
                scoreText.SetCycle(17, 1);
                break;
            case 1500:
                scoreText.SetCycle(18, 1);
                break;
            case 1000:
                scoreText.SetCycle(19, 1);
                break;
            case 500:
                scoreText.SetCycle(20, 1);
                break;
            case 0:
                scoreText.SetCycle(21, 1);
                break;
        }
        scoreText.Animate(0.5f);

        SetXY(200, 200);

        if (myGame.score >= 8000)
        {
            SetCycle(0, 1);
        }
        if (myGame.score >= 5000 && myGame.score <= 7999)
        {
            SetCycle(1, 1);
        }
        if (myGame.score >= 2000 && myGame.score <= 4999)
        {
            SetCycle(2, 1);
        }
        if (myGame.score >= 0 && myGame.score <= 1999)
        {
            SetCycle(3, 1);
        }
        Animate(0.3f);
        myGame.score = 10500;
    }
    void Update()
    {
        
    }

   
}

