using System;
using UnityEngine;

namespace Project.GameInput
{
    public interface IQueueable
    {
        public Action<KeyCode> OnExecuteKey { get; set; }
    }
}
