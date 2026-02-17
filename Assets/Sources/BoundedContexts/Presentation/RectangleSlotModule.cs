using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;

namespace Sources.BoundedContexts.Presentation
{
    public class RectangleSlotModule : EntityModule
    {
        [field: SerializeField] public EntityLink RectangleLink { get; private set; }

        public void FillSlot()
        {
            //Debug.Log($"Заполнить слот");
        }
    }
}