using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartButton : Sprite
{
    MyGame myGame;
    public StartButton() : base("PlayButton.png", false, false)
    {
        myGame = (MyGame)game;
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
            if (Input.mouseX > x && Input.mouseY > y && Input.mouseX < x + width && Input.mouseY < y + height)
            {
                myGame.LoadLevel(myGame.levelIndex +1); ;
            }
        }
    }

}

