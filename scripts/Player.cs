using Godot;
using System;
using System.Diagnostics;


public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 130.0f;
	[Export]
	public float JumpVelocity = 300.0f;
	[Export]
	public bool CanDoubleJump = false;
	[Export]
	public bool DoubleJumpEnabled = false;
	[Export]
	public bool CanDash = false;
	[Export]
	public bool DashEnabled = false;
	public bool isDashing = false;
	public float dashTime = 500f;
	public float dashTimer = 0f;
	public float dashSpeedMultiplier = 2f;
	public Timer timer;
	public AnimatedSprite2D animatedSprite2D;
	public int puntuacion = 0;
	public Label label;

	public void AumentarPuntuacion()
	{
		puntuacion += 1;
	}

	public override void _Ready()
	{
		label = GetNode<Label>("%Label5");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		timer = GetNode<Timer>("Timer");
		timer.Timeout += RecuperarDash;
	}


	public override void _PhysicsProcess(double delta)
	{
		label.Text = "Puntuacion: " + puntuacion.ToString();
		Vector2 velocity = Velocity;
		

		if (IsOnFloor())
		{
			if (DoubleJumpEnabled)
			{
				CanDoubleJump = true;
			}
			
			if (DashEnabled && CanDash == false && timer.TimeLeft == 0)
			{
				timer.Start();
			}


		}

		if (!IsOnFloor() && !isDashing)
		{
			
			velocity += GetGravity() * (float)delta;
		}


		Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		// Dash
		if (isDashing)
		{
			dashTimer -= (float)delta * 1000f; // dashTime y dashTimer están en ms
			velocity.Y = 0;
			velocity.X = direction.X * (Speed * dashSpeedMultiplier);
			animatedSprite2D.Play("dash");
			if (dashTimer <= 0f)
			{
				isDashing = false;
				timer.Start();
			}

			if (direction.X < 0)
				{
					animatedSprite2D.FlipH = true;
				}
				else
				{
					animatedSprite2D.FlipH = false;
				}
		}
		else
		{
			if (Input.IsActionJustPressed("Dash") && CanDash && direction != Vector2.Zero)
			{
				isDashing = true;
				CanDash = false;
				dashTimer = dashTime;
				animatedSprite2D.Play("dash");
			}
			else
			{
				// Movimiento normal y salto
				if (Input.IsActionJustPressed("Jump") && IsOnFloor())
				{
					animatedSprite2D.Play("jump");
					velocity.Y = -JumpVelocity;
				}
				else if (Input.IsActionJustPressed("Jump") && CanDoubleJump)
				{
					animatedSprite2D.Play("doubleJump");
					velocity.Y = -JumpVelocity;
					CanDoubleJump = false;
				}
				
				if (direction != Vector2.Zero)
				{
					velocity.X = direction.X * Speed;

					if (direction.X < 0)
					{
						animatedSprite2D.FlipH = true;
					}
					else
					{
						animatedSprite2D.FlipH = false;
					}

					if (IsOnFloor())
						animatedSprite2D.Play("run");
				}
				else
				{
					if (IsOnFloor())
						animatedSprite2D.Play("idle");
					velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				}
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void RecuperarDash()
	{
		CanDash = true;
	}
}
