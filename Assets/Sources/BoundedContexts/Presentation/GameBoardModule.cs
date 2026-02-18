using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.BoundedContexts.Presentation
{
    public class GameBoardModule : EntityModule, IDropHandler
    {
        [field: SerializeField] public RectTransform TopTransform { get; private set; }
        [field: SerializeField] public RectTransform BotTransform { get; private set; }
        

        public void OnDrop(PointerEventData eventData) =>
            Entity.AddOnDropEvent(eventData);
    }
}