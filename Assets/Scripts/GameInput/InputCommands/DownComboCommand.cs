using System;
using UnityEngine;

namespace Project.GameInput
{
    public class DownComboCommand : ICommand, IQueueable
    {
        public Action OnExecute { get; set; }
        public Action<KeyCode> OnExecuteKey { get; set; }

        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SaveInputToQueue(KeyCode.DownArrow);

            Debug.Log("DOWN Command");
            this.OnExecute?.Invoke();
            this.OnExecuteKey?.Invoke(inputHandler.CurrentKey);
        }
    }
}