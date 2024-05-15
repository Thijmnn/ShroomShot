using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


    public class IngameMenu : Sprite
    {
        LevelButton backtomenu;
        LevelButton continueButton;

        MyGame myGame;
        public IngameMenu() : base("BackgroundLevelSelect.png", false,false) 
        {
            myGame = (MyGame)game;

            backtomenu = new LevelButton(new Vec2(400,400), "BackToMenuButtonPlaceholder.png", 1);
            AddChild(backtomenu);

            continueButton = new LevelButton(new Vec2(800, 800), "ContinueButtonPlaceholder.png", myGame.levelIndex - 2);
            AddChild(continueButton);
        }

        void Update()
        {
            CheckInput();
        }

    void CheckInput()
    {
        
    }

    }

