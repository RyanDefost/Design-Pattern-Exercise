using System.Collections.Generic;
using UnityEngine;

namespace Project.GameInput
{
    public class InputHandler
    {
        public ICommand UpCommand;
        public ICommand DownCommand;
        public ICommand LeftCommand;
        public ICommand RightCommand;
        public ICommand EnterCommand;

        public KeyCode CurrentKey { get; private set; }
        public InputQueue InputQueue { get; private set; }

        private List<KeyCommands> keyCommands = new List<KeyCommands>();

        public InputHandler(InputQueue queue)
        {
            this.InputQueue = queue;

            BindInputToCommand(KeyCode.UpArrow, new UpComboCommand());
            BindInputToCommand(KeyCode.DownArrow, new DownComboCommand());
            BindInputToCommand(KeyCode.LeftArrow, new LeftComboCommand());
            BindInputToCommand(KeyCode.RightArrow, new RightComboCommand());

            BindInputToCommand(KeyCode.Space, new EnterComboCommand());
        }

        public void HandleInput()
        {
            foreach (var keyCommand in this.keyCommands)
            {
                if (Input.GetKeyDown(keyCommand.key))
                {
                    this.CurrentKey = keyCommand.key;

                    keyCommand.command.Execute(this);
                }
            }
        }

        public void BindInputToCommand(KeyCode keycode, ICommand command)
        {
            this.keyCommands.Add(new KeyCommands()
            {
                key = keycode,
                command = command
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
    }
}