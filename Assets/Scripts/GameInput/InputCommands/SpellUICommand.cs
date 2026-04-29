using System;
using UnityEngine;

namespace Project.GameInput
{
    public class SpellUICommand : ICommand
    {
        public Action OnExecute { get; set; }
        public Action<KeyCode> OnExecuteKey { get; set; }
        
        public void Execute(InputHandler inputHandler)
        {
                Debug.Log("Open SpellBook");
                this.OnExecute?.Invoke();
                this.OnExecuteKey?.Invoke(inputHandler.CurrentKey);
        }
    }
}