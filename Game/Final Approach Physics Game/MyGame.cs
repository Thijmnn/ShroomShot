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
    public List<PolyShape> _polyShapes;

    Vec2 mousePos1;
    Vec2 mousePos2;

    public int levelIndex;

    public Hole hole1;
    public Hole hole2;
    public Hole hole3;

    Sprite PopUPMSg;
    Sprite RtoRotateMsg;
    AnimationSprite scoreText;

    public  int score;

    public bool Level1Complete;
    public bool Level2Complete;

    
    Sound MainTheme;

    public MyGame () : base(/*1200, 800*/1920,1080, false,false)
	{

        score = 10500;
        scoreText = new AnimationSprite("ScoreText.png", 4, 6);

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
         
            if (MainTheme == null)
            { 
                MainTheme = new Sound("LevelMusic.wav");
                MainTheme.Play();
            }


        if (levelIndex == 2 || levelIndex == 5 || levelIndex == 8)
        {
            if (Input.GetKeyDown(Key.SPACE))
            {
                PopUPMSg.alpha = 0;
            }
            if (Input.GetKeyDown(Key.R))
            {
                RtoRotateMsg.alpha = 0;
            }
        }
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
        ScoreHandler();

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
        if (Input.GetKeyDown(Key.Z))
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
       /* if (Input.GetMouseButtonUp(1))
        {
            mousePos2 = new Vec2(Input.mouseX, Input.mouseY);
            AddLine(mousePos1, mousePos2);
            Console.WriteLine("AddLine(new Vec2 " + mousePos1 + ",new Vec2 " + mousePos2 + ");");
        }*/
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
        _polyShapes = new List<PolyShape>();
    }
    public void ScoreHandler()
    {
        if (scoreText == null)
        {
            scoreText = new AnimationSprite("ScoreText.png", 4, 6);
        }
        switch (score)
        {
            case 10500:
                scoreText.SetCycle(1, 1);
                break;
            case 10000:
                scoreText.SetCycle(1, 1);
                break;
            case 9500:
                scoreText.SetCycle(2, 1);
                break;
            case 9000:
                scoreText.SetCycle(3, 1);
                break;
            case 8500:
                scoreText.SetCycle(4, 1);
                break;
            case 8000:
                scoreText.SetCycle(5, 1);
                break;
            case 7500:
                scoreText.SetCycle(6, 1);
                break;
            case 7000:
                scoreText.SetCycle(7, 1);
                break;
            case 6500:
                scoreText.SetCycle(8, 1);
                break;
            case 6000:
                scoreText.SetCycle(9, 1);
                break;
            case 5500:
                scoreText.SetCycle(10, 1);
                break;
            case 5000:
                scoreText.SetCycle(11, 1);
                break;
            case 4500:
                scoreText.SetCycle(12, 1);
                break;
            case 4000:
                scoreText.SetCycle(13, 1);
                break;
            case 3500:
                scoreText.SetCycle(14, 1);
                break;
            case 3000:
                scoreText.SetCycle(15, 1);
                break;
            case 2500:
                scoreText.SetCycle(16, 1);
                break;
            case 2000:
                scoreText.SetCycle(17, 1);
                break;
            case 1500:
                scoreText.SetCycle(18, 1);
                break;
            case 1000:
                scoreText.SetCycle(19, 1);
                break;
            case 500:
                scoreText.SetCycle(20, 1);
                break;
            case 0:
                scoreText.SetCycle(21, 1);
                break;
        }
        scoreText.Animate(0.5f);

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

            Sprite background = new Sprite("Level1Final.png");
            AddChild(background);

            
            Sprite ScoreElement = new Sprite("ScoreBar.png");
            AddChild(ScoreElement);
            ScoreElement.SetXY(game.width/2 - ScoreElement.width + 50,0);

            MenuButton menubutton = new MenuButton(new Vec2(5, 5));
            AddChild(menubutton);

            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            

            AnimationSprite backgroundUI = new AnimationSprite("UIFinal.png", 1, 3);
            AddChild(backgroundUI);
            backgroundUI.SetCycle(0, 1);

            PopUPMSg = new Sprite("ShootMessage.png");
            AddChild(PopUPMSg);
            PopUPMSg.SetXY(442, 940);

            RtoRotateMsg = new Sprite("pressR.png");
            AddChild(RtoRotateMsg);
            RtoRotateMsg.SetXY(0, 900);

            PolyShape cross = new PolyShape(new Vec2(1745, 93), new List<Vec2>
            {
                //new Vec2(40,0),
                //new Vec2(40,40),
                //new Vec2(0,40),
                //new Vec2(0,95),
                //new Vec2(40,95),
                //new Vec2(40,135),

                //new Vec2(95,135),
                //new Vec2(95,95),
                //new Vec2(135,95),
                //new Vec2(135,40),
                //new Vec2(95,40),
                //new Vec2(95,0),
                //new Vec2(40,0)

                new Vec2( 5, 28),
                new Vec2( 5, 54),
                new Vec2( 28, 54),
                new Vec2( 28, 73),
                new Vec2( 57, 73),
                new Vec2( 57, 54),
                new Vec2( 78, 54),
                new Vec2( 78, 27),
                new Vec2( 57, 27),
                new Vec2( 57, 6),
                new Vec2( 28, 6),
                new Vec2( 28, 28),
                new Vec2( 5, 28)

            }, new Sprite("1_-_3.png"));
            AddChild(cross);
            _polyShapes.Add(cross);

            PolyShape triangle = new PolyShape(new Vec2(1560, 320), new List<Vec2>
            {

                new Vec2( 4, 10),
                new Vec2( 52, 68),
                new Vec2( 97, 10),
                new Vec2( 4, 10)

        }, new Sprite("1_-_2.png"));
            triangle.Rotate(180);
            triangle.SetPos(new Vec2(1600, 440));
            AddChild(triangle);
            _polyShapes.Add(triangle);

            PolyShape trapezium = new PolyShape(new Vec2(1718, 797), new List<Vec2>
            {
                new Vec2( 7, 5),
                new Vec2( 25, 50),
                new Vec2( 116, 50),
                new Vec2( 98, 5),
                new Vec2(7,5)
                

        }, new Sprite("1_-_1.png")) ;

            AddChild(trapezium);
            _polyShapes.Add(trapezium);

            scoreText = new AnimationSprite("ScoreText.png", 4, 6);
            AddChild(scoreText);
            scoreText.SetXY(568, 1);
            scoreText.width = scoreText.width - 50;
            scoreText.height = scoreText.height - 50;

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
                endscreen.SetCycle(2, 1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(3, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(0, 1);
            }
            if (score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(1, 1);
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
            
            _text = new EasyDraw(250, 40);
            _text.TextAlign(CenterMode.Min, CenterMode.Min);
            AddChild(_text);

            



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

            PolyShape border = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2( 373, 587),
            new Vec2( 406, 614),
            new Vec2( 538, 622),
            new Vec2( 574, 597),
            new Vec2( 541, 570),
            new Vec2( 414, 563),
            new Vec2( 375, 586),
            }, null, false);

            AddChild(border);

            shape = new PolyShape(new Vec2(880, 282), 50, false);
            AddChild(shape);

            hole2 = new Hole(new Vec2(1073, 403));
            AddChild(hole2);

            Sprite background = new Sprite("Level2Final.png");
            AddChild(background);

            MenuButton menubutton = new MenuButton(new Vec2(5, 5));
            AddChild(menubutton);

            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            

            AnimationSprite backgroundUI = new AnimationSprite("UIFinal.png", 1, 3);
            AddChild(backgroundUI);
            backgroundUI.SetCycle(1, 1);

            PolyShape circle = new PolyShape(new Vec2(1818, 153), 34, true, new Sprite("2_-_4.png"));
            AddChild(circle);
            _polyShapes.Add(circle);

            PolyShape square = new PolyShape(new Vec2(1770, 545), new List<Vec2> {
            new Vec2( 5, 3),
            new Vec2( 3, 58),
            new Vec2( 62, 57),
            new Vec2( 64, 3),
            new Vec2( 5, 3)
            }, new Sprite("2_-_2.png"));
            AddChild(square);
            _polyShapes.Add(square);



            PolyShape rhomboid = new PolyShape(new Vec2(1582, 345), new List<Vec2> {
            new Vec2( 31, 5),
            new Vec2( 6, 46),
            new Vec2( 26, 89),
            new Vec2( 52, 47),
            new Vec2( 31, 5)
            }, new Sprite("2_-_3.png"));
            AddChild(rhomboid);
            _polyShapes.Add(rhomboid);

            PolyShape angledPlank = new PolyShape(new Vec2(1610, 792), new List<Vec2> {
            new Vec2( 10, 48),
            new Vec2( 8, 68),
            new Vec2( 65, 69),
            new Vec2( 116, 20),
            new Vec2( 99, 7),
            new Vec2( 62, 48),
            new Vec2( 11, 48)
            }, new Sprite("2_-_1.png"));
            angledPlank.Rotate(45);
            AddChild(angledPlank);
            _polyShapes.Add(angledPlank);


            PopUPMSg = new Sprite("ShootMessage.png");
            AddChild(PopUPMSg);
            PopUPMSg.SetXY(272, 930);

            RtoRotateMsg = new Sprite("pressR.png");
            AddChild(RtoRotateMsg);
            RtoRotateMsg.SetXY(888, 900);

            Sprite ScoreElement = new Sprite("ScoreBar.png");
            AddChild(ScoreElement);
            ScoreElement.SetXY(game.width / 2 - ScoreElement.width + 50, 0);

            scoreText = new AnimationSprite("ScoreText.png", 4, 6);
            AddChild(scoreText);
            scoreText.SetXY(568, 1);
            scoreText.width = scoreText.width - 50;
            scoreText.height = scoreText.height - 50;
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
                endscreen.SetCycle(2, 1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(3, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(0, 1);
            }
            if (score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(1, 1);
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

            hole2 = new Hole(new Vec2(517, 467));
            AddChild(hole2);

            PolyShape shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2(396, 343),
            new Vec2(426, 342),
            new Vec2(437, 212),
            new Vec2(611, 212),
            new Vec2(616, 195)
            }, null, false);
            AddChild(shape);

            

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2(989, 195),
            new Vec2(991, 211),
            new Vec2(1167, 214),
            new Vec2(1182, 742),
            new Vec2(358, 744)
        }, null, false);
            AddChild(shape);


            shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2(991, 196),
            new Vec2(613, 196)
        }, null, false);
            AddChild(shape);
            

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2( 878, 508),
            new Vec2( 686, 509),
            new Vec2( 688, 430),
            new Vec2( 881, 429),
            new Vec2( 881, 393),
            new Vec2( 662, 392),
            new Vec2( 655, 509),
            new Vec2( 380, 510),
            new Vec2( 381, 547),
            new Vec2( 881, 544),
            new Vec2( 879, 508)
        }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2> {
            new Vec2( 394, 343),
            new Vec2( 359, 745)

        }, null, false);
            AddChild(shape);

            shape = new PolyShape(new Vec2(0, 0), new List<Vec2>
            {
            new Vec2( 881, 430),
            new Vec2( 878, 508)

        }, null, false);
            AddChild(shape);

            Sprite background = new Sprite("Level3Final.png");
            AddChild(background);


            AnimationSprite backgroundUI = new AnimationSprite("UIFinal.png", 1, 3);
            AddChild(backgroundUI);
            backgroundUI.SetCycle(2, 1);

            
            PolyShape spekjeshape = new PolyShape(new Vec2(1590, 840), new List<Vec2> {
            new Vec2( 31, 5),
            new Vec2( 6, 46),
            new Vec2( 26, 89),
            new Vec2( 52, 47),
            new Vec2( 31, 5)
            }, new Sprite("2_-_3.png"));
            AddChild(spekjeshape);
            _polyShapes.Add(spekjeshape);


            PolyShape BopItshape = new PolyShape(new Vec2(1665, 65),
                new List<Vec2> {
                new Vec2( 15, 34),
                new Vec2( 15, 57),
                new Vec2( 30, 59),
                new Vec2( 30, 34),
                new Vec2( 15, 34)
                },
                new List<Ball>{
                    new Ball(14,new Vec2( 22, 22), Vec2.Zero(), false),
                    new Ball(14,new Vec2( 21, 70), Vec2.Zero(), false)
                }, new Sprite("3_-_6.png"));
            AddChild(BopItshape);
            _polyShapes.Add(BopItshape);


            PolyShape Squareshape = new PolyShape(new Vec2(1769, 721), new List<Vec2> {
            new Vec2( 5, 3),
            new Vec2( 3, 58),
            new Vec2( 62, 57),
            new Vec2( 64, 3),
            new Vec2( 5, 3)
            }, new Sprite("2_-_2.png"));
            AddChild(Squareshape);
            _polyShapes.Add(Squareshape);


            PolyShape Arrowshape = new PolyShape(new Vec2(1740, 487), new List<Vec2> {
            new Vec2( 17, 8),
            new Vec2( 17, 54),
            new Vec2( 5, 55),
            new Vec2( 33, 90),
            new Vec2( 62, 54),
            new Vec2( 51, 54),
            new Vec2( 50, 8),
            new Vec2( 17, 8)

            }, new Sprite("3_-_5.png"));
            AddChild(Arrowshape);
            _polyShapes.Add(Arrowshape);

            PolyShape Hexashape = new PolyShape(new Vec2(1550, 363), new List<Vec2> {

            new Vec2( 3, 20),
            new Vec2( 2, 49),
            new Vec2( 21, 67),
            new Vec2( 53, 67),
            new Vec2( 72, 48),
            new Vec2( 72, 21),
            new Vec2( 53, 3),
            new Vec2( 21, 3),
            new Vec2( 3, 20)

            }, new Sprite("3_-_4.png")) ;
            AddChild(Hexashape);
            _polyShapes.Add(Hexashape);


            GameState gamestate = new GameState(10, new Vec2(20, 20), false);
            AddChild(gamestate);

            

            PopUPMSg = new Sprite("ShootMessage.png");
            AddChild(PopUPMSg);
            PopUPMSg.SetXY(300, 800);

            RtoRotateMsg = new Sprite("pressR.png");
            AddChild(RtoRotateMsg);
            RtoRotateMsg.SetXY(372, 900);

            Sprite ScoreElement = new Sprite("ScoreBar.png");
            AddChild(ScoreElement);
            ScoreElement.SetXY(game.width / 2 - ScoreElement.width + 50, 0);

            MenuButton menubutton = new MenuButton(new Vec2(5, 5));
            AddChild(menubutton);

            scoreText = new AnimationSprite("ScoreText.png", 4, 6);
            AddChild(scoreText);
            scoreText.SetXY(568, 1);
            scoreText.width = scoreText.width - 50;
            scoreText.height = scoreText.height - 50;
        }


        if (index == 9)
        {
            
            Endscreen endscreen = new Endscreen();
            AddChild(endscreen);

            RetryButton retryButton = new RetryButton(new Vec2(200, 200));
            AddChild(retryButton);
            if (score >= 8000)
            {
                endscreen.SetCycle(0, 1);
            }
            if (score >= 5000 && score <= 7999)
            {
                endscreen.SetCycle(1, 1);
            }
            if (score >= 2000 && score <= 4999)
            {
                endscreen.SetCycle(2, 1);
            }
            if (score >= 0 && score <= 1999)
            {
                endscreen.SetCycle(3, 1);
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

