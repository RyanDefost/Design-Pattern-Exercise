using System;
using UnityEngine;

namespace Project.GameInput.MovementInput
{
    public class MoveCommand : ICommand
    {
        public Action OnExecute { get; set; }

        private Vector2 direction = Vector2.zero;

        public MoveCommand(Vector2 direction)
        {
            this.direction = direction;
        }

        public void Execute(InputHandler inputHandler)
        {
            var actor = (Player.Player)inputHandler.inputReceiver;

            actor.AddPosition(direction * actor.speed * Time.deltaTime);

            this.OnExecute?.Invoke();
        }
    }
}
