using Godot;
using System;

public partial class LevelOne : Node2D
{
	private TextureProgressBar vieBar;

	public override void _Ready()
	{
		//GD.Print("Recherche du nœud Robot...");
		
		Robot robot = GetNode<Robot>("SceneMap/CanvasLayer/Robot");
		if (robot != null)
		{
			//GD.Print("Robot trouvé !");
		}
		else
		{
			GD.PrintErr("❌ ERREUR ");
		}

		vieBar = GetNode<TextureProgressBar>("HUB/CanvasLayer/TextureProgressBar");
		if (vieBar != null)
		{
			GD.Print("TextureProgressBar !");
		}
		else
		{
			GD.PrintErr("❌ ERREUR ");
		}

		if (robot != null && vieBar != null)
		{
			robot.vie = vieBar; 
			vieBar.MaxValue = robot.Vie; 
			vieBar.Value = robot.Vie;
			//GD.Print("Barre de vie : ", vieBar.Value); 
		}
		else
		{
			GD.PrintErr("❌ ERREUR");
		}
	}
}
