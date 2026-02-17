using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class RectangleModule : EntityModule, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [field: Required] [field: SerializeField] public Image Background { get; private set; }
        [field: Required] [field: SerializeField] public RectangleSlotModule SlotModule { get; private set; }
        [Required] [SerializeField] private RectTransform _rectTransform;
        [Required] [SerializeField] private Canvas _mainCanvas;
        [Required] [SerializeField] private CanvasGroup _canvasGroup;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            //Transform slotTransform = _rectTransform.parent;
            //slotTransform.SetAsLastSibling();
            _rectTransform.SetParent(_mainCanvas.transform);
            _rectTransform.SetAsLastSibling();
            
            _canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            //transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }

        public void MoveDown(Vector3 pos)
        {
            SlotModule.FillSlot();
            transform.localPosition = pos;
            Debug.Log($"Заполнить слот");
        }
    }
}