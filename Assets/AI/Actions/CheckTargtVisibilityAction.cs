using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTargtVisibility",
    story: "Is [self] still seing [Target] ?",
    category: "MyActions",
    id: "6631cdf4c5807d35fc3aef90850a2151")]
public partial class CheckTargtVisibilityAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DetectionRadius;
    [SerializeReference] public BlackboardVariable<string> TargetLayerName;
    [SerializeReference] public BlackboardVariable<string> EnemyLayerName;

    private LayerMask targetLayerMask;
    private LayerMask obstacleLayerMask;
    
    private Collider[] results = new Collider[1]; // Buffer que almacena los colliders y optimiza la memoria

    protected override Status OnStart()
    {
        // Inicialización de capas para el filtrado físico
        obstacleLayerMask = ~LayerMask.GetMask(TargetLayerName.Value, EnemyLayerName.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null || Target.Value == null)
            return Status.Failure;

        Vector3 origin = Self.Value.transform.position + Vector3.up * 1.0f;
        Vector3 directionToTarget = Target.Value.transform.position - origin;
        float distance = directionToTarget.magnitude;

        // DEBUG
        Debug.DrawRay(origin, directionToTarget.normalized * distance, Color.red);

        // 1. Check rango
        if (distance > DetectionRadius.Value)
        {
            Debug.Log("FAIL: Fuera de rango");
            Target.Value = null;
            return Status.Failure;
        }

        // 2. Check obstáculos
        bool hitObstacle = Physics.Raycast(
            origin,
            directionToTarget.normalized,
            distance,
            obstacleLayerMask);

        if (hitObstacle)
        {
            Debug.Log("FAIL: Obstáculo detectado");
            Target.Value = null;
            return Status.Failure;
        }

        return Status.Success;
    }
}

