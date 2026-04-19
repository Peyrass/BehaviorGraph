using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.InputSystem;


[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckHealAction", story: "[Target] needs help", category: "Action", id: "a8223288c60eac06014d55e19051c1e6")]
public partial class CheckHealAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    
    protected override Status OnUpdate()
    {
        //
        if (Keyboard.current.hKey.isPressed)
        {
            Debug.Log("¡Voy a curarte jefe!");
            return Status.Success;
        }
        
        return Status.Failure; //el árbol salta a la siguiente prioridad
    }
}