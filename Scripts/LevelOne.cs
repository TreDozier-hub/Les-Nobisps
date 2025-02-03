using Godot;
using System;

public partial class LevelOne : Node2D
{
	private TextureProgressBar vieBar;

	public override void _Ready()
	{
		GD.Print("Recherche du nœud Robot...");
		// Utilise le chemin d'accès complet pour le nœud Robot
		Robot robot = GetNode<Robot>("SceneMap/CanvasLayer/Robot");
		if (robot != null)
		{
			GD.Print("Nœud Robot trouvé !");
		}
		else
		{
			GD.PrintErr("❌ ERREUR : Nœud Robot introuvable !");
		}

		GD.Print("Recherche du nœud TextureProgressBar...");
		// Utilise le chemin d'accès complet pour la barre de vie fixe
		vieBar = GetNode<TextureProgressBar>("HUB/CanvasLayer/TextureProgressBar");
		if (vieBar != null)
		{
			GD.Print("Nœud TextureProgressBar trouvé !");
		}
		else
		{
			GD.PrintErr("❌ ERREUR : Nœud TextureProgressBar introuvable !");
		}

		// Vérifie que les deux éléments existent
		if (robot != null && vieBar != null)
		{
			robot.vie = vieBar; 
			vieBar.MaxValue = robot.Vie; 
			vieBar.Value = robot.Vie;
			GD.Print("Barre de vie initialisée avec la valeur : ", vieBar.Value); // Ligne de débogage
		}
		else
		{
			GD.PrintErr("❌ ERREUR : Robot ou barre de vie introuvable !");
		}
	}
}
