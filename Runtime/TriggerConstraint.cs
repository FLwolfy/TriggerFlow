using System;
using UnityEngine;

namespace TriggerFlow.Runtime
{
    [Serializable]
    public abstract class TriggerConstraint
    {
        public abstract bool OnCheck(Collider other);
    }
}