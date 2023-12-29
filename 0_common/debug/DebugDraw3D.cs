using Game.DepressionClimber;
using Godot;
using System.Collections.Generic;

public partial class DebugDraw3D : Control
{
	List<Vector> vectors = new();

	public override void _Draw()
	{
		base._Draw();
		Camera3D camera = GetViewport().GetCamera3D();
		foreach (Vector item in vectors)
		{
			item.Draw(this, camera);
		}
	}

	public void DrawTriangle(Vector2 pos, Vector2 dir, int size, Color color)
	{
		Vector2 a = pos + dir * size;
		Vector2 b = pos + dir.Rotated(2 * Mathf.Pi / 3) * size;
		Vector2 c = pos + dir.Rotated(4 * Mathf.Pi / 3) * size;
		Vector2[] points = { a, b, c };
		Color[] colors = { color };
		DrawPolygon(points, colors);
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (!Visible)
			return;
		QueueRedraw();
	}

	public void AddVector(Node3D _gameObject, string _property, float _scale, int _width, Color _color)
	{
		vectors.Add(new Vector(_gameObject, _property, _scale, _width, _color));
	}

	public partial class Vector
	{
		Node3D gameObject;
		string property;
		float scale;
		int width;
		Color color;

		public Vector(Node3D _gameObject, string _property, float _scale, int _width, Color _color)
		{
			gameObject = _gameObject;
			property = _property;
			scale = _scale;
			width = _width;
			color = _color;
		}

		public void Draw(DebugDraw3D controlNode, Camera3D camera)
		{
			Vector2 start = camera.UnprojectPosition(gameObject.Position);
			Vector3 velocity = (Vector3)gameObject.Get(property);
			// Hack to better visualize velocity
			// velocity.Y = 0;
			Vector2 end = camera.UnprojectPosition(gameObject.Position + velocity * scale);
			controlNode.DrawLine(start, end, color, width);
			controlNode.DrawTriangle(end, start.DirectionTo(end), width * 2, color);
		}
	}
}
