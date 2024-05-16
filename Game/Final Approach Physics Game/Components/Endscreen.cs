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

        
        SetXY(200, 200);
    }
    void Update()
    {
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
        myGame.score = 10500;   
    }

   
}

