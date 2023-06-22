using Godot;
using System;

public partial class Portals : Node
{

	[Signal]
	public delegate void PortalActivatedEventHandler(Player player, Portal source, Portal destination);

	public Portal GreenPortal;
	public Portal RedPortal;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GreenPortal = GetNode<Portal>("GreenPortal");
		RedPortal = GetNode<Portal>("RedPortal");

		GreenPortal.PlayerEnteredPortal += (player) => EmitSignal(SignalName.PortalActivated, player, GreenPortal, RedPortal);
		RedPortal.PlayerEnteredPortal += (player) =>  EmitSignal(SignalName.PortalActivated, player, RedPortal, GreenPortal);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
