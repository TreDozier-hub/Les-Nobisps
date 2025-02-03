using Godot;
using System;

public partial class EtoilesToutes : Sprite2D
{
	private Vector2 fixedPosition; // Stocke la position initiale

	public override void _Ready()
	{
		ZIndex = -1; // Place l'objet derrière les autres
		fixedPosition = GlobalPosition; // Sauvegarde la position de départ
		Modulate = new Color(1, 1, 1, 0.5f); // Rend l'opacité à 50%
	}

	public override void _Process(double delta)
	{
		GlobalPosition = fixedPosition; // Garde la position fixe
	}
}
