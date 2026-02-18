using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Components.Slots;
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
    public class SlotEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityPool _pool;

        public SlotEntityFactory(
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
            _pool = poolManager.GetPool<RectangleSlotTag>();
            _pool.InitPool(Create);
        }

        public ProtoEntity Create(RectangleColors color)
        {
            ProtoEntity entity = _pool.Get();

            if (entity.HasRectangleSlotColor())
                entity.ReplaceRectangleSlotColor(color);
            else
                entity.AddRectangleSlotColor(color);
            
            return entity;
        }
        
        private ProtoEntity Create()
        {
            RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            EntityLink link = Object.Instantiate(config.RectangleSlot);
            RectangleSlotModule module = link.GetModule<RectangleSlotModule>();
            
            Aspect.RectangleSlot.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            entity.AddRectangleSlotModule(module);
            entity.AddTransform(module.transform);

            return entity;
        }
    }
}