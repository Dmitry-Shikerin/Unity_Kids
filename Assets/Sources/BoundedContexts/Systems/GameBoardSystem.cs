using System;
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
    public class GameBoardSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                GameBoardTag,
                OnDropEvent>());     
        [DI] private readonly ProtoIt _lastIt = new(
            It.Inc<
                RectangleTag,
                LastComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                 OnDrop(entity);
            }
        }

        private void OnDrop(ProtoEntity entity)
        {
            PointerEventData eventData = entity.GetOnDropEvent().Value;
            GameBoardModule gameBoardModule = entity.GetGameBoardModule().Value;
            RectangleModule module = eventData.pointerDrag.GetComponent<RectangleModule>();
            ProtoEntity rectangleEntity = module.Entity;
            
            if (rectangleEntity.HasIsOnGameBoard())
                return;
            
            module.transform.SetParent(gameBoardModule.transform);

            if (_lastIt.Len() > 1)
                throw new IndexOutOfRangeException("LastIt.Len() > 1");

            ProtoEntity lastRectangleEntity = _lastIt.First().Entity;
            RectangleModule lastRectangleModule = lastRectangleEntity.GetRectangleModule().Value;

            if (CanPost(module))
            {
                rectangleEntity.AddLast();
                lastRectangleEntity.DelLast();
                Vector3 targetPosition = GetTargetPosition(gameBoardModule, lastRectangleModule);
                rectangleEntity.AddMoveToEvent(targetPosition);
                return;
            }

            rectangleEntity.AddDestroyEvent();
        }

        private Vector3 GetTargetPosition(GameBoardModule gameBoardModule, RectangleModule lastRectangleModule)
        {
            return _lastIt.Len() == 0
                ? gameBoardModule.BotTransform.position
                : lastRectangleModule.TopTransform.position;
        }

        private bool CanPost(RectangleModule module)
        {
            if (_lastIt.Len() == 0)
                return true;
            
            ProtoEntity lastRectangleEntity = _lastIt.First().Entity;
            RectangleModule lastRectangleModule = lastRectangleEntity.GetRectangleModule().Value;
            float leftPos = lastRectangleModule.LeftTransform.position.x;
            float rightPos = lastRectangleModule.RightTransform.position.x;
            
            if (module.transform.position.x < leftPos || module.transform.position.x > rightPos)
                return false;

            return true;
        }
    }
}