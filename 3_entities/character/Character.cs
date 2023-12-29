using Godot;
using System;

public partial class Character : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int MoveDirection = 0;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("invert_gravity") && IsOnFloor())
		{
			// Invert Gravity here
			UpDirection = -UpDirection;
			gravity = -gravity;
		}

		MoveDirection = 0;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		MoveDirection = 0;
		if (Input.IsActionPressed("move_left"))
			MoveDirection -= 1;
		if (Input.IsActionPressed("move_right"))
			MoveDirection += 1;

		velocity.X = MoveDirection * Speed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
