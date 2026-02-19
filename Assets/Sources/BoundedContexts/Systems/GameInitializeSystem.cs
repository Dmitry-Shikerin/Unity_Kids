using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.Game;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Domain;
using Sources.BoundedContexts.Infrastructure.Factories;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Systems
{
    [EcsSystem(5)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class GameInitializeSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                GameTag,
                LoadGameEvent>());        
        [DI] private readonly ProtoItExc _lastIt = new(
            It.Inc<
                RectangleTag,
                LastComponent>(),
            It.Exc<
                RectangleTag,
                LastComponent,
            InGameBoardComponent>());        
        [DI] private readonly ProtoIt _rectIt = new(
            It.Inc<
                RectangleTag,
                InGameBoardComponent>());

        private readonly ISceneService _sceneService;
        private readonly IDataService _dataService;
        private readonly IAssetCollector _assetCollector;
        private readonly HudView _hudView;
        private readonly SlotEntityFactory _slotEntityFactory;
        private readonly GameEntityFactory _gameEntityFactory;
        private readonly GameBoardEntityFactory _gameBoardEntityFactory;
        private readonly RectangleEntityFactory _rectangleEntityFactory;

        public GameInitializeSystem(
            ISceneService sceneService,
            IDataService dataService,
            IAssetCollector assetCollector,
            HudView hudView,
            SlotEntityFactory slotEntityFactory,
            GameEntityFactory gameEntityFactory,
            GameBoardEntityFactory gameBoardEntityFactory,
            RectangleEntityFactory rectangleEntityFactory)
        {
            _sceneService = sceneService;
            _dataService = dataService;
            _assetCollector = assetCollector;
            _hudView = hudView;
            _slotEntityFactory = slotEntityFactory;
            _gameEntityFactory = gameEntityFactory;
            _gameBoardEntityFactory = gameBoardEntityFactory;
            _rectangleEntityFactory = rectangleEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoEntity gameEntity = _gameEntityFactory.Create();
            _hudView.GameEntity = gameEntity;
            _hudView.Construct(_sceneService, _dataService);
            _gameBoardEntityFactory.Create(_hudView.GameBoardLink);
            RectangleConfig config = _assetCollector.Get<RectangleConfig>();
            
            foreach (KeyValuePair<RectangleColors, Sprite> sprite in config.Sprites)
            {
                RectTransform scroll = _hudView.ScrollRect.content;
                ProtoEntity slotEntity = _slotEntityFactory.Create(sprite.Key);
                RectangleSlotModule slotModule = slotEntity.GetRectangleSlotModule().Value;
                slotModule.transform.SetParent(scroll);
                ProtoEntity rectangleEntity = _rectangleEntityFactory.Create(sprite.Key, slotEntity);
                slotEntity.AddChildRectangle(rectangleEntity);
                RectangleModule rectangleModule = rectangleEntity.GetRectangleModule().Value;
                rectangleModule.transform.SetParent(slotModule.transform);
                rectangleModule.transform.localPosition = Vector3.zero;
            }

            if (_dataService.HasKey(IdsConst.SaveData) == false)
                return;
            
            _hudView.HideView(UiViewId.GameOver, UiViewId.Gameplay);
            _hudView.ShowView(UiViewId.LoadGame);
        }

        public void Run()
        {
            foreach (ProtoEntity _ in _it)
            {
                if (_dataService.HasKey(IdsConst.SaveData) == false)
                    throw new Exception("Save data not found");
                
                SaveData data = _dataService.LoadData<SaveData>(IdsConst.SaveData);
                
                foreach (RectangleSaveData rectangleData in data.Rectangles)
                {
                    ProtoEntity rectangleEntity = _rectangleEntityFactory.Create(rectangleData.Color);
                    RectangleModule module = rectangleEntity.GetRectangleModule().Value;
                    module.transform.SetParent(_hudView.GameBoardLink.transform);
                    module.transform.position = new Vector3(rectangleData.PosX, rectangleData.PosY);
                    rectangleEntity.AddInGameBoard();

                    if (rectangleData.IsLast)
                        rectangleEntity.AddLast();
                }
                
                _hudView.HideView(UiViewId.LoadGame);
                _hudView.ShowView(UiViewId.Gameplay);
            }

            int len = _lastIt.Len();
            
            if (_lastIt.Len() > 0)
                return;

            float lastPos = 0;
            ProtoEntity temp = default;
            int index = 0;
            
            foreach (ProtoEntity entity in _rectIt)
            {
                index++;

                if (index == 1)
                {
                    lastPos = entity.GetRectangleModule().Value.transform.position.y;
                    temp = entity;
                    continue;
                }

                float position = entity.GetRectangleModule().Value.transform.position.y;
                
                if (position > lastPos)
                {
                    lastPos = position;
                    temp = entity;
                }

                if (index == len)
                    temp.AddLast();
            }
        }
    }
}