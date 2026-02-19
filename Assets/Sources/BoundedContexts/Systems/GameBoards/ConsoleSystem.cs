using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Events;
using Sources.BoundedContexts.Components.GameBoards;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Systems.GameBoards
{
    [EcsSystem(32)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class ConsoleSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                GameBoardTag,
                PrintEvent>()); 
        
        private readonly ILocalizationService _localizationService;
        private TweenerCore<Color, Color, ColorOptions> _tween;

        public ConsoleSystem(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                _tween?.Kill();
                GameBoardModule module = entity.GetGameBoardModule().Value;
                TMP_Text text = module.ConsoleText;
                text.color = Color.black;
                string key = entity.GetPrintEvent().Key;
                text.text = _localizationService.GetText(key);
                _tween = text.DOFade(0, 3).SetEase(Ease.InCubic);
            }
        }
    }
}