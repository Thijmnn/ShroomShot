using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Endscreen : AnimationSprite
{
    LevelButton nextlevel;

    Sprite nextlevelsprite;

    MyGame myGame;
    public Endscreen() : base("Endscreen.png",1,4,-1,false,false) 
    {
        myGame = (MyGame)game;
        nextlevel = new LevelButton(new Vec2(800,800), "NextLevelButtonPlaceholder.png", myGame.levelIndex + 3);
        AddChild(nextlevel);
    }
    void Update()
    {
        //checkinput();
    }

    void checkinput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mouseX > nextlevel.x && Input.mouseY > nextlevel.y && Input.mouseX < nextlevel.x + nextlevelsprite.width && Input.mouseY < nextlevel.y + nextlevelsprite.height)
            {
                myGame.LoadLevel(1);
            }
        }
    }
}

