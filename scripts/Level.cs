using Godot;
using System;

public partial class Level : Node2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node node in GetTree().GetNodesInGroup("portals"))
		{
			if (node is Portals portals)
			{
				portals.PortalActivated += (player, source, destination) => source.TeleportTo(player, destination);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}
	}
}
