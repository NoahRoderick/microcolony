using Godot;

public partial class UIHUD : Control
{
    [Export] public ProgressBar O2Bar = default!;
    [Export] public ProgressBar H2OBar = default!;
    [Export] public ProgressBar PowerBar = default!;
    [Export] public Label TimerLabel = default!;
    [Export] public Button RestartButton = default!;
    [Export] public Button SolarButton = default!;
    [Export] public Button RecyclerButton = default!;
    [Export] public Button HydroponicsButton = default!;

    public void SetMeters(float o2, float h2o, float power)
    {
        O2Bar.Value = o2;
        H2OBar.Value = h2o;
        PowerBar.Value = power;
    }

    public void SetTimer(float seconds)
    {
        TimerLabel.Text = Mathf.CeilToInt(seconds).ToString();
    }

    public void ShowEnd(string message)
    {
        TimerLabel.Text = message;
        RestartButton.Visible = true;
    }
}
