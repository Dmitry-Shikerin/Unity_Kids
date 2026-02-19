using System.Collections.Generic;
using System.Linq;
using Sources.BoundedContexts.Domain;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Common.Domain.Constants
{
    public class IdsConst
    {
        //Scenes
        public const string Gameplay = "Gameplay";
        
        //Gameplay
        public const string SaveData = "SaveData";
        
        private static Dictionary<string, List<string>> _cachedIdsByType = new ();

        public static IReadOnlyDictionary<string, EntityData> AllIds { get; } = new Dictionary<string, EntityData>()
        {
            { SaveData, new EntityData(SaveData, typeof(SaveData), true) }
        };

        public static IReadOnlyList<string> GetIds<T>() 
            where T : struct, IEntitySaveData
        {
            string id = typeof(T).Name;
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Values
                .Where(data => data.Type == typeof(T))
                .Select(data => data.ID)
                .ToList();
            
            return _cachedIdsByType[id];
        }

        public static IReadOnlyList<string> GetDeleteIds()
        {
            string id = "Deleted";
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Values
                .Where(data => data.IsDeleted)
                .Select(data => data.ID)
                .ToList();
            
            return _cachedIdsByType[id];
        }
        
        public static IReadOnlyList<string> GetAll()
        {
            string id = "All";
            
            if (_cachedIdsByType.TryGetValue(id, out List<string> ids))
                return ids;
            
            _cachedIdsByType[id] = AllIds.Keys.ToList();
            
            return _cachedIdsByType[id];
        }
    }
}