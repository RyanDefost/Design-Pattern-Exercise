using System;
using Project.GameLogic.Systems;
using UnityEngine;

namespace Project.GameInput.MovementInput
{
    public class MoveCommand : ICommand
    {
        public Action OnExecute { get; set; }

        private Vector2 direction = Vector2.zero;
        private Vector2 lastStep = Vector2.one;

        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();
        
        public MoveCommand(Vector2 direction)
        {
            this.direction = direction;
        }

        //Executes when the correlating input has been given inside the InputHandler.
        public void Execute(InputHandler inputHandler)
        {
            var actor = (Player.Player)inputHandler.inputReceiver;

            if (!collisionSystem.CheckCollisions(actor.CollisionComponent))
                actor.AddPosition(direction * actor.speed * Time.deltaTime);
            else
                actor.AddPosition(-(direction * actor.speed * Time.deltaTime)*2);
                            
            this.OnExecute?.Invoke();
        }
    }
}
