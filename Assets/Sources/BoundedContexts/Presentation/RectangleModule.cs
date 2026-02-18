using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Sources.BoundedContexts.Presentation
{
    public class RectangleModule : EntityModule, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private HudView _hudView;
        
        [field: Required] [field: SerializeField] public Image Background { get; private set; }
        [field: Required] [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: Required] [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
        [field: SerializeField] public RectTransform TopTransform { get; private set; }
        [field: SerializeField] public RectTransform LeftTransform { get; private set; }
        [field: SerializeField] public RectTransform RightTransform { get; private set; }


        [Inject]
        private void Construct(HudView hudView) =>
            _hudView = hudView;

        public void OnBeginDrag(PointerEventData eventData) =>
            Entity.AddOnBeginDragEvent(eventData);

        public void OnDrag(PointerEventData eventData) =>
            RectTransform.anchoredPosition += eventData.delta / _hudView.MainCanvas.scaleFactor;

        public void OnEndDrag(PointerEventData eventData) =>
            Entity.AddOnEndDragEvent(eventData);
    }
}