using TriggerFlow.Runtime;
using UnityEngine;

namespace TriggerFlow.Example.Actions
{
    public class DebugAction : TriggerAction
    {
        public string message = "Triggered!";
        public bool applyNamePrefix = false;

        public override void OnPerform(Collider other)
        {
            string str = applyNamePrefix ? message + ' ' + other.name : message;
            Debug.Log(str);
        }
    }
}