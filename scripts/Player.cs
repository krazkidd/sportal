using Godot;
using System;

public partial class Player : CharacterBody2D
{

	private const float SPEED = 300.0f;
	private const float JUMP_VELOCITY = -400.0f;
	private readonly float GRAVITY = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	private AnimatedSprite2D animatedSprite2D;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += GRAVITY * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JUMP_VELOCITY;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0.0f)
		{
			velocity.X = direction * SPEED;


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
			velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);

			animatedSprite2D.Stop();
		}

		Velocity = velocity;

		MoveAndSlide();
	}

	#region IPortable

    public bool IsInPortal { get; set; }

	#endregion

}
