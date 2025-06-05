using System;
using UnityEngine;

namespace Project.GameInput
{
    public class LeftComboCommand : ICommand, IQueueable
    {
        public Action OnExecute { get; set; }
        public Action<KeyCode> OnExecuteKey { get; set; }

        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SaveInputToQueue(inputHandler.CurrentKey);

            Debug.Log("LEFT Command");
            this.OnExecute?.Invoke();
            this.OnExecuteKey?.Invoke(inputHandler.CurrentKey);

        }
    }
}