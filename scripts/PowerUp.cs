using Godot;
using System;

public partial class PowerUp : Area2D
{
    [Export]
    public bool unlockDoubleJump = false;
    [Export]
    public bool unlockDash = false;

    public override void _Ready()
    {
        BodyEntered += JugadorEntro;
    }

    public void JugadorEntro(Node2D body)
    {
        if (body is Player jugador)
        {
            if (unlockDoubleJump)
            {
                jugador.DoubleJumpEnabled = true;
            }
            if (unlockDash)
            {
                jugador.DashEnabled = true;
            }
        }
        
    }

}
