using DG.Tweening;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.Rectangles
{
    [EcsSystem(26)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleMoveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _moveIt = new(
            It.Inc<
                RectangleTag,
                MoveToEvent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _moveIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                Vector3 pos = entity.GetMoveToEvent().Value;
                module.transform
                    .DOJump(pos, 300, 1, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        entity.AddLast();
                    });
                entity.AddIsOnGameBoard();
            }
        }
    }
}