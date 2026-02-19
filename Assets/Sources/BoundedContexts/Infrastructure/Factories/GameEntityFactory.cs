using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Zenject;

namespace Sources.BoundedContexts.Infrastructure.Factories
{
    public class GameEntityFactory : EntityFactory
    {
        public GameEntityFactory(
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
        
        public ProtoEntity Create()
        {
            Aspect.Game.NewEntity(out ProtoEntity entity);
            return entity;
        }
    }
}