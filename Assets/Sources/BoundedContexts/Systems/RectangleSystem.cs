using DG.Tweening;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.BoundedContexts.Systems
{
    [EcsSystem(20)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleSystem : IProtoRunSystem
    {
        private readonly HudView _hudView;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                RectangleTag,
                OnBeginDragEvent>());

        [DI] private readonly ProtoIt _endIt = new(
            It.Inc<
                RectangleTag,
                OnEndDragEvent>());

        [DI] private readonly ProtoIt _moveIt = new(
            It.Inc<
                RectangleTag,
                MoveToEvent>());

        [DI] private readonly ProtoIt _destroyIt = new(
            It.Inc<
                RectangleTag,
                DestroyEvent>());

        [DI] private readonly ProtoIt _gameBoardIt = new(
            It.Inc<
                GameBoardTag>());

        [DI] private readonly ProtoIt _rectanglesIt = new(
            It.Inc<
                RectangleTag>());

        public RectangleSystem(HudView hudView)
        {
            _hudView = hudView;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                PointerEventData eventData = entity.GetOnBeginDragEvent().Value;

                DisableScroll();
                module.RectTransform.SetParent(_hudView.transform);
                module.RectTransform.SetAsLastSibling();
                module.CanvasGroup.blocksRaycasts = false;
            }

            foreach (ProtoEntity entity in _endIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                PointerEventData eventData = entity.GetOnBeginDragEvent().Value;
                RectangleSlotModule slotModule = entity.GetParentSlot().Value.GetRectangleSlotModule().Value;

                EnableScroll();

                if (IsInHole(eventData, module))
                    return;

                if (IsInGameBoard(eventData) == false)
                {
                    module.transform.SetParent(slotModule.transform);
                    module.transform.localPosition = Vector3.zero;
                }

                module.CanvasGroup.blocksRaycasts = true;
            }

            foreach (ProtoEntity entity in _moveIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                Vector3 pos = entity.GetMoveToEvent().Value;
                entity.GetParentSlot().Value.AddFillSlotEvent();
                module.transform.DOJump(pos, 300, 1, 0.5f).SetEase(Ease.Linear);
                entity.AddIsOnGameBoard();
            }

            foreach (ProtoEntity entity in _destroyIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;

                Sequence sequence = DOTween.Sequence();
                sequence.Join(
                    module.transform
                        .DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear));
                sequence.Join(
                    module.CanvasGroup
                        .DOFade(0f, 1f)
                        .SetEase(Ease.InOutQuad));
                sequence.onComplete = () => Object.Destroy(module.gameObject);
                sequence.Play();
            }
        }

        private bool IsInHole(PointerEventData eventData, RectangleModule module)
        {
            HoleView holeView = eventData.pointerEnter.GetComponent<HoleView>();
            ProtoEntity entity = module.Entity;

            if (holeView == null)
                return false;

            if (IsPointInside(Input.mousePosition) == false)
                return false;

            if (entity.HasIsOnGameBoard() == false)
                return false;

            module.transform
                .DOJump(holeView.transform.position, 300, 1, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    entity.DelIsOnGameBoard();
                    bool hasLast = entity.HasLast();

                    Object.Destroy(entity.GetRectangleModule().Value.gameObject);

                    if (hasLast)
                        return;

                    ProtoEntity temp = default;

                    foreach (ProtoEntity entity in _rectanglesIt)
                    {
                        RectangleModule entityModule = entity.GetRectangleModule().Value;

                        if (temp == default)
                        {
                            entityModule.transform.position = new Vector3(module.transform.position.x,
                                _gameBoardIt.First().Entity.GetGameBoardModule().Value.BotTransform.position.y);
                            temp = entity;
                            continue;
                        }

                        entityModule.transform.position = new Vector3(module.transform.position.x,
                            temp.GetRectangleModule().Value.TopTransform.position.y);
                        temp = entity;
                    }
                });

            module.CanvasGroup.blocksRaycasts = true;

            return true;
        }

        private bool IsInGameBoard(PointerEventData eventData)
        {
            GameBoardModule module = eventData.pointerEnter.GetComponent<GameBoardModule>();

            return module != null;
        }

        private void EnableScroll()
        {
            _hudView.ScrollRect.enabled = true;
        }

        private bool IsPointInside(Vector2 screenPosition)
        {
            RectTransform holeRect = (RectTransform)Object.FindObjectOfType<HoleView>().gameObject.transform;

            if (holeRect == null)
                return false;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    holeRect,
                    screenPosition,
                    Object.FindObjectOfType<HudView>().GetComponent<Canvas>().worldCamera,
                    out var localPoint) == false)
            {
                return false;
            }

            Rect rect = holeRect.rect;
            float semiX = rect.width * 0.5f;
            float semiY = rect.height * 0.5f;

            if (semiX <= 0f || semiY <= 0f)
                return false;

            float cx = rect.center.x;
            float cy = rect.center.y;
            float dx = (localPoint.x - cx) / semiX;
            float dy = (localPoint.y - cy) / semiY;

            var result = dx * dx + dy * dy <= 1f;

            return result;
        }

        private void DisableScroll()
        {
            _hudView.ScrollRect.StopMovement();
            _hudView.ScrollRect.enabled = false;
        }
    }
}