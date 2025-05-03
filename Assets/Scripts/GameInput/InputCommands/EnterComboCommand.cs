using System;
using UnityEngine;

namespace Project.GameInput
{
    public class EnterComboCommand : ICommand
    {
        public Action OnExecute { get; set; }

        public void Execute(InputHandler inputHandler)
        {
            inputHandler.InputQueue.SetCurrentQueue();

            Debug.Log("ENTER Command");
        }
    }
}