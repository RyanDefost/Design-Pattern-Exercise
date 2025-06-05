using Project.GameInput;
using Project.GameInput.MovementInput;
using Project.GameLogic;
using UnityEngine;

namespace Project.Player
{
    public class Player : Entity, IInputReceiver
    {
        public string name;
        public Color teamColor;

        private InputHandler inputHandler;
        private MoveCommand MoveUpCommand = new MoveCommand(Vector2.up);
        private MoveCommand MoveDownCommand = new MoveCommand(Vector2.down);
        private MoveCommand MoveLeftCommand = new MoveCommand(Vector2.left);
        private MoveCommand MoveRightCommand = new MoveCommand(Vector2.right);

        public Player(PlayerData data)
        {
            SetPlayerData(data);

            this.inputHandler = new InputHandler(this);

            this.inputHandler.BindInputToCommand(data.inputCodes[0], MoveUpCommand);
            this.inputHandler.BindInputToCommand(data.inputCodes[1], MoveDownCommand);
            this.inputHandler.BindInputToCommand(data.inputCodes[2], MoveLeftCommand);
            this.inputHandler.BindInputToCommand(data.inputCodes[3], MoveRightCommand);
        }

        public void UpdatePlayer()
        {
            this.inputHandler.HandleInput();
            this.UpdateEntity();
        }

        private void SetPlayerData(PlayerData data)
        {
            this.name = data.Name;
            spriteRenderer.color = data.teamColor;
        }
    }

    public struct PlayerData
    {
        public string Name;
        public Color teamColor;
        public Vector2 spawnPosition;

        public KeyCode[] inputCodes;
    }
}
