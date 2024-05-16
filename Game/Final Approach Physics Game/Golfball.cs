using GXPEngine;
using System.Collections.Generic;


public class Golfball : Ball
{
    public float animationSpeed = .2f;
    public AnimationSprite sprite;
    Hole hole;

    Endscreen endscreen;
    public Golfball(Vec2 pos) : base(10, pos)
    {
        sprite = new AnimationSprite("GolfballFinal.png", 18, 1, -1, false, false);
        sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
        
        AddChild(sprite);
       

    }
    public void Update()
    {
        sprite.Animate(animationSpeed * velocity.Length());
        CheckDistToHole();
        RotateBall();
        velocity *= .99f;

        //if (endscreen != null)
        //{
        //    if (myGame.score >= 8000)
        //    {
        //        endscreen.SetCycle(3, 1);
        //    }
        //    if (myGame.score >= 5000 && myGame.score <= 7999)
        //    {
        //        endscreen.SetCycle(2, 1);
        //    }
        //    if (myGame.score >= 2000 && myGame.score <= 4999)
        //    {
        //        endscreen.SetCycle(1, 1);
        //    }
        //    if (myGame.score >= 0 && myGame.score <= 1999)
        //    {
        //        endscreen.SetCycle(0, 1);
        //    }
        //    myGame.score = 10500;
        //}

        if (Input.GetKeyDown(Key.L))
        {
            LoadEndscreen();
        }
    }

    void CheckDistToHole()
    {
        hole = game.FindObjectOfType<Hole>();
        if (hole != null)
        {
            Vec2 distanceHole = new Vec2(hole.x, hole.y) - position;
            if (distanceHole.Length() < radius + 3)
            {
                //myGame.LoadLevel(myGame.levelIndex + 1);
                if (myGame.levelIndex == 2) { myGame.Level1Complete = true; }
                if (myGame.levelIndex == 5) { myGame.Level2Complete = true; }

                myGame.Level1Complete = true;

                LoadEndscreen();
                
                this.Destroy();
            }
        }
    }

    void LoadEndscreen()
    {
        endscreen = new Endscreen();
        if (endscreen != null)
        {
            if (myGame.score >= 8000)
            {
                endscreen.SetCycle(0, 1);
            }
            if (myGame.score >= 5000 && myGame.score <= 7999)
            {
                endscreen.SetCycle(1, 1);
            }
            if (myGame.score >= 2000 && myGame.score <= 4999)
            {
                endscreen.SetCycle(2, 1);
            }
            if (myGame.score >= 0 && myGame.score <= 1999)
            {
                endscreen.SetCycle(3, 1);
            }
            myGame.score = 10500;
        }
        game.AddChild(endscreen);
    }

    void RotateBall()
    {
        sprite.rotation = velocity.GetAngleDegrees();
    }
}

