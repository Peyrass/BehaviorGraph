using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.Serialization;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlertAction",
    story: "[Self] shout and alert [companionActivate]",
    category: "MyActions",
    id: "67cc408e7f16f6cb6b3cd34e60a469fa")]
public partial class AlertAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [FormerlySerializedAs("CompanionActivate")]
    [SerializeReference] public BlackboardVariable<bool> companionActivate;

    protected override Status OnStart()
    {
        Debug.Log($"{Self.Value.name} está gritando para alertar al compañero!");
        
        if (companionActivate != null)
        {
            companionActivate.Value = true; // Aquí sedespierta al Shooter
        }
        
        return Status.Success;
    }
}