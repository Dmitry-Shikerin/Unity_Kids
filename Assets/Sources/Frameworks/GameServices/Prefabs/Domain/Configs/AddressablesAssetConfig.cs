using Sirenix.OdinInspector;
using Sources.BoundedContexts.Domain;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sources.Frameworks.GameServices.Prefabs.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(AddressablesAssetConfig), menuName = "Configs/" + nameof(AddressablesAssetConfig), order = 51)]
    public class AddressablesAssetConfig  : ScriptableObject
    {
        [field: Title("Configs")]
        [field: SerializeField] public AssetReferenceT<RectangleConfig> RectangleConfig { get; private set; }
        
        [field: Title("Prefabs")]
        [field: SerializeField] public AssetReferenceT<GameObject> Rectangle { get; private set; }
    }
}