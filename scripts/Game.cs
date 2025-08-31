using Godot;
using System.Text.Json;

public partial class Game : Node2D
{
    [Export] public NodePath HudPath;
    private UIHUD _hud = default!;
    private Timer _tickTimer = default!;

    private float _o2 = 100f;
    private float _h2o = 100f;
    private float _power = 100f;

    private float _colonistO2;
    private float _colonistH2O;
    private float _solarPower;
    private float _recyclerH2O;
    private float _hydroponicsO2;

    public override void _Ready()
    {
        _hud = GetNode<UIHUD>(HudPath);
        _tickTimer = GetNode<Timer>("TickTimer");
        LoadBalance();
        _tickTimer.Timeout += OnTick;
        _hud.SetMeters(_o2, _h2o, _power);
    }

    private void LoadBalance()
    {
        var file = FileAccess.Open("res://data/balance.json", FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PrintErr("balance.json not found");
            return;
        }
        var json = file.GetAsText();
        file.Close();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        _colonistO2 = (float)root.GetProperty("colonist_o2").GetDouble();
        _colonistH2O = (float)root.GetProperty("colonist_h2o").GetDouble();
        _solarPower = (float)root.GetProperty("solar_power").GetDouble();
        _recyclerH2O = (float)root.GetProperty("recycler_h2o").GetDouble();
        _hydroponicsO2 = (float)root.GetProperty("hydroponics_o2").GetDouble();
    }

    private void OnTick()
    {
        _o2 = Mathf.Clamp(_o2 + _hydroponicsO2 + _colonistO2 * 3, 0, 100);
        _h2o = Mathf.Clamp(_h2o + _recyclerH2O + _colonistH2O * 3, 0, 100);
        _power = Mathf.Clamp(_power + _solarPower, 0, 100);
        _hud.SetMeters(_o2, _h2o, _power);
        if (_o2 <= 0 || _h2o <= 0 || _power <= 0)
            GD.Print("Game Over");
    }
}
