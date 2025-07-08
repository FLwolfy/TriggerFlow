using System.Collections.Generic;
using UnityEngine;

namespace TriggerFlow.Runtime
{
    public class TriggerController : MonoBehaviour
    {
        public List<TriggerFlow> flows = new();
        
        private void OnTriggerEnter(Collider other)
        {
            RunBehaviors(TriggerType.OnTriggerEnter, other);
        }

        private void OnTriggerStay(Collider other)
        {
            RunBehaviors(TriggerType.OnTriggerStay, other);
        }

        private void OnTriggerExit(Collider other)
        {
            RunBehaviors(TriggerType.OnTriggerExit, other);
        }

        private void RunBehaviors(TriggerType type, Collider other)
        {
            foreach (var behavior in flows)
            {
                if (behavior == null) continue;
                if (behavior.triggerType != type) continue;

                bool allPass = true;
                foreach (var c in behavior.constraints)
                {
                    if (c == null || !c.OnCheck(other))
                    {
                        allPass = false;
                        break;
                    }
                }

                if (!allPass) continue;

                foreach (var a in behavior.actions)
                {
                    if (a != null) a.OnPerform(other);
                }
            }
        }
    }


}