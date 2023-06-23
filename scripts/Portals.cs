using Godot;
using System;

public partial class Portals : Node
{

	[Signal]
	public delegate void PlayerTeleportedEventHandler(Player player, Portal source, Portal destination);

	private Portal GreenPortal;
	private Portal RedPortal;

	private Portal _sourcePortal = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GreenPortal = GetNode<Portal>("GreenPortal");
		RedPortal = GetNode<Portal>("RedPortal");

		GreenPortal.PlayerEnteredPortal += (player) => HandlePlayerEnteredPortal(player, GreenPortal);
		GreenPortal.PlayerExitedPortal += (player) => HandlePlayerExitedPortal(player, RedPortal);

		RedPortal.PlayerEnteredPortal += (player) => HandlePlayerEnteredPortal(player, RedPortal);
		RedPortal.PlayerExitedPortal += (player) => HandlePlayerExitedPortal(player, GreenPortal);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void HandlePlayerEnteredPortal(Player player, Portal portal)
	{
		// NOTE: The player may enter the destination portal and fire off
		// 		 this signal, so we have to check this flag isn't set.

		if (!player.IsInPortal)
		{
			player.IsInPortal = true;

			_sourcePortal = portal;
		}
	}

	private void Teleport(Player player, Portal source, Portal destination)
	{
		Vector2 positionOffset = player.GlobalPosition - source.GlobalPosition;
		player.GlobalPosition = destination.GlobalPosition + positionOffset;

		EmitSignal(SignalName.PlayerTeleported, player, source, destination);
	}

	private void HandlePlayerExitedPortal(Player player, Portal portal)
	{
		// NOTE: We wait until the player exits the source portal to
		//       do the actual teleportatoin.

		if (player.IsInPortal)
		{
			Teleport(player, _sourcePortal, portal);

			player.IsInPortal = false;

			_sourcePortal = null;
		}
	}

}
