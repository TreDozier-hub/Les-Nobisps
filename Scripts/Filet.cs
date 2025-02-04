using Godot;
using System;

public partial class Filet : Area2D
{
	[Export] public float TempsAvantDisparition = 2.0f;
	private AnimatedSprite2D filet;

	public override void _Ready()
	{		
		filet = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (filet == null)
		{
			GD.PrintErr("❌ ERREUR");
			return;
		}

		filet.Play("LancerFilet");

		//GD.Print($"🎬 Animation actuelle : {filet.Animation}");
		//GD.Print($"🕐 Frame actuelle : {filet.Frame}");
		//GD.Print($"👀 Filet visible ? {Visible}");

		Connect("body_entered", new Callable(this, nameof(_on_Filet_body_entered)));
	}

	private void _on_Filet_body_entered(Node body)
	{
		GD.Print("🔍 Collision détectée avec : ", body.Name);

		if (body is MonstreHumain monstreHumain)
		{
			GD.Print("🔥 Monstre attrapé !");
			monstreHumain.Bloquer();

			var timer = GetTree().CreateTimer(TempsAvantDisparition);
			timer.Connect("timeout", new Callable(this, nameof(_on_Timer_Timeout)));
		}
	}

	private void _on_Timer_Timeout()
	{
		GD.Print("🧹 Filet supprimé !");
		QueueFree();
	}
}
