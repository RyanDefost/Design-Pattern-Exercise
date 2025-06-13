using System;
using UnityEngine;

namespace Project.GameInput
{
    /// <summary>
    /// Interface that shows if a input can be Queued inside the InputQueue.
    /// </summary>
    public interface IQueueable
    {
        public Action<KeyCode> OnExecuteKey { get; set; }
    }
}
