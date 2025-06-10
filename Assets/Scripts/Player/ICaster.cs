using UnityEngine;

namespace Project.Player
{
    public interface ICaster
    {
        public CastingComponent CastingComponent { get; }
        //public MinionCreator MinionCreator { get; }
        //public inputQueue inputQueue { get; }


        public float CastArea { get; set; }
        public Vector2 Position { get; }
        public Color Team { get; }

    }
}
