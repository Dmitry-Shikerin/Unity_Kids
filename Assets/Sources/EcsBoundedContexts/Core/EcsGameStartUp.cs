using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Components.Events;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.SaveLoads.Domain;

namespace Sources.EcsBoundedContexts.Core
{
    public class EcsGameStartUp : IEcsGameStartUp
    {
        private readonly ProtoSystems _systems;
        private readonly ProtoWorld _world;
        private readonly ISystemsCollector _systemsCollector;
        private ProtoSystems _unitySystems;
        private bool _isInitialize;

        public EcsGameStartUp(
            ProtoWorld protoWorld,
            ProtoSystems systems,
            ISystemsCollector systemsCollector)
        {
            _world = protoWorld;
            _systems = systems;
            _systemsCollector = systemsCollector;
        }

        public void Initialize()
        {
            InitUnitySystems();
            AddModules();
            _systemsCollector.AddSystems();
            AddOneFrame();
            _systems.Init();
            Init();
        }

        public void Update(float deltaTime)
        {
            if (_isInitialize == false)
                return;
            
            _unitySystems?.Run();
            _systems?.Run();
        }

        public void Destroy()
        {
            _systems?.Destroy();
            _unitySystems?.Destroy();
        }

        private void AddModules()
        {
            _systems.AddModule(new AutoInjectModule());
        }

        private void AddOneFrame()
        {
            _systems.DelHere<SaveDataEvent>();
            _systems.DelHere<ClearDataEvent>();
            _systems.DelHere<EnableGameObjectEvent>();
            _systems.DelHere<DisableGameObjectEvent>();
            _systems.DelHere<OnBeginDragEvent>();
            _systems.DelHere<OnEndDragEvent>();
            _systems.DelHere<MoveToEvent>();
            _systems.DelHere<OnDropEvent>();
            _systems.DelHere<DestroyEvent>();
            _systems.DelHere<FillSlotEvent>();
            _systems.DelHere<DropRectanglesEvent>();
            _systems.DelHere<PrintEvent>();
            _systems.DelHere<LoadGameEvent>();
        }
        
        private void Init()
        {
            _isInitialize = true;
        }

        private void InitUnitySystems()
        {
            _unitySystems = new ProtoSystems(_world);
            _unitySystems
                .AddModule(new AutoInjectModule())
                .AddModule(new UnityModule())
                .Init();
        }
    }
}
