using Godot;
using System;

public partial class Coin : Area2D
{

	public override void _Ready()
	{
		
		BodyEntered += _on_body_entered;
	}

	public void _on_body_entered(Node2D body)
	{
		QueueFree();
	}

}
