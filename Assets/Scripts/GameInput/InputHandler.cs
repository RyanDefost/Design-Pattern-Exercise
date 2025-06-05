using System.Collections.Generic;
using UnityEngine;

namespace Project.GameInput
{
    public class InputHandler
    {
        public KeyCode CurrentKey { get; private set; }
        public IInputReceiver inputReceiver { get; private set; }

        private List<KeyCommands> keyCommands = new List<KeyCommands>();

        public InputHandler(IInputReceiver inputReceiver)
        {
            this.inputReceiver = inputReceiver;
        }

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

        public void BindInputToCommand(KeyCode keycode, ICommand command, bool isHolding = false)
        {
            this.keyCommands.Add(new KeyCommands()
            {
                key = keycode,
                command = command,
                isHolding = isHolding
            });
        }

        public void UnBindInput(KeyCode keycode)
        {
            var items = this.keyCommands.FindAll(x => x.key == keycode);
            items.ForEach(x => this.keyCommands.Remove(x));
        }

        public List<KeyCommands> GetKeyCommands() { return this.keyCommands; }
    }

    public class KeyCommands
    {
        public KeyCode key;
        public ICommand command;
        public bool isHolding;
    }
}