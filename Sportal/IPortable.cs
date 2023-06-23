using Godot;
using System;

namespace Sportal
{
    public interface IPortable
    {

        public Vector2 GlobalPosition { get; set; }

        public bool IsInPortal { get; set; }

    }
}