using Godot;

public partial class MainMenu : Control
{
    [Export] public string GameScene = "res://scenes/game.tscn";

    public override void _Ready()
    {
        GetNode<Button>("StartButton").Pressed += OnStartPressed;
    }

    private void OnStartPressed()
    {
        GetTree().ChangeSceneToFile(GameScene);
    }
}
