using Godot;
using System;
using System.Collections.Generic;

public partial class ChangeResources : Control
{
	[Export] private Container _tabContainer;
	private bool _tabsVisible = false;

	public override void _Ready()
	{
		base._Ready();
		if (!InputMap.HasAction("toggle_debug_tabs"))
		{
			InputMap.AddAction("toggle_debug_tabs");
			InputEventKey ev = new()
			{
				Keycode = Key.T
			};
			InputMap.ActionAddEvent("toggle_debug_tabs", ev);
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (Input.IsActionJustPressed("toggle_debug_tabs"))
		{
			Visible = !Visible;
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!Visible)
			return;
	}

	public Control AddDebugTab(string name, PackedScene tab)
	{
		Control control = tab.Instantiate<Control>();
		_tabContainer.AddChild(control);
		control.Name = name;
		return control;
	}

	public void RemoveDebugTab(Control tab)
	{
		_tabContainer.RemoveChild(tab);
	}
}
