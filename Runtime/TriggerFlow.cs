using System.Collections.Generic;
using UnityEngine;

namespace TriggerFlow.Runtime
{
    [CreateAssetMenu(menuName = "Trigger/TriggerFlow")]
    public class TriggerFlow : ScriptableObject
    {
        public TriggerType triggerType = TriggerType.OnTriggerEnter;

        [SerializeReference]
        public List<TriggerConstraint> constraints = new List<TriggerConstraint>();

        [SerializeReference]
        public List<TriggerAction> actions = new List<TriggerAction>();
    }
}