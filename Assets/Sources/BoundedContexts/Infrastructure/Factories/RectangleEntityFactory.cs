using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using Zenject;

namespace Sources.BoundedContexts.Infrastructure.Factories
{
    public class RectangleEntityFactory : EntityFactory
    {
        public RectangleEntityFactory(
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
        }

        public override ProtoEntity Create(EntityLink link)
        {
            Aspect.Rectangle.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            
            return entity;
        }
        
        public ProtoEntity Create(EntityLink link, RectangleColors color, Sprite sprite)
        {
            ProtoEntity entity = Create(link);
            
            RectangleModule module = link.GetModule<RectangleModule>();
            module.Background.sprite = sprite;
            entity.AddRectangleColor(color);
            
            return entity;
        }
    }
}