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

namespace Sources.BoundedContexts.Systems.GameBoards
{
    [EcsSystem(24)]
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
            GameBoardModule gameBoardModule = entity.GetGameBoardModule().Value;
            RectangleModule module = entity.GetOnDropEvent().Value;

            if (module == null)
                throw new ArgumentNullException(nameof(module));
            
            ProtoEntity rectangleEntity = module.Entity;
            
            if (rectangleEntity.HasIsOnGameBoard())
                return;
            
            module.transform.SetParent(gameBoardModule.transform);

            if (_lastIt.Len() > 1)
                throw new IndexOutOfRangeException("LastIt.Len() > 1");
            
            if (CanPost(module))
            {
                Vector3 targetPosition = GetTargetPosition(gameBoardModule, module);
                rectangleEntity.AddMoveToEvent(targetPosition);
                rectangleEntity.GetParentSlot().Value.AddFillSlotEvent();
                
                if (_lastIt.Len() > 0)
                {
                    ProtoEntity lastRectangleEntity = _lastIt.First().Entity;
                    lastRectangleEntity.DelLast();
                }
                
                return;
            }

            rectangleEntity.AddDestroyEvent();
            rectangleEntity.GetParentSlot().Value.AddFillSlotEvent();
        }

        private Vector3 GetTargetPosition(GameBoardModule gameBoardModule, RectangleModule module)
        {
            float xPos = module.transform.position.x;
            
            Debug.Log($"Can laST {_lastIt.Len() > 0}");
            
            return _lastIt.Len() == 0
                ? new Vector3(xPos, gameBoardModule.BotTransform.position.y)
                : new Vector3(xPos, _lastIt.First().Entity.GetRectangleModule().Value.TopTransform.position.y);
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