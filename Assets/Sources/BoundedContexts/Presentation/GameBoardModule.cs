using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.BoundedContexts.Presentation
{
    public class GameBoardModule : EntityModule, IDropHandler
    {
        [SerializeField] private RectTransform _topTransform;
        [SerializeField] private RectTransform _botTransform;
        [SerializeField] private RectangleModule _module;

        private RectangleModule _lustModule;

        public void OnDrop(PointerEventData eventData)
        {
            RectangleModule module =
                (RectangleModule)eventData.pointerDrag.GetComponent<RectangleModule>();
            module.transform.SetParent(transform);
            //module.transform.localPosition = Vector3.zero;
            var pos = eventData.position;

            Vector3 downPos;

            if (_lustModule == null)
            {
                downPos = _botTransform.position;
            }
            else
            {
                downPos = _lustModule.TopTransform.position;

                var leftPos = _lustModule.LeftTransform.position.x;
                var rightPos = _lustModule.RightTransform.position.x;
                Debug.Log($"LeftPos {leftPos}, rightPos {rightPos}, position {module.transform.position}");
                
                if (module.transform.position.x > leftPos && module.transform.position.x < rightPos)
                {
                    Debug.Log($"попал");
                    _module = module;
                }
                else
                {
                    Debug.Log($"Не попал уничтожаем");
                    Destroy(module.gameObject);
                }
            }


            Debug.Log($"OnDrop");
            module.MoveDown(new Vector3(pos.x, downPos.y));
            _lustModule = module;
        }
    }
}