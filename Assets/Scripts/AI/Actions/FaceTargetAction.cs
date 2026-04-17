using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FaceTargetAction", story: "[Self] faces to [Target]", category: "MyActions", id: "93478801eef561e1166f6de7ef2fd8b1")]
public partial class FaceTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> TurnSpeed;

    protected override Status OnUpdate()
    { 
        if (Target.Value == null) return Status.Failure;

        Vector3 direction = (Target.Value.transform.position - Self.Value.transform.position).normalized;
        direction.y = 0; // No queremos que el enemigo se incline hacia arriba/abajo

        if (direction == Vector3.zero) return Status.Running;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Self.Value.transform.rotation = Quaternion.Slerp(Self.Value.transform.rotation, targetRotation, Time.deltaTime * TurnSpeed.Value);

        return Status.Running;
    }
}

