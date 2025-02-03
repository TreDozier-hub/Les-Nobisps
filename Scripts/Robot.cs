//using Godot;
//using System;
//
//public partial class Robot : CharacterBody2D
//{
	//public const float Speed = 300.0f;
	//public const float JumpVelocity = -400.0f;
//
	//public override void _PhysicsProcess(double delta)
	//{
		//Vector2 velocity = Velocity;
//
		//// Add the gravity.
		//if (!IsOnFloor())
		//{
			//velocity += GetGravity() * (float)delta;
		//}
//
		//// Handle Jump.
		//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		//{
			//velocity.Y = JumpVelocity;
		//}
//
		//// Get the input direction and handle the movement/deceleration.
		//// As good practice, you should replace UI actions with custom gameplay actions.
		//Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//if (direction != Vector2.Zero)
		//{
			//velocity.X = direction.X * Speed;
		//}
		//else
		//{
			//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		//}
//
		//Velocity = velocity;
		//MoveAndSlide();
	//}
//}


//----------------------------------------------------


//using Godot;
//using System;
//
//public partial class Robot : CharacterBody2D
//{
	//[Export] private AnimatedSprite2D animatedSprite2D;
	//[Export] private AnimatedSprite2D filet; 
	//private Timer filetTimer;  // Timer pour cacher le filet après un certain temps
//
	//public const float Speed = 300.0f;
	//public const float JumpVelocity = -900.0f;
//
	//public int Vie = 30;
//
	//public override void _Ready()
	//{
		//filet = GetNode<AnimatedSprite2D>("filet");
//
		//if (filet == null)
//{
	//GD.PrintErr("⚠️ ERREUR : Impossible de trouver 'filet'. Vérifie son chemin !");
//}
//else
//{
	//GD.Print("✅ Filet correctement assigné :", filet.Name);
//}
//
//
		//GD.Print("✅ Filet trouvé :", filet != null);
		//if (filet != null)
		//{
			//filet.Visible = false;  // Cache le filet au départ
			//filet.Stop();
		//}
		//else
		//{
			//GD.PrintErr("⚠️ ERREUR : Le filet n'est pas assigné !");
		//}
//
		//// Timer pour cacher le filet après un certain temps
		//filetTimer = new Timer();
		//filetTimer.WaitTime = 1.5f;
		//filetTimer.OneShot = true;
		//filetTimer.Timeout += () => CacherFilet();
		//AddChild(filetTimer);
	//}
//
	//public override void _PhysicsProcess(double delta)
	//{
		//Vector2 velocity = Velocity;
//
		//// Ajouter la gravité
		//if (!IsOnFloor())
		//{
			//velocity += GetGravity() * (float)delta;
		//}
//
		//// Gestion du saut
		//if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
		//{
			//velocity.Y = JumpVelocity;
		//}
//
		//// Gestion du filet (appui sur "Shift")
		//if (Input.IsActionJustPressed("filet"))
		//{
			//LancerFilet();
		//}
//
		//// Gestion du mouvement
		//Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//if (direction != Vector2.Zero)
		//{
			//velocity.X = direction.X * Speed;
		//}
		//else
		//{
			//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		//}
//
		//// Animation
		//GérerAnimation(direction);
//
		//Velocity = velocity;
		//MoveAndSlide();
	//}
//
	//private void LancerFilet()
	//{
		//GD.Print("👀 Filet visible ?", filet.Visible);
//GD.Print("🎬 Animation actuelle :", filet.Animation);
//GD.Print("🕐 Frame actuelle :", filet.Frame);
//
		//GD.Print("🎯 Filet activé !");
//
		//if (filet != null)
		//{
			//filet.Visible = true;  // Affiche le filet
			//filet.Frame = 0;  
			//filet.Play("LancerFilet"); // Joue l'animation
			//
			//filetTimer.Start(); //  Timer pour cacher le filet
		//}
	//}
//
	//private void CacherFilet()
	//{
		//GD.Print("🛑 Filet caché !");
		//if (filet != null)
		//{
			//filet.Visible = false;
			//filet.Stop();
		//}
	//}
//
	//private void GérerAnimation(Vector2 direction)
	//{
		//if (!IsOnFloor())
		//{
			//animatedSprite2D.Animation = "Saut";
		//}
		//else
		//{
			//if (direction.X != 0)
			//{
				//animatedSprite2D.Animation = (direction.X > 0) ? "marche_droite" : "marche_gauche";
				//animatedSprite2D.FlipH = direction.X < 0;
			//}
			//else
			//{
				//animatedSprite2D.Animation = "Repos";
			//}
		//}
	//}
//
	//public void FaireDomage(int domage)
	//{
		//Vie -= domage;
		//GD.Print("💥 Robot Vie: " + Vie);
//
		//if (Vie <= 0)
		//{
			//Mort();
		//}
	//}
//
	//private void Mort()
	//{        
		//QueueFree(); // Supprime le robot de la scène
	//}
//}
//________________________________________________________

using Godot;
using System;

public partial class Robot : CharacterBody2D
{
	[Export] private PackedScene FiletScene;  // Ajoute cette ligne pour exporter la scène du filet
	[Export] private AnimatedSprite2D animatedSprite2D;
	private Timer filetTimer;  

	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
	public int Vie = 30;

	public override void _Ready()
	{
		// Vérifie si la scène du filet est bien assignée
		if (FiletScene == null)
		{
			GD.PrintErr("⚠️ ERREUR : FiletScene non assigné dans l'inspecteur !");
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
