using Godot;
using System;

public partial class Portal : Area2D
{

	[Signal]
	public delegate void PlayerEnteredPortalEventHandler(Player player);



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += (Node2D body) =>
		{
			if (body is Player p && !p.IsInPortal)
			{
				p.IsInPortal = true;

				EmitSignal(SignalName.PlayerEnteredPortal, p);
			}
		};

		BodyExited += (Node2D body) =>
		{
			if (body is Player p && p.IsInPortal)
			{
				p.IsInPortal = false;
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
