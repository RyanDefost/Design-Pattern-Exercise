using Project.GameInput;
using Project.GameInput.MovementInput;
using Project.GameLogic;
using Project.Summon;
using UnityEngine;

namespace Project.Player
{
    public class Player : Entity, IInputReceiver, ICaster
    {
        public Vector2 Position { get => this.gameObject.transform.position; }
        public PlayerData PlayerData { get; set; }
        public InputQueue InputQueue { get; set; }
        public MinionCreator MinionCreator { get; set; }


        private MinionManager MinionManager = ISingleton<MinionManager>.Instance();

        private InputHandler inputHandler;
        private MoveCommand MoveUpCommand = new MoveCommand(Vector2.up);
        private MoveCommand MoveDownCommand = new MoveCommand(Vector2.down);
        private MoveCommand MoveLeftCommand = new MoveCommand(Vector2.left);
        private MoveCommand MoveRightCommand = new MoveCommand(Vector2.right);


        public Player(PlayerData data)
        {
            SetCastingRefrences();

            this.PlayerData = data;

            this.spriteRenderer.color = PlayerData.teamColor;
            this.gameObject.name = data.Name;

            this.inputHandler = new InputHandler(this);
            this.inputHandler.BindInputToCommand(data.inputCodes[0], MoveUpCommand, true);
            this.inputHandler.BindInputToCommand(data.inputCodes[1], MoveDownCommand, true);
            this.inputHandler.BindInputToCommand(data.inputCodes[2], MoveLeftCommand, true);
            this.inputHandler.BindInputToCommand(data.inputCodes[3], MoveRightCommand, true);
        }

        public void UpdatePlayer()
        {
            this.inputHandler.HandleInput();
            this.InputQueue.UpdateInputQueue();

            this.UpdateEntity();
        }
        private void SetCastingRefrences()
        {
            this.InputQueue = new InputQueue();
            this.MinionCreator = new MinionCreator(this);

            this.InputQueue.OnSetCurrentQueue += Cast;
        }

        private void Cast()
        {
            this.MinionManager.ActivateMinion(this.MinionCreator);
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
