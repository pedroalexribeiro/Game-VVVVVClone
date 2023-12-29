using System.Collections.Generic;
using Godot;

public partial class EventBus : Node
{
	public static EventBus Instance { get; } = new EventBus();

	// Global bus
	public Dictionary<string, HashSet<Callable>> CallableList;

	public static void AddListener(string eventName, Callable listener)
	{
		Instance.CallableList.TryAdd(eventName, new HashSet<Callable>());
		Instance.CallableList.TryGetValue(eventName, out HashSet<Callable> HashSet);
		HashSet.Add(listener);
	}

	public static void RemoveListener(string eventName, Callable listener)
	{
		if (!Instance.CallableList.ContainsKey(eventName))
			GD.PushError($"{eventName} is not a known event");
		Instance.CallableList.TryGetValue(eventName, out HashSet<Callable> HashSet);
		HashSet.Remove(listener);
	}

	public static void EmitSignal(string eventName)
	{
		Instance.CallableList.TryGetValue(eventName, out HashSet<Callable> eventListeners);
		foreach (Callable item in eventListeners)
		{
			item.CallDeferred();
		}
	}
}
