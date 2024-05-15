using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Hole : Sprite
{
    public static Vec2 position;
    public Hole(Vec2 pPos) : base("Hole.png", false, false)
    {
        SetOrigin(width / 2, height / 2);
        position = pPos;
        x = position.x;
        y = position.y;
    }


}
