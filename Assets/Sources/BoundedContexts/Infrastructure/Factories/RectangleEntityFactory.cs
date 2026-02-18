using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.EntityPools.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using Zenject;

namespace Sources.BoundedContexts.Infrastructure.Factories
{
    public class RectangleEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityPool _pool;

        public RectangleEntityFactory(
            IAssetCollector assetCollector,
            IEntityPoolManager poolManager,
            IEntityRepository repository,
            ProtoWorld world, 
            GameAspect aspect,
            DiContainer container)
            : base(
                repository,
                world,
                aspect,
                container)
        {
            _assetCollector = assetCollector;
            _pool = poolManager.GetPool<RectangleTag>();
            _pool.InitPool(Create);
        }

        public ProtoEntity Create(RectangleColors color, ProtoEntity parentSlot)
        {
            ProtoEntity entity = _pool.Get();
            RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            entity.GetRectangleModule().Value.Background.sprite = config.Sprites[color];
            entity.AddRectangleColor(color);

            if (entity.HasParentSlot())
                entity.ReplaceParentSlot(parentSlot);
            else
                entity.AddParentSlot(parentSlot);

            return entity;
        }

        private ProtoEntity Create()
        {
            RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            EntityLink link = Object.Instantiate(config.Rectangle);
            RectangleModule module = link.GetModule<RectangleModule>();
            
            Aspect.Rectangle.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            entity.AddRectangleModule(module);
            
            return entity;
        }
    }
}