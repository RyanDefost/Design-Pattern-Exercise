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
            OnExecute?.Invoke();
            OnExecuteKey?.Invoke(inputHandler.currentKey);

            Debug.Log("RIGHT Command");
        }
    }
}