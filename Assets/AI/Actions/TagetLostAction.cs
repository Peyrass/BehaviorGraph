using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TagetLostAction", story: "Check if [target] is lost by [self]", category: "MyActions", id: "a735ba0fbf8baee25651e6c28b3f3493")]
public partial class TargetLostAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DetectionRadius;
    [SerializeReference] public BlackboardVariable<float> DetectionAngle;
    [SerializeReference] public BlackboardVariable<string> TargetLayerName;
    [SerializeReference] public BlackboardVariable<string> EnemyLayerName;

    private LayerMask obstacleLayerMask;

    protected override Status OnStart()
    {
        // Solo necesitamos saber qué bloquea la vista
        obstacleLayerMask = ~LayerMask.GetMask(TargetLayerName, EnemyLayerName);
        
        // Si no hay un objetivo asignado al empezar, la pérdida es inmediata
        if (Target.Value == null) return Status.Success;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Target.Value == null) return Status.Success;

        Vector3 directionToTarget = Target.Value.transform.position - Self.Value.transform.position;
        float distance = directionToTarget.magnitude;

        // 1. ¿Se salió del radio?
        if (distance > DetectionRadius.Value) return Status.Success;

        // 2. ¿Se salió del ángulo de visión?
        if (Vector3.Angle(directionToTarget, Self.Value.transform.forward) > DetectionAngle.Value / 2f)
            return Status.Success;

        // 3. ¿Hay un obstáculo en medio? (Raycast)
        if (Physics.Raycast(Self.Value.transform.position, directionToTarget, distance, obstacleLayerMask))
            return Status.Success;

        // Si ninguna de las anteriores se cumple, el objetivo SIGUE a la vista
        return Status.Running;
    }
}
