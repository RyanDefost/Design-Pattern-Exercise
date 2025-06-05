using Project.GameInput;
using Project.Summon;
using UnityEngine;

namespace Project.Player
{
    public interface ICaster
    {
        public Vector2 Position { get; }

        public PlayerData PlayerData { get; set; }

        public InputQueue InputQueue { get; set; }

        public MinionCreator MinionCreator { get; set; }
    }
}
