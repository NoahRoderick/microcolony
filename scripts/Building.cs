using Godot;

public partial class Building : Node
{
	[Export] public bool IsOn { get; private set; } = true;
	[Export] public float PowerCost { get; set; } = 0f;

	public void Toggle()
	{
		IsOn = !IsOn;
	}

	public void SetOn(bool on)
	{
		IsOn = on;
	}
}
