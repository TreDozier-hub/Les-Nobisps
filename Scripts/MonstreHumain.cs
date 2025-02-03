using Godot;
using System;

public partial class MonstreHumain : CharacterBody2D
{
	[Export] private AnimatedSprite2D animatedSprite2D;
	[Export] public float Speed { get; set; } = 200.0f;

	public const float JumpVelocity = -400.0f;
	[Export] private Area2D _area2D;

	public override void _Ready()
	{
		if (_area2D != null)
		{
			_area2D.BodyEntered += OnArea2DBodyEntered;
		}
		else
		{
			GD.PrintErr("⚠️ _area2D n'est pas assigné dans l'inspecteur !");
		}
	}

	// ✅ Déplacement du monstre
	private void Avancer()
	{
		Velocity = new Vector2(-Speed, 0);
		MoveAndSlide();
		//GD.Print("Monstre se déplace avec vitesse: ", Velocity);

		if (animatedSprite2D != null && animatedSprite2D.Animation != "avancer")
		{
			animatedSprite2D.Play("avancer");
		}
	}
	
	// ✅ Collision avec le robot
	private void OnArea2DBodyEntered(Node body)
	///_on_area_2d_body_entered
	{
		GD.Print("Collision détectée avec : ", body.Name);

		if (body is Robot robot) 
		{
			robot.FaireDomage(10);
			GD.Print("💥 Dégâts infligés au robot : 10 points");
		}
	}

	// ✅ Déplacement continu du monstre
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		Avancer();
		Velocity = velocity;
		MoveAndSlide();
	}
	
	// ✅ Bloquer le monstre avec le filet
	public void Bloquer()
	{
		GD.Print("🕸️ Le monstre est pris au piège !");
		Velocity = Vector2.Zero;
		Speed = 0;
		MoveAndSlide(); // Appliquer la mise à jour

		var timer = GetTree().CreateTimer(2.0);
		timer.Timeout += () => 
		{
			GD.Print("💀 Le monstre disparaît !");
			QueueFree();
		};
	}
	
	//public void Bloquer()
//{
	//GD.Print("🕸️ Le monstre est pris au piège !");
	//Velocity = Vector2.Zero;
	//Speed = 0;
	//MoveAndSlide(); // Forcer la mise à jour immédiate
//}

}
