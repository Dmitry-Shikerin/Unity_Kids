using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.BoundedContexts.Presentation
{
    public class GameBoardModule : EntityModule, IDropHandler
    {
        [SerializeField] private RectTransform _topTransform;
        [SerializeField] private RectTransform _botTransform;
        
        public void OnDrop(PointerEventData eventData)
        {
            RectangleModule module = 
                (RectangleModule)eventData.pointerDrag.GetComponent<RectangleModule>();
            module.transform.SetParent(transform);
            module.transform.localPosition = Vector3.zero;
            var pos = eventData.pointerDrag.transform.position;
            var downPos = _botTransform.localPosition;
            Debug.Log($"OnDrop");
            module.MoveDown(downPos);
        }
    }
}