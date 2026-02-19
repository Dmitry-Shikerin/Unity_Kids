using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.BoundedContexts.Systems.GameBoards
{
    [EcsSystem(19)]
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

            if (rectangleEntity.HasInGameBoard())
                return;
            
            module.transform.SetParent(gameBoardModule.transform);

            if (_lastIt.Len() > 1)
                throw new IndexOutOfRangeException("LastIt.Len() > 1");
            
            if (CanPost(module, gameBoardModule))
            {
                rectangleEntity.GetParentSlot().Value.AddFillSlotEvent();
                entity.AddPrintEvent("PlaceCube");
                Vector3 targetPosition = GetTargetPosition(gameBoardModule, module);

                if (_lastIt.Len() > 0)
                {
                    ProtoEntity lastRectangleEntity = _lastIt.First().Entity;
                    lastRectangleEntity.DelLast();
                }
                
                rectangleEntity.AddMoveToEvent(targetPosition, () =>
                {
                    rectangleEntity.AddLast();
                    rectangleEntity.AddInGameBoard();
                });

                return;
            }

            entity.AddPrintEvent("MissedTower");
            rectangleEntity.AddDestroyEvent();
            rectangleEntity.GetParentSlot().Value.AddFillSlotEvent();
        }

        private Vector3 GetTargetPosition(GameBoardModule gameBoardModule, RectangleModule module)
        {
            if (_lastIt.Len() == 0)
                return new Vector3(module.transform.position.x, gameBoardModule.BotTransform.position.y);

            RectangleModule lastModule = _lastIt.First().Entity.GetRectangleModule().Value;
            float topLastRectanglePosY = lastModule.TopTransform.position.y;
            float leftPos = lastModule.LeftTransform.position.x;
            float rightPos = lastModule.RightTransform.position.x;
            float xPos = Random.Range(leftPos, rightPos);
            
            return new Vector3(xPos, topLastRectanglePosY);
        }

        private bool CanPost(RectangleModule module, GameBoardModule gameBoardModule)
        {
            if (_lastIt.Len() == 0)
                return true;
            
            ProtoEntity lastRectangleEntity = _lastIt.First().Entity;
            RectangleModule lastRectangleModule = lastRectangleEntity.GetRectangleModule().Value;
            float leftPos = lastRectangleModule.LeftTransform.position.x;
            float rightPos = lastRectangleModule.RightTransform.position.x;
            
            if (module.transform.position.x < leftPos || module.transform.position.x > rightPos)
                return false;

            if (module.transform.position.y < lastRectangleModule.TopTransform.position.y)
                return false;
            
            if (module.transform.position.y < gameBoardModule.BotTransform.position.y)
                return false;

            if (module.LeftTransform.position.x < gameBoardModule.LeftTransform.position.x)
                return false;

            return true;
        }
    }
}