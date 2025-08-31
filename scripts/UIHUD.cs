using Godot;

public partial class UIHUD : Control
{
    [Export] public ProgressBar O2Bar = default!;
    [Export] public ProgressBar H2OBar = default!;
    [Export] public ProgressBar PowerBar = default!;

    public void SetMeters(float o2, float h2o, float power)
    {
        O2Bar.Value = o2;
        H2OBar.Value = h2o;
        PowerBar.Value = power;
    }
}
