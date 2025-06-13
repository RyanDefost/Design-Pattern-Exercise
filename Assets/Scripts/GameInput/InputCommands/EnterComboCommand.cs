using System;
using UnityEngine;

namespace Project.GameInput
{
    public class EnterComboCommand : ICommand
    {
        public Action OnExecute { get; set; }

        //Executes when the correlating input has been given inside the InputHandler.
        public void Execute(InputHandler inputHandler)
        {
            var actor = (InputQueue)inputHandler.inputReceiver;

            actor.SetCurrentQueue();

            Debug.Log("ENTER Command");
            OnExecute?.Invoke();
        }
    }
}