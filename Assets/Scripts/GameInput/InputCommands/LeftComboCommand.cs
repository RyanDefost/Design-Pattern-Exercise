using System;
using UnityEngine;

namespace Project.GameInput
{
    public class LeftComboCommand : ICommand, IQueueable
    {
        public Action OnExecute { get; set; }
        public Action<KeyCode> OnExecuteKey { get; set; }

        //Executes when the correlating input has been given inside the InputHandler.
        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SaveInputToQueue(KeyCode.LeftArrow);

            Debug.Log("LEFT Command");
            this.OnExecute?.Invoke();
            this.OnExecuteKey?.Invoke(inputHandler.CurrentKey);

        }
    }
}