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

        private List<KeyCode> _inputQueue = new List<KeyCode>();
        private int _queueSize = 5;

        public InputQueue()
        {
            inputHandler = new InputHandler(this);

            TryAssignAllQueueable();
        }

        public void UpdateQueue()
        {
            inputHandler.HandleInput();
        }

        public void SaveInputToQueue(KeyCode key)
        {
            _inputQueue.Add(key);
            RemoveLastInputFromQueue();
        }

        public void RemoveLastInputFromQueue()
        {
            while (_inputQueue.Count > _queueSize)
            {
                _inputQueue.RemoveAt(0);
            }
        }

        public void SetCurrentQueue()
        {
            if (_inputQueue.Count <= 0) return;
            if (CurrentQueue == null) CurrentQueue = new List<KeyCode>();

            CurrentQueue.Clear();
            CurrentQueue.AddRange(_inputQueue);

            _inputQueue.Clear();

            Debug.Log("SETTING C_QUEUE: " + CurrentQueue);
            OnSetCurrentQueue?.Invoke();
        }

        private void TryAssignAllQueueable()
        {
            if (inputHandler == null) return;

            foreach (var keyCommand in inputHandler.GetKeyCommands())
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
