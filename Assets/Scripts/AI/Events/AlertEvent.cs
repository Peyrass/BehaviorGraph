using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AlertEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AlertEvent", message: "[Self] has spoted [Target]", category: "MyEvents", id: "e76cc6ab2068fb6fafcb6c2a6267a901")]
public sealed partial class AlertEvent : EventChannel<GameObject, GameObject> { }

