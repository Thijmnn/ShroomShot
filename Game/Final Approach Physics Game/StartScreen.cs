using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartScreen : Sprite
{
    MyGame myGame;
    public StartScreen() : base("MainScreenFinal.png", false, false)
    {
        width = game.width;
        height = game.height;
        myGame = (MyGame)game;

        LevelButton startbutton = new LevelButton(new Vec2(630,600),"PlayButtonFinal.png",myGame.levelIndex +1);

        //startbutton.x = game.width / 2 - startbutton.width / 2;
        //startbutton.y = game.height - startbutton.height * 1.5f;
        AddChild(startbutton);
    }



}