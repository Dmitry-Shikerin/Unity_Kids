using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.BoundedContexts.Domain
{
    [CreateAssetMenu(fileName = nameof(RectangleConfig), menuName = "Configs/" + nameof(RectangleConfig), order = 51)]
    public class RectangleConfig : SerializedScriptableObject
    {
         [field: Required] [field: SerializeField] public EntityLink Rectangle { get; private set; }
         [field: Required] [field: SerializeField] public EntityLink RectangleSlot { get; private set; }
         [field: SerializeField] public ColorsSpritesDictionary Sprites { get; private set; }
    }
}