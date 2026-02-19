using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.Rectangles
{
    [EcsSystem(21)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleEndDragSystem : IProtoRunSystem
    {
        private readonly HudView _hudView;

        [DI] private readonly ProtoItExc _endIt = new(
            It.Inc<
                RectangleTag,
                OnEndDragEvent>(),
            It.Exc<InPoolComponent>());
        [DI] private readonly ProtoIt _gameBoardIt = new(
            It.Inc<
                GameBoardTag>());   

        public RectangleEndDragSystem(HudView hudView)
        {
            _hudView = hudView;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _endIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                GameObject pointerEnter = entity.GetOnEndDragEvent().Value;
                ProtoEntity gameBoardEntity = _gameBoardIt.First().Entity;

                EnableScroll();

                if (IsInGameBoardButNotHole(pointerEnter, module))
                {
                    _gameBoardIt.First().Entity.AddPrintEvent("NoGettingIntoHole");
                    entity.DelInGameBoard();
                    
                    if (entity.HasLast())
                        entity.DelLast();
                    
                    entity.AddDestroyEvent();
                    _gameBoardIt.First().Entity.AddDropRectanglesEvent();
                    module.CanvasGroup.blocksRaycasts = true;
                    continue;
                }

                if (IsInHole(pointerEnter, module))
                {
                    HoleView holeView = pointerEnter.GetComponent<HoleView>();
                    entity.DelInGameBoard();
                    
                    if (entity.HasLast())
                        entity.DelLast();
                    
                    entity.AddMoveToEvent(holeView.transform.position, () =>
                        entity.GetReturnToPoolAction().ReturnToPool.Invoke());
                    gameBoardEntity.AddDropRectanglesEvent();
                    gameBoardEntity.AddPrintEvent("HittingHole");
                    
                    continue;
                }

                if (IsInGameBoard(pointerEnter) == false)
                {
                    RectangleSlotModule slotModule = entity.GetParentSlot().Value.GetRectangleSlotModule().Value;
                    module.transform.SetParent(slotModule.transform);
                    module.transform.localPosition = Vector3.zero;
                    
                    if (entity.HasPrintEvent() == false)
                        gameBoardEntity.AddPrintEvent("MissedPlayingField");
                }

                module.CanvasGroup.blocksRaycasts = true;
            }
        }

        private bool IsInGameBoardButNotHole(GameObject pointerEnter, RectangleModule module)
        {
            if (pointerEnter == null)
            {
                return false;
            }
            
            HoleView holeView = pointerEnter.GetComponent<HoleView>();
            ProtoEntity entity = module.Entity;

            return (holeView == null || IsPointInside(Input.mousePosition) == false) && entity.HasInGameBoard();
        }
        
        private bool IsInHole(GameObject eventData, RectangleModule module)
        {
            HoleView holeView = eventData.GetComponent<HoleView>();
            ProtoEntity entity = module.Entity;

            if (holeView == null)
                return false;

            if (IsPointInside(Input.mousePosition) == false)
                return false;

            if (entity.HasInGameBoard() == false)
                return false;

            return true;
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

        private bool IsInGameBoard(GameObject pointerEnter)
        {
            GameBoardModule module = pointerEnter.GetComponent<GameBoardModule>();

            return module != null;
        }

        private void EnableScroll()
        {
            _hudView.ScrollRect.enabled = true;
        }
    }
}