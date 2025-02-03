using Godot;
using System;

public partial class Filet : Area2D
{
	[Export] public float TempsAvantDisparition = 2.0f;
	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); // Assure-toi que ce chemin est bon
		sprite.Play("LancerFilet");

		GD.Print($"🎬 Animation actuelle : {sprite.Animation}");
		GD.Print($"🕐 Frame actuelle : {sprite.Frame}");
		GD.Print($"👀 Filet visible ? {Visible}");

		Connect("body_entered", new Callable(this, nameof(_on_Filet_body_entered)));
	}

	private void _on_Filet_body_entered(Node body)
	{
		GD.Print("🔍 Collision détectée avec : ", body.Name);

		if (body is MonstreHumain monstreHumain)
		{
			GD.Print("🔥 Monstre attrapé !");
			monstreHumain.Bloquer();

			GetTree().CreateTimer(TempsAvantDisparition).Timeout += () =>
			{
				GD.Print("🧹 Filet supprimé !");
				QueueFree();
			};
		}
	}
}
