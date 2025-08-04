using Godot;
using System;
using System.Diagnostics;

public partial class Coin : Area2D
{
	public bool recogida = false;
	public override void _Ready()
	{
		
		BodyEntered += _on_body_entered;
	}

	public void _on_body_entered(Node2D body)
	{
		QueueFree();
		if (body is Player jugador && recogida == false)
		{
			jugador.AumentarPuntuacion();
			recogida = true;
		}
		
	}

}
