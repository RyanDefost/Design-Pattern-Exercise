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

        public KeyCode currentKey { get; private set; }
        public InputQueue inputQueue { get; private set; }

        private List<KeyCommands> keyCommands = new List<KeyCommands>();

        public InputHandler(InputQueue queue)
        {
            inputQueue = queue;

            BindInputToCommand(KeyCode.UpArrow, new UpComboCommand());
            BindInputToCommand(KeyCode.DownArrow, new DownComboCommand());
            BindInputToCommand(KeyCode.LeftArrow, new LeftComboCommand());
            BindInputToCommand(KeyCode.RightArrow, new RightComboCommand());

            BindInputToCommand(KeyCode.Space, new EnterComboCommand());
        }

        public void HandleInput()
        {
            foreach (var keyCommand in keyCommands)
            {
                if (Input.GetKeyDown(keyCommand.key))
                {
                    currentKey = keyCommand.key;

                    keyCommand.command.Execute(this);
                }
            }
        }

        public void BindInputToCommand(KeyCode keycode, ICommand command)
        {
            keyCommands.Add(new KeyCommands()
            {
                key = keycode,
                command = command
            });
        }

        public void UnBindInput(KeyCode keycode)
        {
            var items = keyCommands.FindAll(x => x.key == keycode);
            items.ForEach(x => keyCommands.Remove(x));
        }

        public List<KeyCommands> GetKeyCommands() { return keyCommands; }
    }

    public class KeyCommands
    {
        public KeyCode key;
        public ICommand command;
    }
}