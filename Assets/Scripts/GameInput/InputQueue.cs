using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameInput
{
    public class InputQueue
    {
        public List<KeyCode> CurrentQueue { get; private set; }
        public Action OnSetCurrentQueue;

        private InputHandler inputHandler;

        private List<KeyCode> inputQueue = new List<KeyCode>();
        private int queueSize = 5;

        public InputQueue()
        {
            this.inputHandler = new InputHandler(this);

            TryAssignAllQueueable();
        }

        public void UpdateQueue()
        {
            this.inputHandler.HandleInput();
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
