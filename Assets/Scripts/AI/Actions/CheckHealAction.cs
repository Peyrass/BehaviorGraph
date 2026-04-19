using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.InputSystem;



[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckHealAction", story: "[Target] [NeedsHelp]", category: "MyActions", id: "a8223288c60eac06014d55e19051c1e6")]
public partial class CheckHealAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<bool> NeedsHelp;
    protected override Status OnStart()
    {
        if (NeedsHelp.Value == true) 
        {
            return Status.Success; // ¡Pasa a curar!
        }

        return Status.Failure;
    }
}