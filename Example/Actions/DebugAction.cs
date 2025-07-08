using TriggerFlow.Runtime;
using UnityEngine;

namespace TriggerFlow.Example.Actions
{
    public class DebugAction : TriggerAction
    {
        public string message = "Triggered!";

        public override void OnPerform(Collider other)
        {
            Debug.Log($"{message} {other.name}");
        }
    }
}