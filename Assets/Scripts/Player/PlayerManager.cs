using Project.GameLogic;
using Project.GameLogic.ServiceLocator;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerManager : GameBehaviour
    {
        private List<Player> players = new List<Player>();

        public PlayerManager()
        {
            MultiServiceLocator.Provide<PlayerManager>(this);

            PlayerData player1 = new PlayerData();
            player1.Name = "Player 1";
            player1.team = Color.blue;
            player1.health = 100;
            player1.speed = 2;
            player1.spawnPosition = new Vector2(10, 0);
            player1.movementInput = new KeyCode[4] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
            player1.castInput = new KeyCode[5] { KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L, KeyCode.Space };
            SpawnPlayer(player1);

            PlayerData player2 = new PlayerData();
            player2.Name = "Player 2";
            player2.team = Color.red;
            player2.health = 100;
            player1.speed = 2;
            player2.spawnPosition = new Vector2(-10, 0);
            player2.movementInput = new KeyCode[4] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
            player2.castInput = new KeyCode[5] { KeyCode.Keypad5, KeyCode.Keypad2, KeyCode.Keypad1, KeyCode.Keypad3, KeyCode.KeypadEnter };
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
