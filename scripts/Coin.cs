using Godot;
using System;
using System.Diagnostics;

public partial class Coin : Area2D
{
	public AudioStreamPlayer2D audioStreamPlayer;
	public bool recogida = false;
	public override void _Ready()
	{
		audioStreamPlayer = GetNode<AudioStreamPlayer2D>("SFX");
		BodyEntered += _on_body_entered;
	}

	public void _on_body_entered(Node2D body)
	{

		if (body is Player jugador && recogida == false)
		{
			audioStreamPlayer.Play();
			jugador.AumentarPuntuacion();
			recogida = true;
			Visible = false;
		}
		if (!audioStreamPlayer.Playing)
		{
			QueueFree();
		}
		
	}

}
