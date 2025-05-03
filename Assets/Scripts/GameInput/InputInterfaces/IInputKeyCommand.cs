using System;
using UnityEngine;

namespace Project.GameInput
{
    public interface IInputKeyCommand : ICommand
    {
        public Action<KeyCode> OnExecute { get; set; }

        public void Execute();
    }
}

