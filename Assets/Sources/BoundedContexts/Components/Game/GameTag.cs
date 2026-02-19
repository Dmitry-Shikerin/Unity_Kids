using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.BoundedContexts.Components.Game
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Rectangle)]
    [Aspect(AspectName.Game)]
    public struct GameTag
    {
    }
}