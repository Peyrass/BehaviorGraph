using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ElPrimoEnemySearch", story: "[Self] has seen [Enemy]", category: "MyActions",
    id: "0cb594361134cc79d3570a346f2abcfc")]
public partial class ElPrimoEnemySearchAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<float> DetectionRadius;
    [SerializeReference] public BlackboardVariable<string> EnemyLayerName;
    [SerializeReference] public BlackboardVariable<string> ObstacleLayerName;

    private LayerMask enemyLayerMask;
    private LayerMask obstacleLayerMask;

    private Collider[] results = new Collider[1];

    protected override Status OnStart()
    {
        enemyLayerMask = LayerMask.GetMask(EnemyLayerName.Value); //Capa objetivo
        obstacleLayerMask = LayerMask.GetMask(ObstacleLayerName.Value); //Única capa obstáculo
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Physics.OverlapSphereNonAlloc(
                Self.Value.transform.position,
                DetectionRadius.Value,
                results,
                enemyLayerMask) <= 0)
            return Status.Running;
        
        var origin = Self.Value.transform.position;
        var targetPos = results[0].transform.position;

        var distanceToTarget = targetPos - origin;

        if (Physics.Raycast(origin,
                distanceToTarget.normalized,
                distanceToTarget.magnitude,
                obstacleLayerMask)) 
            return Status.Running;

        Enemy.Value = results[0].gameObject; //Objetivo escogido
        return Status.Success;
    }
}