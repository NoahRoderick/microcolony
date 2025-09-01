using Godot;
using System.Text.Json;

public partial class Game : Node2D
{
    private const int ColonistCount = 3;

    [Export] public NodePath HudPath;
    [Export] public NodePath SolarPath;
    [Export] public NodePath RecyclerPath;
    [Export] public NodePath HydroponicsPath;

    private UIHUD _hud = default!;
    private Timer _tickTimer = default!;
    private Building _solar = default!;
    private Building _recycler = default!;
    private Building _hydroponics = default!;

    private float _o2 = 100f;
    private float _h2o = 100f;
    private float _power = 100f;
    private float _remaining = 600f;

    private float _colonistO2;
    private float _colonistH2O;
    private float _solarPower;
    private float _recyclerH2O;
    private float _hydroponicsO2;
    private float _recyclerPowerCost;
    private float _hydroponicsPowerCost;

    public override void _Ready()
    {
        _hud = GetNode<UIHUD>(HudPath);
        _tickTimer = GetNode<Timer>("TickTimer");
        _solar = GetNode<Building>(SolarPath);
        _recycler = GetNode<Building>(RecyclerPath);
        _hydroponics = GetNode<Building>(HydroponicsPath);

        LoadBalance();

        _hud.SolarButton.Toggled += _solar.SetOn;
        _hud.RecyclerButton.Toggled += _recycler.SetOn;
        _hud.HydroponicsButton.Toggled += _hydroponics.SetOn;
        _hud.RestartButton.Pressed += Restart;

        _tickTimer.Timeout += OnTick;
        _hud.RestartButton.Visible = false;
        _hud.SetMeters(_o2, _h2o, _power);
        _hud.SetTimer(_remaining);
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
        _recyclerPowerCost = (float)root.GetProperty("recycler_power_cost").GetDouble();
        _hydroponicsPowerCost = (float)root.GetProperty("hydroponics_power_cost").GetDouble();
        _recycler.PowerCost = _recyclerPowerCost;
        _hydroponics.PowerCost = _hydroponicsPowerCost;
    }

    private void OnTick()
    {
        _o2 = Mathf.Clamp(
            _o2 + (_hydroponics.IsOn ? _hydroponicsO2 : 0) + _colonistO2 * ColonistCount,
            0,
            100);
        _h2o = Mathf.Clamp(
            _h2o + (_recycler.IsOn ? _recyclerH2O : 0) + _colonistH2O * ColonistCount,
            0,
            100);
        _power = Mathf.Clamp(
            _power + (_solar.IsOn ? _solarPower : 0)
                   - (_recycler.IsOn ? _recyclerPowerCost : 0)
                   - (_hydroponics.IsOn ? _hydroponicsPowerCost : 0),
            0,
            100);

        _remaining -= 1f;

        _hud.SetMeters(_o2, _h2o, _power);
        _hud.SetTimer(_remaining);

        if (_o2 <= 0 || _h2o <= 0 || _power <= 0)
        {
            _hud.ShowEnd("Mission Failed");
            _tickTimer.Stop();
        }
        else if (_remaining <= 0)
        {
            _hud.ShowEnd("Mission Complete");
            _tickTimer.Stop();
        }
    }

    private void Restart()
    {
        GetTree().ReloadCurrentScene();
    }
}
