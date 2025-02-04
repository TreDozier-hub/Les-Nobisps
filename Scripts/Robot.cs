

using Godot;
using System;

public partial class Robot : CharacterBody2D
{
	[Export] public TextureProgressBar vie = null;
	[Export] private PackedScene FiletScene; 
	[Export] private AnimatedSprite2D animatedSprite2D;
	private Timer filetTimer;  
	//private TextureProgressBar vie;

	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
	public int Vie = 30;

	public override void _Ready()
	{
		vie = GetNode<TextureProgressBar>("TextureProgressBar");
		vie.Value = Vie;
		
		
		// Vérifie si le filet est bien assignée
		if (FiletScene == null)
		{
			GD.PrintErr("⚠️ ERREUR");
		}
		
		// Création du Timer pour cacher le filet
		filetTimer = new Timer();
		filetTimer.WaitTime = 1.5f;
		filetTimer.OneShot = true;
		AddChild(filetTimer);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Ajouter la gravité
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Gestion du saut
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
		{			
			velocity.Y = JumpVelocity;
		}

		// Lancer le filet en appuyant sur "Shift"
		if (Input.IsActionJustPressed("filet"))
		{
			LancerFilet();
		}

		// Gestion du mouvement
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			animatedSprite2D.Play("marche_droite");
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		if (Input.IsActionJustPressed("ui_left"))
		{
			
			animatedSprite2D.Play("marche_gauche");
		}
		
		
		Velocity = velocity;
		MoveAndSlide();
	}

	private void LancerFilet()
	{
		if (FiletScene == null)
		{
			GD.PrintErr("❌ Impossible d'instancier le filet, FiletScene est NULL !");
			return;
		}

		GD.Print("🎯 Filet lancé !");

		// Crée une instance du filet
		Area2D filetInstance = (Area2D)FiletScene.Instantiate();
		
		// Positionne le filet devant le Robot
		filetInstance.GlobalPosition = GlobalPosition + new Vector2(50, 0);  

		// Ajoute le filet à la scène
		GetParent().AddChild(filetInstance);

		// Détruit le filet après un certain temps
		filetTimer.Timeout += () => { if (IsInstanceValid(filetInstance)) filetInstance.QueueFree(); };
		filetTimer.Start();
		}
		public void FaireDomage(int domage)
	{
		Vie -= domage;
		GD.Print("💥 Robot Vie: " + Vie);
				vie.Value = Vie;

		if (Vie <= 0)
		{
			Mort();
		}
	}

	private void Mort()
	{        
		QueueFree(); // Supprime le robot de la scène
		//Is dead 
	}
	}
