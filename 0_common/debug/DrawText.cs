using Game.DepressionClimber;
using Godot;
using System.Collections.Generic;
using System.Globalization;

public partial class DrawText : Control
{
	[Export]
	private Container _textContainer;
	private SortedDictionary<string, IDebuggableText> _debugText;

	private List<Label> _availableDebugLabels;

	public override void _Ready()
	{
		base._Ready();
		_debugText = new();
		_availableDebugLabels = new();
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!Visible)
			return;
		int currentLabelIndex = 0;
		Label currentLabel;
		foreach (KeyValuePair<string, IDebuggableText> kvp in _debugText)
		{
			if (currentLabelIndex < _availableDebugLabels.Count)
			{
				currentLabel = _availableDebugLabels[currentLabelIndex];
			}
			else
			{
				currentLabel = new Label();
				_availableDebugLabels.Add(currentLabel);
				_textContainer.AddChild(currentLabel);
			}
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			currentLabel.Text = textInfo.ToTitleCase(kvp.Key) + ": " + kvp.Value.GetPropertyText(kvp.Key);
			currentLabelIndex += 1;
		}
		if (currentLabelIndex < _availableDebugLabels.Count - 1)
		{
			int totalAvailableLables = _availableDebugLabels.Count;
			for (int i = currentLabelIndex; i < totalAvailableLables; i++)
			{
				Label deleteLabel = _availableDebugLabels[currentLabelIndex + 1];
				_availableDebugLabels.Remove(deleteLabel);
				deleteLabel.QueueFree();
			}
		}
	}

	public void AddDebugInfo(IDebuggableText node, string property)
	{
		if (!_debugText.TryAdd(property, node))
			GD.PushWarning("Two values added with teh same info");
	}

	// Improve add struct with specific configuration to allow better debug showings
}
