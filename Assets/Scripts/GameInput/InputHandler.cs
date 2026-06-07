using System.Collections.Generic;
using UnityEngine;

namespace Project.GameInput
{
    /// <summary>
    /// Checks for assigned inputs from the UnityEngine, calls any function that is assigned and called by Unity.
    /// </summary>
    public class InputHandler
    {
        public KeyCode CurrentKey { get; private set; }
        public IInputReceiver inputReceiver { get; private set; }

        private List<KeyCommands> keyCommands = new List<KeyCommands>();

        public InputHandler(IInputReceiver inputReceiver)
        {
            this.inputReceiver = inputReceiver;
        }

        // Checks if any of the assigned commands are pressed by the player.
        public void HandleInput()
        {
            foreach (var keyCommand in this.keyCommands)
            {
                if (Input.GetKeyDown(keyCommand.key) && !keyCommand.isHolding)
                {
                    this.CurrentKey = keyCommand.key;

                    keyCommand.command.Execute(this);
                }

                if (Input.GetKey(keyCommand.key) && keyCommand.isHolding)
                {
                    this.CurrentKey = keyCommand.key;

                    keyCommand.command.Execute(this);
                }
            }
        }

        // Binds an input code to a compatible ICommand class.
        public void BindInputToCommand(KeyCode keycode, ICommand command, bool isHolding = false)
        {
            this.keyCommands.Add(new KeyCommands()
            {
                key = keycode,
                command = command,
                isHolding = isHolding
            });
        }

        // Unbinds an input code from being checked by the InputHandler.
        public void UnBindInput(KeyCode keycode)
        {
            var items = this.keyCommands.FindAll(x => x.key == keycode);
            items.ForEach(x => this.keyCommands.Remove(x));
        }

        public List<KeyCommands> GetKeyCommands() { return this.keyCommands; }
    }

    /// <summary>
    /// A container for information about the input and classes that need to be called from the InputHandler.
    /// </summary>
    public class KeyCommands
    {
        public KeyCode key;
        public ICommand command;
        public bool isHolding;
    }
}