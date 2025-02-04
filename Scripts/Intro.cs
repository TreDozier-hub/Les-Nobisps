using Godot;

public partial class Intro : Node
{
	private RichTextLabel label;
	public override void _Ready()
	{
		
		
		label = GetNodeOrNull<RichTextLabel>("RichTextLabel");

		//if (label == null)
		//{
			//GD.PrintErr("❌ ERREUR ");
		//}
		//else
		//{
			//GD.Print("trouvé !");
		//}
		
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer != null)
		{
			  // Joue l'animation
			animationPlayer.Play("scroll_text");
			animationPlayer.AnimationFinished += OnAnimationFinished;
		}
		else
		{
			GD.PrintErr("❌ ERREUR!");
		}
	}

	private void OnAnimationFinished(StringName animName)
	{
		if (animName == "scroll_text") 
		{
			GetTree().ChangeSceneToFile("res://Scene_Jeux/level_one.tscn");
		}
	}
}
