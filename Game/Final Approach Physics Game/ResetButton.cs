﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;

    public class ResetButton : Sprite
    {
    MyGame myGame;
    public ResetButton(Vec2 pos) : base("ResetButton.png",false,false)
        {
        x = pos.x;
        y = pos.y;
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
            
            if (Input.mouseX > x && Input.mouseY > y && Input.mouseX < x + width && Input.mouseY < y + height)
            {
                myGame.LoadLevel(myGame.levelIndex);
            }
        }
    }
}

