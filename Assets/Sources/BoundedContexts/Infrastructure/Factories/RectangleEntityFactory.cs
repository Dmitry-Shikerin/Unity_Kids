using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
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
            throw new System.NotImplementedException();
        }
    }
}