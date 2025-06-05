using Project.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerManager : GameBehaviour
    {
        private List<Player> players = new List<Player>();

        public PlayerManager()
        {
            PlayerData player1 = new PlayerData();
            player1.Name = "Player 1";
            player1.teamColor = Color.blue;
            player1.spawnPosition = new Vector2(10, 0);
            player1.inputCodes = new KeyCode[4] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
            SpawnPlayer(player1);

            PlayerData player2 = new PlayerData();
            player2.Name = "Player 2";
            player2.teamColor = Color.red;
            player2.spawnPosition = new Vector2(-10, 0);
            player2.inputCodes = new KeyCode[4] { KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L };
            SpawnPlayer(player2);
        }

        public override void Update()
        {
            UpdatePlayers();
        }

        public void SpawnPlayer(PlayerData data)
        {
            Player player = new Player(data);
            this.players.Add(player);
        }

        public void DeSpawnPlayer(Player player)
        {
            this.players.Remove(player);
        }

        public List<Player> GetPlayers() => players;

        private void UpdatePlayers()
        {
            foreach (var player in this.players)
            {
                player.UpdatePlayer();
            }
        }
    }
}
