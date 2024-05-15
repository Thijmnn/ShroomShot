using GXPEngine;
using System.Collections.Generic;


public class Golfball : Ball
{
    public float animationSpeed = .2f;
    AnimationSprite sprite;
    Hole hole;
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
    }

    void CheckDistToHole()
    {
        hole = game.FindObjectOfType<Hole>();
        if (hole != null)
        {
            Vec2 distanceHole = new Vec2(hole.x, hole.y) - position;
            if (distanceHole.Length() < radius + 3)
            {
                myGame.LoadLevel(myGame.levelIndex + 1);
            }
        }
    }

    void RotateBall()
    {
        sprite.rotation = velocity.GetAngleDegrees();
    }
}

