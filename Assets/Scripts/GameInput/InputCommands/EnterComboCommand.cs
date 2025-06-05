using System;
using UnityEngine;

namespace Project.GameInput
{
    public class EnterComboCommand : ICommand
    {
        public Action OnExecute { get; set; }

        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SetCurrentQueue();

            Debug.Log("ENTER Command");
            OnExecute?.Invoke();
        }
    }
}