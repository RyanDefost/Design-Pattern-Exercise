using System;

namespace Project.GameInput
{
    public interface ICommand
    {
        public Action OnExecute { get; set; }

        public void Execute(InputHandler inputHandler);
    }
}

