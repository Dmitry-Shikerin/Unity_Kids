using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Zenject;

namespace Sources.BoundedContexts.Infrastructure.Factories
{
    public class GameBoardEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public GameBoardEntityFactory(
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
            _repository = repository;
        }

        public ProtoEntity Create(EntityLink link)
        {
            GameBoardModule module = link.GetModule<GameBoardModule>();
            
            Aspect.GameBoard.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            entity.AddGameBoardModule(module);

            return entity;
        }
    }
}