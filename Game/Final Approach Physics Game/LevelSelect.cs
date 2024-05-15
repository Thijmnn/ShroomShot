using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class LevelSelect : AnimationSprite
{
    Sprite Levelpath;

    MyGame myGame;

    LevelButton Level1;
    LevelButton Level2;
    LevelButton Level3;

    public LevelSelect() : base("BackgroundLevelselectFinal.png", 8, 13,-1,false,false)
    {
        Levelpath = new Sprite("LevelPathFinal.png");
        AddChild(Levelpath);

        Level1 = new LevelButton(new Vec2(165, 195), "Level1IslandFinal.png", 2, 1);
        AddChild(Level1);  
        Level1.width = 300;
        Level1.height = 300;

        Level2 = new LevelButton(new Vec2(200, 400), "Level1PlaceHolder.png", 5, 2);
        AddChild(Level2);

        Level3 = new LevelButton(new Vec2(200, 700), "Level1PlaceHolder.png", 8, 2);
        AddChild(Level3);

        myGame = (MyGame)game;
        
    }

    void Update()
    {
        Animate(0.3f);
        //CheckInput();
        if (myGame.Level1Complete)
        {
            Level2.SetCycle(0, 1);
        }
        else
        {
            Level2.SetCycle(1, 1);
        }
        if (myGame.Level2Complete)
        {
            Level3.SetCycle(0, 1);
        }
        else
        {
            Level3.SetCycle(1, 1);
        }
    }
}
