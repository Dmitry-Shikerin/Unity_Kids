using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        private List<RectangleModule> _modules = new List<RectangleModule>();

        public async void Remove(RectangleModule delModule)
        {
            Debug.Log($"Remove");
            bool isLust = delModule._isLust;
            _modules.Remove(delModule);
            Destroy(delModule.gameObject);
            Debug.Log($"Если не попал в дыру то уничтожить");

            if (isLust)
                return;

            RectangleModule temp = null;

            foreach (RectangleModule module in _modules)
            {
                if (temp == null)
                {
                    module.MoveDown(new Vector3(module.transform.position.x, _botTransform.position.y));
                    temp = module;
                    continue;
                }

                Debug.Log($"Move");
                module.MoveDown(new Vector3(module.transform.position.x, temp.TopTransform.position.y));
                temp = module;

                await UniTask.Yield();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            RectangleModule module =
                (RectangleModule)eventData.pointerDrag.GetComponent<RectangleModule>();

            if (module._isOnGameBoard)
                return;
            
            module.SetGameBoard(this);
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
                Debug.Log($"Lust position");

                var leftPos = _lustModule.LeftTransform.position.x;
                var rightPos = _lustModule.RightTransform.position.x;
                //Debug.Log($"LeftPos {leftPos}, rightPos {rightPos}, position {module.transform.position}");

                if (module.transform.position.x > leftPos && module.transform.position.x < rightPos)
                {
                    //Debug.Log($"попал");
                    _module = module;
                }
                else
                {
                    //Debug.Log($"Не попал уничтожаем");
                    // Создаем последовательность анимаций
                    Sequence sequence = DOTween.Sequence();
        
                    // Добавляем вращение вокруг оси Z (для UI обычно используется Z)
                    sequence.Join(module.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear));
        
                    // Добавляем исчезание (прозрачность)
                    sequence.Join(module._canvasGroup.DOFade(0f, 1f)
                        .SetEase(Ease.InOutQuad));
        
                    sequence.onComplete = () => Destroy(module.gameObject);
                    // Запускаем
                    sequence.Play();
                    
                    return;
                }
            }
            
            //Debug.Log($"OnDrop");
            module.MoveDown(new Vector3(pos.x, downPos.y));
            module._isLust = true;

            if (_lustModule != null)
            {
                _lustModule._isLust = false;
            }
            _lustModule = module;
            _modules.Add(module);
        }
    }
}