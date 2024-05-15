using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LevelButton : AnimationSprite
    {
        MyGame myGame;
        //public delegate void OnButtonClicked(int index);
        //public OnButtonClicked onButtonClicked;
        int index;
        public bool active;

        public LevelButton(Vec2 pos, string fileName, int nIndex ,int cols = 1, int rows = 1) : base(fileName, cols, rows, -1, false, false)
        {
            x = pos.x;
            y = pos.y;
            active = true;
            myGame = (MyGame)game;
            index = nIndex;
        }
        void Update()
        {
            if (active)
            {
                CheckInput();
            }
        }

        void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
                if (Input.mouseX > x && Input.mouseY > y && Input.mouseX < x + width && Input.mouseY < y + height)
                {
                    //onButtonClicked.Invoke(index);
                    myGame.LoadLevel(index);
                }
            }
        }
    }
}
