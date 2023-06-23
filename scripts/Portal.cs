using Godot;
using System;

public partial class Portal : Area2D
{

	[Signal]
	public delegate void PlayerEnteredPortalEventHandler(Player player);
	[Signal]
	public delegate void PlayerExitedPortalEventHandler(Player player);


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

				// we emit this signal elsewhere
				//EmitSignal(SignalName.PlayerExitedPortal, p);
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TeleportTo(Player player, Portal destination)
	{
		Vector2 positionOffset = player.GlobalPosition - GlobalPosition;
		player.GlobalPosition = destination.GlobalPosition + positionOffset;

		destination.EmitSignal(SignalName.PlayerExitedPortal, player);
	}

}
