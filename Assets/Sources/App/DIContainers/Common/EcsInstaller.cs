using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.EntityPools.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Features;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.MyLeoEcsProto.Repositories.Impl;
using UnityEngine;
using Zenject;

namespace Sources.App.DIContainers.Common
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEcsGameStartUp>().To<EcsGameStartUp>().AsSingle();
            GameAspect aspect = new GameAspect();
            ProtoWorld world = new ProtoWorld(aspect);
            ProtoSystems systems = new ProtoSystems(world);
            Container.Bind<ProtoWorld>().FromInstance(world).AsSingle();
            Container.Bind<GameAspect>().FromInstance(aspect).AsSingle();
            Container.Bind<ProtoSystems>().FromInstance(systems).AsSingle();
            Container.Bind<IEventBuffer>().To<EventBuffer>().AsSingle();
            Container.Bind<IEntityRepository>().To<EntityRepository>().AsSingle();
            Container.Bind<IFeatureService>().To<FeatureService>().AsSingle();
            
            //Pools
            Container.Bind<IEntityPoolManager>().To<EntityPoolManager>().AsSingle();
        }
    }
}