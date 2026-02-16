using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers
{
    [EcsSystem(68)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class GameOverSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        private ProtoEntity _bunker;
        private bool _isDeath;

        private ProtoEntity _killEnemyCounter;

        public GameOverSystem(
            IEntityRepository entityRepository,
            IStorageService storageService)
        {
            _entityRepository = entityRepository;
            _storageService = storageService;
        }

        public void Run()
        {
            if (true)
                return;
            
            // if (_bunker.HasHealth())
            //     return;
            
            OnDeath();
        }

        public void Init(IProtoSystems systems)
        {
            // _killEnemyCounter = _entityRepository.GetByName(IdsConst.KillEnemyCounter);
            // _bunker = _entityRepository.GetByName(IdsConst.Bunker);
        }

        private void OnDeath()
        {
            if (_isDeath)
                return;

            // int score = _killEnemyCounter.GetKillEnemyCounter().Value;
            // _sdkService.SetPlayerScore(score);
            // _storageService.ClearAll();
            // _uiViewService.Show(UiViewId.GameOverView);
            // _uiViewService.Get<GameOverUiView>().ScoreText.text = score.ToString();
            // _isDeath = true;
        }
    }
}