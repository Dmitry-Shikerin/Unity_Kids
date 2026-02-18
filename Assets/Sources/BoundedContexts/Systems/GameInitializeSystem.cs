using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Infrastructure.Factories;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Systems
{
    [EcsSystem(5)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class GameInitializeSystem : IProtoInitSystem
    {
        private readonly IAssetCollector _assetCollector;
        private readonly HudView _hudView;
        private readonly SlotEntityFactory _slotEntityFactory;
        private readonly GameBoardEntityFactory _gameBoardEntityFactory;
        private readonly RectangleEntityFactory _rectangleEntityFactory;

        public GameInitializeSystem(
            IAssetCollector assetCollector,
            HudView hudView,
            SlotEntityFactory slotEntityFactory,
            GameBoardEntityFactory gameBoardEntityFactory,
            RectangleEntityFactory rectangleEntityFactory)
        {
            _assetCollector = assetCollector;
            _hudView = hudView;
            _slotEntityFactory = slotEntityFactory;
            _gameBoardEntityFactory = gameBoardEntityFactory;
            _rectangleEntityFactory = rectangleEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _gameBoardEntityFactory.Create(_hudView.GameBoardLink);
            RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            
            foreach (KeyValuePair<RectangleColors, Sprite> sprite in config.Sprites)
            {
                RectTransform scroll = _hudView.ScrollRect.content;
                ProtoEntity slotEntity = _slotEntityFactory.Create(sprite.Key);
                RectangleSlotModule slotModule = slotEntity.GetRectangleSlotModule().Value;
                slotModule.transform.SetParent(scroll);
                ProtoEntity rectangleEntity = _rectangleEntityFactory.Create(sprite.Key, slotEntity);
                slotEntity.AddChildRectangle(rectangleEntity);
                RectangleModule rectangleModule = rectangleEntity.GetRectangleModule().Value;
                rectangleModule.transform.SetParent(slotModule.transform);
                rectangleModule.transform.localPosition = Vector3.zero;
            }
        }
    }
}