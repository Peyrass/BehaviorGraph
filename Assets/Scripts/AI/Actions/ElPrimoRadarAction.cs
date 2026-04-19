using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ElPrimoRadar", story: "[Self] have seen [Target]", category: "MyActions",
    id: "8b911c684089f33ace14068c3dd7cd92")]
public partial class ElPrimoRadarAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> SenseRadius;
    [SerializeReference] public BlackboardVariable<string> TargetLayerName;

    private LayerMask targetLayerMask;
    private Collider[] results = new Collider[1];


    protected override Status OnStart()
    {
        targetLayerMask = LayerMask.GetMask(TargetLayerName.Value); //Capa objetivo.

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            Self.Value.transform.position,
            SenseRadius.Value,
            results,
            targetLayerMask
        );

        //Si no se detecta ninguna colisión espera
        if (hitCount <= 0)
            return Status.Running;


        Target.Value = results[0].gameObject; //Lo establece como player
        return Status.Success;
    }
}