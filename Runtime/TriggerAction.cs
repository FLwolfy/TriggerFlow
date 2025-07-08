using System;
using UnityEngine;

namespace TriggerFlow.Runtime
{
    [Serializable]
    public abstract class TriggerAction
    {
        public abstract void OnPerform(Collider other);
    }
}