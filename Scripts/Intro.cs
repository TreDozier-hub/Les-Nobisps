using Godot;

public partial class Intro : Node
{
	private RichTextLabel label;
	public override void _Ready()
	{
		
		// Assure-toi que le chemin est correct
		label = GetNodeOrNull<RichTextLabel>("RichTextLabel");

		if (label == null)
		{
			GD.PrintErr("❌ ERREUR : RichTextLabel introuvable !");
		}
		else
		{
			GD.Print("✅ RichTextLabel trouvé !");
		}
		
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer != null)
		{
			animationPlayer.Play("scroll_text");  // Joue l'animation
			animationPlayer.AnimationFinished += OnAnimationFinished;  // Détecte la fin de l'animation
		}
		else
		{
			GD.PrintErr("❌ ERREUR : AnimationPlayer non trouvé !");
		}
	}

	private void OnAnimationFinished(StringName animName)
	{
		if (animName == "scroll_text") // Vérifie si c'est bien l'animation du texte
		{
			GetTree().ChangeSceneToFile("res://Scene_Jeux/level_one.tscn");
		}
	}
}
