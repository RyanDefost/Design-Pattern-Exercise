using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameInput
{
    public class InputQueue : IInputReceiver
    {
        public List<KeyCode> CurrentQueue { get; private set; }
        public Action OnSetCurrentQueue;

        private List<KeyCode> inputQueue = new List<KeyCode>();
        private int queueSize = 5;

        private InputHandler inputHandler;
        private UpComboCommand upComboCommand = new UpComboCommand();
        private DownComboCommand downComboCommand = new DownComboCommand();
        private LeftComboCommand leftComboCommand = new LeftComboCommand();
        private RightComboCommand rightComboCommand = new RightComboCommand();
        private EnterComboCommand enterComboCommand = new EnterComboCommand();


        public InputQueue()
        {
            this.inputHandler = new InputHandler(this);
            this.inputHandler.BindInputToCommand(KeyCode.UpArrow, upComboCommand);
            this.inputHandler.BindInputToCommand(KeyCode.DownArrow, downComboCommand);
            this.inputHandler.BindInputToCommand(KeyCode.LeftArrow, leftComboCommand);
            this.inputHandler.BindInputToCommand(KeyCode.RightArrow, rightComboCommand);

            this.inputHandler.BindInputToCommand(KeyCode.Space, enterComboCommand);

            //TryAssignAllQueueable();
        }

        public void UpdateInputQueue()
        {
            inputHandler.HandleInput();
        }

        public void SaveInputToQueue(KeyCode key)
        {
            this.inputQueue.Add(key);
            RemoveLastInputFromQueue();
        }

        public void RemoveLastInputFromQueue()
        {
            while (this.inputQueue.Count > this.queueSize)
            {
                this.inputQueue.RemoveAt(0);
            }
        }

        public void SetCurrentQueue()
        {
            if (this.inputQueue.Count <= 0) return;
            if (CurrentQueue == null) this.CurrentQueue = new List<KeyCode>();

            this.CurrentQueue.Clear();
            this.CurrentQueue.AddRange(this.inputQueue);

            this.inputQueue.Clear();

            Debug.Log("Invoke");
            this.OnSetCurrentQueue?.Invoke();
        }

        private void TryAssignAllQueueable()
        {
            if (this.inputHandler == null) return;

            foreach (var keyCommand in this.inputHandler.GetKeyCommands())
            {
                if (keyCommand.command is IQueueable)
                {
                    var queueable = keyCommand.command as IQueueable;
                    queueable.OnExecuteKey += SaveInputToQueue;
                }
            }
        }
    }
}
