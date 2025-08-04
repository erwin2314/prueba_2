using Godot;
using System;

public partial class Slime : Node2D
{
    [Export]
    public float speed = 60;
    [Export]
    public float direction = 1; //1 es para la derecha, -1 para la izquierda
    public RayCast2D rayCast2DRight;
    public RayCast2D rayCast2DLeft;
    public AnimatedSprite2D animatedSprite2D;
    public override void _Ready()
    {
        rayCast2DLeft = GetNode<RayCast2D>("RayCastLeft");
        rayCast2DRight = GetNode<RayCast2D>("RayCastRight");
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }


    public override void _Process(double delta)
    {
        if (rayCast2DRight.IsColliding())
        {
            direction = -1;
        }
        else if (rayCast2DLeft.IsColliding())
        {
            direction = 1;
        }

        if (direction == 1)
        {
            animatedSprite2D.FlipH = false;
        }
        else
        {
            animatedSprite2D.FlipH = true;
        }

        Position += new Vector2(direction * speed * (float)delta, 0);
    }

}
