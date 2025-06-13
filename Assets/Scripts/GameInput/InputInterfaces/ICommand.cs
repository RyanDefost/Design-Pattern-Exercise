using System;

namespace Project.GameInput
{
    /// <summary>
    /// Interface for classes that identifies compatible classes for the InputHandler.
    /// </summary>
    public interface ICommand
    {
        public Action OnExecute { get; set; }

        //Executes when the correlating input has been given inside the InputHandler.
        public void Execute(InputHandler inputHandler);
    }
}

