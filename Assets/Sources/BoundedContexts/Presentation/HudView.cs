using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class HudView : MonoBehaviour
    {
        [field: Required] [field: SerializeField] public List<EntityLink> Rectangles { get; private set; }
        [field: Required] [field: SerializeField] public Transform RectanglesParent { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink GameBoardLink { get; private set; }
        [field: Required] [field: SerializeField] public ScrollRect ScrollRect { get; private set; }
        [field: Required] [field: SerializeField] public Canvas MainCanvas { get; private set; }
    }
}