using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Presentation
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _newGameButton2;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private List<UiView> _uiViews;
        
        private ISceneService _sceneService;
        private IDataService _dataService;

        [field: Required] [field: SerializeField] public Transform RectanglesParent { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink GameBoardLink { get; private set; }
        [field: Required] [field: SerializeField] public ScrollRect ScrollRect { get; private set; }
        [field: Required] [field: SerializeField] public Canvas MainCanvas { get; private set; }
        
        public ProtoEntity GameEntity { get; set; }
        
        public void Construct(ISceneService sceneService, IDataService dataService)
        {
            _sceneService = sceneService;
            _dataService = dataService;
        }

        private void Awake()
        {
            _newGameButton.onClick.AddListener(LoadNewGame);
            _newGameButton2.onClick.AddListener(LoadNewGame);
            _loadGameButton.onClick.AddListener(LoadSavedGame);
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveListener(LoadNewGame);
            _newGameButton2.onClick.RemoveListener(LoadNewGame);
            _loadGameButton.onClick.RemoveListener(LoadSavedGame);
        }

        private void LoadSavedGame() =>
            GameEntity.AddLoadGameEvent();

        private void LoadNewGame()
        {
            _dataService.Clear(IdsConst.SaveData);
            _sceneService.ChangeSceneAsync(IdsConst.Gameplay);
        }

        public void ShowView(params UiViewId[] views)
        {
            foreach (UiViewId id in views)
            {
                foreach (UiView view in _uiViews)
                {
                    if (view.UiViewId != id)
                        continue;
                    
                    view.Show();
                }
            }
        }
        
        public void HideView(params UiViewId[] views)
        {
            foreach (UiViewId id in views)
            {
                foreach (UiView view in _uiViews)
                {
                    if (view.UiViewId != id)
                        continue;
                    
                    view.Hide();
                }
            }
        }

        public UiView GetView(UiViewId id)
        {
            foreach (UiView view in _uiViews)
            {
                if (view.UiViewId != id)
                    continue;
                
                return view;
            }

            throw new Exception($"View with id {id} not found");
        }
    }
}