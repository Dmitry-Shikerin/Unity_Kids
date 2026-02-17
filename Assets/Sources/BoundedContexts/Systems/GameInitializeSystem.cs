using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Infrastructure.Factories;
using Sources.BoundedContexts.Presentation;
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
        private readonly RectangleEntityFactory _factory;

        public GameInitializeSystem(
            IAssetCollector assetCollector,
            HudView hudView,
            RectangleEntityFactory factory)
        {
            _assetCollector = assetCollector;
            _hudView = hudView;
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            // RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            // EntityLink prefab = config.View;
            //
            // foreach (KeyValuePair<RectangleColors, Sprite> sprite in config.Sprites)
            // {
            //     EntityLink instance = Object.Instantiate(prefab, _hudView.RectanglesParent, false);
            //     _factory.Create(instance, sprite.Key, sprite.Value);
            // }
        }
    }
}