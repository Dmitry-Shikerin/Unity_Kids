using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class RectangleModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Image Background { get; private set; }
    }
}