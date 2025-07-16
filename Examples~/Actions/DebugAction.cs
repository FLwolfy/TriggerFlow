using TriggerFlow.Runtime;
using UnityEngine;

namespace TriggerFlow.Example.Actions
{
    public class DebugAction : TriggerAction
    {
        public string message = "Triggered!";
        public bool applyNameSuffix = false;

        public override void OnPerform(Collider other)
        {
            string str = applyNameSuffix ? message + ' ' + other.name : message;
            Debug.Log(str);
        }
    }
}