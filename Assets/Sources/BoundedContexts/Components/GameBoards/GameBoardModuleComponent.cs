using System;
using Leopotam.EcsProto.Unity;
using Sources.BoundedContexts.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Components.GameBoards
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct GameBoardModuleComponent
    {
        public GameBoardModule Value;
    }
}