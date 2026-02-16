using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.BoundedContexts.Presentation
{
    public class HudView : MonoBehaviour
    {
        [field: Required] [field: SerializeField] public List<EntityLink> Rectangles { get; private set; }
        [field: Required] [field: SerializeField] public Transform RectanglesParent { get; private set; }
    }
}