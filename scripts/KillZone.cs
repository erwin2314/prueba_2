using Godot;
using System;
using System.Diagnostics;

public partial class KillZone : Area2D
{
    public Timer timer;
    public Node2D jugador;
    public Camera2D camera2D;
    public bool jugadorMuerto = false;
    public override void _Ready()
    {
        
        BodyEntered += JugadorEntroELArea;
        timer = GetNode<Timer>("Timer");
        timer.Timeout += RegresarAUnPunto;
    }

    public void JugadorEntroELArea(Node2D body)
    {
        Engine.TimeScale = 0.3;
        timer.Start();
        jugador = body;
        camera2D = jugador.GetNode<Camera2D>("Camera2D");
        jugadorMuerto = true;

        if (body is Player player)
        {
            player.ReproducirSFXHurt();
        }
    }
    public void RegresarAUnPunto()
    {
        jugadorMuerto = false;
        Engine.TimeScale = 1;
        jugador.Position = new Vector2(-186, 41);
        camera2D.Zoom = new Vector2(4, 4);
        jugador.Rotation = 0;
    }

    public override void _Process(double delta)
    {
        if (jugadorMuerto)
        {
            JugadorMuere(jugador);
        }
    }

    public void JugadorMuere(Node2D body)
    {
        jugador = body;
        camera2D.Zoom = new Vector2(10, 10);
        jugador.Rotation += (float)0.05;
    }
}
