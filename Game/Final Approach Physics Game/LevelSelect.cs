using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class LevelSelect : Sprite
{
    Sprite Levelpath;

    MyGame myGame;

    LevelButton Level1;
    LevelButton Level2;
    LevelButton Level3;

    public LevelSelect() : base("0001.png",false,false)
    {
        Levelpath = new Sprite("LevelPathFinal.png");
        AddChild(Levelpath);

        Level1 = new LevelButton(new Vec2(165, 195), "Level1IslandFinal.png", 2, 1);
        AddChild(Level1);  
        Level1.width = 300;
        Level1.height = 300;

        Level2 = new LevelButton(new Vec2(381, 604), "Level2IslandFinal.png", 5, 2);
        AddChild(Level2);
        Level2.width = 300;
        Level2.height = 300;

        Level3 = new LevelButton(new Vec2(730, 200), "Level3IslandFinal.png", 8, 2);
        AddChild(Level3);
        Level3.width = 300;
        Level3.height = 300;

        myGame = (MyGame)game;
    
    }

    void Update()
    {
        
        //CheckInput();
        if (myGame.Level1Complete)
        {
            Level2.SetCycle(1, 1);
            Level2.active = true;
        }
        else
        {
            Level2.SetCycle(0, 1);
            Level2.active = false;
        }
        if (myGame.Level2Complete)
        {
            Level3.SetCycle(1, 1);
            Level3.active = true;
        }
        else
        {
            Level3.SetCycle(0, 1);
            Level3.active = false;
        }
    }
}
