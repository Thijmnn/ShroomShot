using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PolyShape : GameObject
    {
        MyGame myGame;
        List<Vec2> points;
        List<NLineSegment> lines;
        bool selected = false;
        bool movable;
        float width = 0;
        float height = 0;
        Sprite sprite;
        string typeName;

        public PolyShape(Vec2 pos, List<Vec2> nPoints, Sprite nSprite = null, bool nMovable = true)
        {
            typeName = "polyshape";
            movable = nMovable;
            points = nPoints;
            x = pos.x;
            y = pos.y;
            lines = new List<NLineSegment>();
            myGame = (MyGame)game;
            if (nSprite != null)
            {
                sprite = nSprite;
                AddChild(sprite);
            }
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].x > width)
                {
                    width = points[i].x;
                }
                if (points[i].y > height)
                {
                    height = points[i].y;
                }
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                Console.WriteLine(i);
                NLineSegment line = new NLineSegment(points[i] + pos, points[i + 1] + pos, 0xff00ff00, 4);
                lines.Add(line);
                myGame.AddLine(line);
            }
        }

        Ball ball;
        int radius;

        public PolyShape(Vec2 pos, int nRadius, bool nMovable)
        {
            radius = nRadius;
            typeName = "circle";
            movable = nMovable;
            x = pos.x - radius;
            y = pos.y - radius;
            width = radius * 2;
            height = radius * 2;
            myGame = (MyGame)game;
            ball = new Ball(radius, pos, default, false);
            myGame.InstantiateBall(ball);
            ball.position = new Vec2(x, y);
            ball.Step();
        }

        void SetOnMouse()
        {
            if (typeName == "polyshape")
            {
                x = Input.mouseX - width / 2;
                y = Input.mouseY - height / 2;
                for (int i = 0; i < lines.Count(); i++)
                {
                    lines[i].start = new Vec2(x, y) + points[i];
                    //lines[i].start -= new Vec2(width/2, height/2);
                    lines[i].end = new Vec2(x, y) + points[i + 1];
                    //lines[i].end -= new Vec2(width / 2, height / 2);
                }
            }
            if (typeName == "circle")
            {
                x = Input.mouseX;
                y = Input.mouseY;
                Console.WriteLine( x + " " +y);
                ball.position = new Vec2(x, y);
                ball.Step();
            }
        }

        public void SetPos(Vec2 pos)
        {
            x = pos.x;
            y = pos.y;
            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i].start = pos + points[i];
                lines[i].end = pos + points[i + 1];
            }
        }

        public void Rotate(int angle)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Vec2 point = points[i];
                point.RotateAroundDegrees(angle, new Vec2(width / 2, height / 2));
                points[i] = point;
                if (sprite != null)
                    sprite.SetXY(points[i].x, points[i].y);
            }
            if (sprite != null)
                sprite.rotation += angle;
            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i].start = new Vec2(x, y) + points[i];
                //lines[i].start -= new Vec2(width/2, height/2);
                lines[i].end = new Vec2(x, y) + points[i + 1];
                //lines[i].end -= new Vec2(width / 2, height / 2);
            }
        }

        void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!selected)
                {
                    if (typeName == "polyshape")
                    {
                        if (Input.mouseX > x && Input.mouseY > y && Input.mouseX < x + width && Input.mouseY < y + height)
                        {
                            selected = true;
                        }
                    }
                    if (typeName == "circle")
                    {
                        if ((new Vec2(x, y) - new Vec2(Input.mouseX, Input.mouseY)).Length() < radius)
                        {
                            selected = true;
                        }
                    }
                }
                else if (selected)
                {
                    selected = false;
                }
            }
            if (Input.GetKeyDown(Key.R) && selected)
            {
                Rotate(45);
            }
            if (Input.GetKeyDown(Key.Q))
            {
                SetPos(new Vec2(0, 0));
            }
        }

        void Update()
        {
            if (GameState.gamestate == 0)
            {
                if (movable)
                {
                    CheckInput();
                    if (selected)
                    {
                        SetOnMouse();
                    }
                }
            }
        }
    }
}