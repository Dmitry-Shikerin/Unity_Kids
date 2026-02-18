using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.Slots;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Infrastructure.Factories;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.BoundedContexts.Systems
{
    [EcsSystem(28)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class SlotSystem : IProtoRunSystem
    {
        private readonly RectangleEntityFactory _factory;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                RectangleSlotTag,
                FillSlotEvent>());

        public SlotSystem(RectangleEntityFactory factory)
        {
            _factory = factory;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                RectangleColors color = entity.GetRectangleSlotColor().Value;
                ProtoEntity rectangleEntity = _factory.Create(color, entity);
                RectangleSlotModule slotModule = entity.GetRectangleSlotModule().Value;
                RectangleModule rectangleModule = rectangleEntity.GetRectangleModule().Value;
                rectangleModule.transform.SetParent(slotModule.transform);
                rectangleModule.transform.localPosition = Vector3.zero;
            }
        }
    }
}