using System;
using System.Collections.Generic;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.BoundedContexts.Domain
{
    [Serializable]
    public struct SaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public List<RectangleSaveData> Rectangles { get; set; }
    }
}