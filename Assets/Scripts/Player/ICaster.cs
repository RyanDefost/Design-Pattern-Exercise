using UnityEngine;

namespace Project.Player
{
    /// <summary>
    /// Identifier for classes that have an CastingComponent.
    /// </summary>
    public interface ICaster
    {
        public CastingComponent CastingComponent { get; }

        public float CastArea { get; set; }
        public Vector2 Position { get; }
        public Color Team { get; }

    }
}
