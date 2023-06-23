using Godot;
using System;

public partial class Portal : Area2D
{

	[Signal]
	public delegate void PlayerEnteredPortalEventHandler(Player player);
	[Signal]
	public delegate void PlayerExitedPortalEventHandler(Player player);

	[Export]
	public Vector2 Orientation { get; set; } = new Vector2(0.0f, 1.0f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += (Node2D body) =>
		{
			if (body is Player p)
			{
				EmitSignal(SignalName.PlayerEnteredPortal, p);
			}
		};

		BodyExited += (Node2D body) =>
		{
			if (body is Player p)
			{
				EmitSignal(SignalName.PlayerExitedPortal, p);
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
