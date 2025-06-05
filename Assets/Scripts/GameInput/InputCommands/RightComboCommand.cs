using System;
using UnityEngine;

namespace Project.GameInput
{
    public class RightComboCommand : ICommand, IQueueable
    {
        public Action OnExecute { get; set; }
        public Action<KeyCode> OnExecuteKey { get; set; }

        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SaveInputToQueue(inputHandler.CurrentKey);

            Debug.Log("RIGHT Command");
            this.OnExecute?.Invoke();
            this.OnExecuteKey?.Invoke(inputHandler.CurrentKey);
        }
    }
}