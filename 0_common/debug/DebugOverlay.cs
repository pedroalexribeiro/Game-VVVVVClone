using Game.DepressionClimber;
using Godot;
using System;

public partial class DebugOverlay : CanvasLayer
{
	[Export] private DebugDraw3D _draw;
	[Export] private DrawText _textDraw;
	[Export] private ChangeResources _tabDraw;

	public override void _Ready()
	{
		base._Ready();
		if (!InputMap.HasAction("toggle_debug"))
		{
			InputMap.AddAction("toggle_debug");
			InputEventKey ev = new()
			{
				Keycode = Key.Backslash
			};
			InputMap.ActionAddEvent("toggle_debug", ev);
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (Input.IsActionJustPressed("toggle_debug"))
		{
			foreach (Control item in GetChildren())
			{
				item.Visible = !item.Visible;
			}
		}
	}

	public void AddVectorDraw(Node3D gameObject, string property, int width = 4, float scale = 0.3f, Color? color = null)
	{
		if (color == null)
			color = new Color(0, 1, 0, 0.5f);
		_draw.AddVector(gameObject, property, scale, width, (Color)color);
	}

	public void AddTextDraw(IDebuggableText gameObject, string property)
	{
		_textDraw.AddDebugInfo(gameObject, property);
	}

	public void AddTabDraw(string property, PackedScene tab)
	{

		_tabDraw.AddDebugTab(property, tab);
	}
}
