using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Components.Game;
using Sources.BoundedContexts.Components.Rectangles;
using Sources.BoundedContexts.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using UnityEngine;

namespace Sources.BoundedContexts.Systems
{
    [EcsSystem(100)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class SaveGameSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                GameTag,
                SaveDataEvent>());

        [DI] private readonly ProtoItExc _saveIt = new(
            It.Inc<
                RectangleTag,
                InGameBoardComponent>(),
            It.Exc<InPoolComponent>());

        private readonly IDataService _dataService;

        public SaveGameSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity _ in _it)
            {
                List<RectangleSaveData> rectanglesData = new List<RectangleSaveData>();
                SaveData data = new SaveData()
                {
                    Rectangles = rectanglesData,
                    Id = IdsConst.SaveData,
                };
            
                foreach (ProtoEntity entity in _saveIt)
                {
                    Vector3 position = entity.GetRectangleModule().Value.transform.position;
                    
                    rectanglesData.Add(new RectangleSaveData()
                    {
                        IsLast = entity.HasLast(),
                        Color = entity.GetRectangleColor().Value,
                        PosX = position.x,
                        PosY = position.y,
                        PosZ = position.z,
                    });
                }
                
                _dataService.SaveData(data, IdsConst.SaveData);
            }
        }
    }
}