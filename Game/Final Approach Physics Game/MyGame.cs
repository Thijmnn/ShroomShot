using System;
using System.Windows;
using GXPEngine;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Security.Cryptography;

public class MyGame : Game
{
    
    public static bool Approximate(float a, float b, float c = 0.0001f)
    {
        return Mathf.Abs(a - b) < c;
    }
    public static bool Approximate(Vec2 a, Vec2 b, float c = 0.0001f)
    {
        return Approximate(a.x, b.x, c) && Approximate(a.y, b.y, c);
    }
    
    static void UnitTest()
    {
        Vec2 v1 = new Vec2(2, 4);
        Vec2 v2 = new Vec2(3, 6);
        Vec2 v3 = new Vec2(0, 0);
        Vec2 res;
        float fRes;


        res = v1 + v2;
        Console.WriteLine("Addition works: {0}, Result: {1}, Goal: (5,10)s",
            res.x == 5 && res.y == 10, res);

        res = v1 - v2;
        Console.WriteLine("Subtraction works: {0}, Result: {1}, Goal: (-1,-2)",
            res.x == -1 && res.y == -2, res);

        res = v1 * 2;
        Console.WriteLine("Scalar works: {0}, Result: {1}, Goal: (4,8)",
            res.x == 4 && res.y == 8, res);

        v3.SetXY(1, 1);
        Console.WriteLine("SetXY works: {0}, Result: {1}, Goal: (1,1)",
            v3.x == 1 && v3.y == 1, v3);

        v1 = new Vec2(6, 8);
        Console.WriteLine("Length works: {0}, Result: {1}, Goal: 10",
            Approximate(v1.Length(), 10), v1.Length());

        Console.WriteLine("Normalized works: {0}, Result: {1}, Goal: (0.6, 0.8)",
            v1.Normalized().x == 0.6f && v1.Normalized().y == 0.8f, v1.Normalized());

        v1.Normalize();
        Console.WriteLine("Normalize works: {0}, Result: {1}, Goal: (0.6, 0.8)",
            v1.x == 0.6f && v1.y == 0.8f, v1);

        fRes = 180 * Vec2.Deg2Rad;
        Console.WriteLine("Deg2Rad works: {0}, Result: {1}, Goal: 3.141593",
            fRes == Mathf.PI, fRes);

        fRes = Mathf.PI * Vec2.Rad2Deg;
        Console.WriteLine("Rad2Deg works: {0}, Result: {1}, Goal: 180",
            fRes == 180, fRes);

        v1 = Vec2.GetUnitVectorRad(Mathf.PI);
        Console.WriteLine("GetUnitVectorRad works: {0}, Result: {1}, Goal: (-1,0)",
            Approximate(v1, new Vec2(-1, 0)), v1);

        v1 = Vec2.GetUnitVectorDeg(180);
        Console.WriteLine("GetUnitVectorDeg works: {0}, Result: {1}, Goal: (-1,0)",
            Approximate(v1, new Vec2(-1, 0)), v1);

        Console.WriteLine("�re these random uni vectors??"); // A: No
        //RANDOMUNITVECCCCCCCCCCC
        for (int i = 0; i < 10; i++)  // Makes 10 random vectors with a length of 1
        {
            Vec2 rnd = Vec2.RandomUnitVector();
            Console.WriteLine("Vector: {0} length: {1}", rnd, rnd.Length());
        }

        // week 2:

        Vec2 v = new Vec2(3, 4); // length = 5
        v.SetAngleRadians(Mathf.PI); // now it should be  (-5,0);
        Console.WriteLine("SetAngleRadians works: {0}, Result: {1}, Goal: (-5,0)", Approximate(v, new Vec2(-5, 0)), v);

        v = new Vec2(3, 4); // length = 5
        v.SetAngleDegrees(180); // now it should be  (-5,0);
        Console.WriteLine("SetAngleDegrees works: {0}, Result: {1}, Goal: (-5,0)", Approximate(v, new Vec2(-5, 0)), v);

        v = new Vec2(3, 4); // length = 5
        v.RotateRadians(Mathf.PI); // now it should be (-3,-4)
        Console.WriteLine("RotateRadians works: {0}, Result: {1}, Goal: (-3,-4)", Approximate(v, new Vec2(-3,-4)), v);

        v = new Vec2(3, 4); // length = 5
        v.RotateDegrees(180); // now it should be (-3,-4)
        Console.WriteLine("RotateDegrees works: {0}, Result: {1}, Goal: (-3,-4)", Approximate(v, new Vec2(-3, -4)), v);

        v = new Vec2(3, 4); // length = 5
        Vec2 point = new Vec2(4, 4);
        v.RotateAroundDegrees(180, point); // now it should be (5,4)
        Console.WriteLine("RotateAroundDegrees works: {0}, Result: {1}, Goal: (5,4))", Approximate(v, new Vec2(5,4)), v);

        // week 4

        Vec2 reflect = new Vec2(0, 1);
        Vec2 reflectBounce = new Vec2(1, -1).Normalized();
        reflect.Reflect(1f, reflectBounce);
        Console.WriteLine("reflect ok? {0} Goal (1,0) | is {1}", Approximate(reflect.x, 1) && Approximate(reflect.y, 0), reflect);

        Vec2 dot = new Vec2(2, 3);
        Vec2 dotOther = new Vec2(4, 1);
        Console.WriteLine("dot works: {0} Goal 11 | is {1}", dot.Dot(dotOther) == 11, dot.Dot(dotOther));
            
        Vec2 normal = new Vec2(6, 8);
        Console.WriteLine("normal works: {0} Goal (-0.8,0.6) | is {1}", normal.Normal().x == -0.8f && normal.Normal().y == 0.6f, normal.Normal());
    }

    static void Main() {
        UnitTest();
		new MyGame().Start();
	}

	EasyDraw _text;

    List<Ball> _movers;
    List<NLineSegment> _lines;

    Vec2 mousePos1;
    Vec2 mousePos2;

    public int levelIndex;

    public Hole hole1;
    public Hole hole2;
    public Hole hole3;


    Sprite star1;
    Sprite star2;
    Sprite star3;

    public  int score;

    public bool Level1Complete;
    public bool Level2Complete;

    public MyGame () : base(/*1200, 800*/1920,1080, false,false)
	{

        score = 10500;
        star1 = new Sprite("StarPlaceholder.png", false, false);
        star2 = new Sprite("StarPlaceholder.png", false, false);
        star3 = new Sprite("StarPlaceholder.png", false, false);

        _movers = new List<Ball>();
        _lines = new List<NLineSegment>();

        mousePos1 = new Vec2();
        mousePos2 = new Vec2();

        foreach (Ball b in _movers)
        {
            AddChild(b);
        }

        _text = new EasyDraw (250,40);
		_text.TextAlign (CenterMode.Min, CenterMode.Min);
		AddChild (_text);
        LoadLevel(0);

        Level1Complete = false;
        Level2Complete = false;
}
    public int GetNumberOfLines()
    {
        return _lines.Count;
    }

    public NLineSegment GetLine(int index)
    {
        if (index >= 0 && index < _lines.Count)
        {
            return _lines[index];
        }
        return null;
    }

    public int GetNumberOfMovers()
    {
        return _movers.Count;
    }

    public Ball GetMover(int index)
    {
        if (index >= 0 && index < _movers.Count)
        {
            return _movers[index];
        }
        return null;
    }

    public void RemoveMover(Ball mover)
    {
        _movers.Remove(mover);
    }

    public void AddLine(Vec2 start, Vec2 end)
    {
        NLineSegment line = new NLineSegment(start, end, 0xff00ff00, 4);
        AddChild(line);
        _lines.Add(line);
    }
    public void AddLine(NLineSegment line)
    {
        AddChild(line);
        _lines.Add(line);
    }

    void Update () 
    {
        /*Console.WriteLine(Level1Complete);
        Console.WriteLine(Level2Complete);*/
        for (int i = _movers.Count - 1; i >= 0; i--)
        {
            Ball mover = _movers[i];
            if (mover.moving)
            {
                mover.Step();
            }
        }

        HandleInput();
    }

    public void InstantiateBall(Ball ball)
    {
        _movers.Add(ball);
        AddChild(ball);
    }

    void HandleInput()
    {
        //targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;
        if (Input.GetKeyDown(Key.UP))
        {
            Ball.acceleration.SetXY(0, -1);
        }
        if (Input.GetKeyDown(Key.DOWN))
        {
            Ball.acceleration.SetXY(0, 1);
        }
        if (Input.GetKeyDown(Key.LEFT))
        {
            Ball.acceleration.SetXY(-1, 0);
        }
        if (Input.GetKeyDown(Key.RIGHT))
        {
            Ball.acceleration.SetXY(1, 0);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ball ball = new Ball(10,new Vec2(Input.mouseX, Input.mouseY));
        //    ball.velocity.SetXY(0, -2);
        //    InstantiateBall(ball);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            Console.WriteLine("new Vec2( " + Input.mouseX + ", " + Input.mouseY + "),");
        }
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                mousePos1 = new Vec2(Input.mouseX,Input.mouseY);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            mousePos2 = new Vec2(Input.mouseX, Input.mouseY);
            AddLine(mousePos1, mousePos2);
            Console.WriteLine("AddLine(new Vec2 " + mousePos1 + ",new Vec2 " + mousePos2 + ");");
        }
    }

    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            //Console.WriteLine(child);
            child.Destroy();
        }
        _lines = new List<NLineSegment>();
        foreach (Ball mover in _movers)
        {
            mover.Destroy();
        }
        _movers = new List<Ball>();
    }

    public void LoadLevel(int index)
    {
        //destroy everything and load a level based on the index
        DestroyAll();
        levelIndex = index;
        if (index == 0)
        {
            StartScreen startscreen = new StartScreen();
            AddChild(startscreen);
            score = 10500;
        }
        if(index == 1) 
        {
            LevelSelect levelselect = new LevelSelect();
            AddChild(levelselect);
            score = 10500;
        }
        if(index == 2) 
        {
            hole1 = new Hole(new Vec2(789, 323));
            AddChild(hole1);

            

            

            PolyShape shape = new PolyShape(new Vec2(0, 0), new List<Vec2>
            {
            new Vec2( 648, 905),
            new Vec2( 676, 216),
            new Vec2( 904, 216),
            new Vec2( 899, 906),
            new Vec2( 648, 905),
            }, null, false);
            AddChild(shape);
            shape = new PolyShape(new Vec2(0, 0), new List<Vec2>
            {
            new Vec2( 738, 538),
            new Vec2( 721, 550),
            new Vec2( 736, 566),
            new Vec2( 808, 567),
            new Vec2( 822, 552),
            new Vec2( 808, 536),
            new Vec2( 738, 538)
            }, null, false);
            AddChild(shape);

            Sprite background = new Sprite("FuguhlutooganLevel1Final.png");
            AddChild(background);

            shape = new PolyShape(new Vec2(100, 100), new List<Vec2>
            {
                new Vec2 (100,0),
                new Vec2 (0,0),
                
                new Vec2 (50,50),
                new Vec2 (100,0),

            }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(100, 200), new List<Vec2>
            {
                new Vec2 (100,0),
                new Vec2 (0,0),

                new Vec2 (50,50),
                new Vec2 (100,0),

        }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(100, 300), new List<Vec2>
            {
                new Vec2 (100,0),
                new Vec2 (0,0),

                new Vec2 (50,50),
                new Vec2 (100,0),

        }, null);
            AddChild(shape);


            MenuButton menubutton = new MenuButton(new Vec2(1300, 100));
            AddChild(menubutton);

            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            ResetButton resetbutton = new ResetButton(new Vec2(1000, 600));
            AddChild(resetbutton);
        }
        if(index == 3)
        {
            Level1Complete = true;
            Endscreen endscreen = new Endscreen();
            AddChild(endscreen);

            RetryButton retryButton = new RetryButton(new Vec2(200, 200));
            AddChild(retryButton);
            if (score >= 8000)
            {
                endscreen.SetCycle(3, 1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(2, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(1, 1);
            }
            if (score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(0, 1);
            }
            score = 10500;
        }
        if (index == 4)
        {
            IngameMenu ingamemenu = new IngameMenu();
            AddChild (ingamemenu);
        }
        if (index == 5)
        {
            Sprite background = new Sprite("BackdropJittleyangVsFuguhlutoogan.png");
            AddChild(background);
            _text = new EasyDraw(250, 40);
            _text.TextAlign(CenterMode.Min, CenterMode.Min);
            AddChild(_text);

            //PolyShape shape = new PolyShape(new Vec2(0,0), new List<Vec2> {
            //    new Vec2(178, 69),
            //    new Vec2(871, 55),
            //    new Vec2(1137, 302),
            //    new Vec2(917, 463),
            //    new Vec2(767, 302),
            //    new Vec2(578, 320),
            //    new Vec2(585, 673),
            //    new Vec2(223, 659),
            //    new Vec2(178, 69)},null,false);
            //AddChild(shape);



            PolyShape shape = new PolyShape(new Vec2(0, 0), new List<Vec2>
            {
                new Vec2( 509, 205),
                new Vec2( 1049, 206),
                new Vec2( 1193, 383),
                new Vec2( 1021, 498),
                new Vec2( 936, 383),
                new Vec2( 712, 382),
                
                new Vec2( 690, 900),
                new Vec2( 464, 903),
                new Vec2( 509, 205)
            }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(0, 100),
                new List<Vec2> {
                new Vec2(10,20),
                new Vec2 (10,100),
                new Vec2 (30,100),
                new Vec2 (30,20),
                new Vec2(10,20)
                },
                new List<Ball>{
                    new Ball(20,new Vec2(20,20), Vec2.Zero(), false),
                    new Ball(20,new Vec2(20,100), Vec2.Zero(), false)
                }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(100, 600), new List<Vec2> {
            new Vec2(0,0),
            new Vec2(0,50),
            new Vec2(50,50),
            new Vec2(50,0),
            new Vec2(0,0),
            }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(700, 600), new List<Vec2> {
            new Vec2(0,0),
            new Vec2(0,100),
            new Vec2(25,100),
            new Vec2(25,0),
            new Vec2(0,0),
            }, null, false);
            shape.Rotate(90);
            shape.SetPos(new Vec2(480,640));
            AddChild(shape);

            shape = new PolyShape(new Vec2(50, 200), new List<Vec2> {
            new Vec2(50,0),
            new Vec2(0,75),
            new Vec2(50,150),
            new Vec2(100,75),
            new Vec2(50,0),
            }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(400, 900), new List<Vec2> {
            new Vec2(10,0),
            new Vec2(0,10),
            new Vec2(90,100),
            new Vec2(180,10),
            new Vec2(170,0),
            new Vec2(90,50),
            new Vec2(10,0),
            }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(200, 800), 30, true);
            AddChild(shape);

            shape = new PolyShape(new Vec2(822, 300), 50, false);
            AddChild(shape);

            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            ResetButton resetbutton = new ResetButton(new Vec2(800,600));
            AddChild(resetbutton);

            hole2 = new Hole(new Vec2(1073, 403));
            AddChild(hole2);

        }
        if (index == 6)
        {
            Level2Complete = true;
            Endscreen endscreen = new Endscreen();
            AddChild(endscreen);

            RetryButton retryButton = new RetryButton(new Vec2(200, 200));
            AddChild(retryButton);
            if (score >= 8000)
            {
                endscreen.SetCycle(3,1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(2, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(1, 1);
            }
            if(score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(0, 1);
            }
            score = 10500;
        }
        if(index == 7)
        {
            IngameMenu ingamemenu = new IngameMenu();
            AddChild(ingamemenu);
        }
        if (index == 8)
        {
            PolyShape shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2 (792,154),
            new Vec2 (1081,156),
            new Vec2(1071, 957),
            new Vec2(442, 954),
        }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2 (431,668),
            new Vec2 (757,687),

        }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2>
            {
            new Vec2 (773,404),
            new Vec2 (448,386),
            new Vec2(432, 667),
            new Vec2(187, 666),
            new Vec2(177, 150),
            new Vec2(456, 152),

        }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(50, 300), new List<Vec2> {
                new Vec2(0,0),
                new Vec2(0,200),
                new Vec2(100,200),
                new Vec2(100,0),
                new Vec2(0,0)},
                new Sprite("square.png"));
            AddChild(shape);

            shape = new PolyShape(new Vec2(50, 600), new List<Vec2> {
                new Vec2(0,0),
                new Vec2(0,200),
                new Vec2(100,200),
                new Vec2(100,0),
                new Vec2(0,0)},
                new Sprite("square.png"));
            AddChild(shape);

            shape = new PolyShape(new Vec2(50, 100), new List<Vec2>
            {
                new Vec2 (100,0),
                new Vec2 (0,0),

                new Vec2 (50,50),
                new Vec2 (100,0),

        }, null);
            AddChild(shape);

            shape = new PolyShape(new Vec2(50, 200), new List<Vec2>
            {
                new Vec2 (100,0),
                new Vec2 (0,0),

                new Vec2 (50,50),
                new Vec2 (100,0),

        }, null);
            AddChild(shape);

            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            ResetButton resetbutton = new ResetButton(new Vec2(1200, 600));
            AddChild(resetbutton);

            hole2 = new Hole(new Vec2(320, 550));
            AddChild(hole2);
        }


        if (index == 9)
        {
            
            Endscreen endscreen = new Endscreen();
            AddChild(endscreen);

            RetryButton retryButton = new RetryButton(new Vec2(200, 200));
            AddChild(retryButton);
            if (score >= 8000)
            {
                endscreen.SetCycle(3, 1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(2, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(1, 1);
            }
            if (score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(0, 1);
            }
            score = 10500;
        }
        if(index == 10)
        {
            IngameMenu ingamemenu = new IngameMenu();
            AddChild(ingamemenu);
        }
     }
}

