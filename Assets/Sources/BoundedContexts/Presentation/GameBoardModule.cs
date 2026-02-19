using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.BoundedContexts.Presentation
{
    public class GameBoardModule : EntityModule, IDropHandler
    {
        [field: SerializeField] public RectTransform TopTransform { get; private set; }
        [field: SerializeField] public RectTransform BotTransform { get; private set; }
        [field: SerializeField] public RectTransform LeftTransform { get; private set; }
        [field: SerializeField] public TMP_Text ConsoleText { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            RectangleModule module = eventData.pointerDrag.GetComponent<RectangleModule>();
            Entity.AddOnDropEvent(module);
        }
    }
}