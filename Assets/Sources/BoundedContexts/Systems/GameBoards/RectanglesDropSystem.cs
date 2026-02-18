using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.GameBoards
{
    [EcsSystem(30)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectanglesDropSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _gameBoardIt = new(
            It.Inc<
                GameBoardTag>());        
        [DI] private readonly ProtoIt _gameBoardDropIt = new(
            It.Inc<
                GameBoardTag,
                DropRectanglesEvent>());
        [DI] private readonly ProtoItExc _rectanglesIt = new(
            It.Inc<
                RectangleTag,
                IsOnGameBoardComponent>(),
            It.Exc<InPoolComponent>());
        [DI] private readonly ProtoIt _lastIt = new(
            It.Inc<
                RectangleTag,
                LastComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _gameBoardDropIt)
            {
                int index = 0;
                ProtoEntity temp = default;
                int len = _rectanglesIt.Len();
                int lastLen = _lastIt.Len();
                float maxWeight = 0;
                ProtoEntity maxTemp = default;
                            
                foreach (ProtoEntity itEntity in _rectanglesIt)
                {
                    RectangleModule entityModule = itEntity.GetRectangleModule().Value;
                    
                    if (temp == default)
                    {
                        entityModule.transform.position = new Vector3(entityModule.transform.position.x,
                            _gameBoardIt.First().Entity.GetGameBoardModule().Value.BotTransform.position.y);
                        temp = itEntity;
                        continue;
                    }
                    
                    entityModule.transform.position = 
                        new Vector3(entityModule.transform.position.x, temp.GetRectangleModule().Value.TopTransform.position.y);
                    temp = itEntity;
                }

                foreach (ProtoEntity itEntity in _rectanglesIt)
                {
                    index++;
                    Vector3 position = itEntity.GetRectangleModule().Value.TopTransform.position;

                    if (position.y > maxWeight)
                    {
                        maxWeight = position.y;
                        maxTemp = itEntity;
                    }
                    
                    if (index == len && lastLen == 0)
                        maxTemp.AddLast();
                }
            }
        }
    }
}