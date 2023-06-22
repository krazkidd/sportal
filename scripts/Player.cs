using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private AnimatedSprite2D animatedSprite2D;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0.0f)
		{
			velocity.X = direction * Speed;


			if (velocity.X < 0.0f)
			{
				animatedSprite2D.FlipH = true;
			}
			else if (velocity.X > 0.0f)
			{
				animatedSprite2D.FlipH = false;
			}

			animatedSprite2D.Play();
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

			animatedSprite2D.Stop();
		}

		Velocity = velocity;

		MoveAndSlide();
	}
}
