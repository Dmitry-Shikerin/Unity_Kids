using DG.Tweening;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.Game;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.Rectangles
{
    [EcsSystem(26)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class RectangleMoveSystem : IProtoRunSystem
    {
        private readonly HudView _hudView;
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _rectIt = new(
            It.Inc<
                RectangleTag,
                LastComponent>());        
        [DI] private readonly ProtoIt _gameBoardIt = new(
            It.Inc<
                GameBoardTag>());
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                GameTag>());
        [DI] private readonly ProtoIt _moveIt = new(
            It.Inc<
                RectangleTag,
                MoveToEvent>());

        public RectangleMoveSystem(
            HudView hudView,
            IDataService dataService)
        {
            _hudView = hudView;
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _moveIt)
            {
                RectangleModule module = entity.GetRectangleModule().Value;
                module.Background.raycastTarget = false;
                MoveToEvent moveEvent = entity.GetMoveToEvent();
                Vector3 pos = moveEvent.Value;
                module.transform
                    .DOJump(pos, 300, 1, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        module.CanvasGroup.blocksRaycasts = true;
                        moveEvent.OnComplete.Invoke();
                        module.Background.raycastTarget = true;
                        _it.First().Entity.AddSaveDataEvent();
                        CheckGameOver();
                    });
            }
        }
        
        private void CheckGameOver()
        {
            RectangleModule rectangleModule = _rectIt.First().Entity.GetRectangleModule().Value;
            GameBoardModule gameBoardModule = _gameBoardIt.First().Entity.GetGameBoardModule().Value;
            float rectangleHeight = rectangleModule.TopTransform.position.y - rectangleModule.transform.position.y;

            if (rectangleModule.transform.position.y + rectangleHeight * 2 < gameBoardModule.TopTransform.position.y)
                return;
            
            _hudView.ShowView(UiViewId.GameOver);
            _hudView.HideView(UiViewId.Gameplay);
            _dataService.Clear(IdsConst.SaveData);
        }
    }
}