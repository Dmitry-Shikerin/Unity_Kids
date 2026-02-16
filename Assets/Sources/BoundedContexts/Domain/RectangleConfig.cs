using Sirenix.OdinInspector;
using Sources.BoundedContexts.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Domain
{
    [CreateAssetMenu(fileName = nameof(RectangleConfig), menuName = "Configs/" + nameof(RectangleConfig), order = 51)]
    public class RectangleConfig : SerializedScriptableObject
    {
         [field: Required] [field: SerializeField] public RectangleModule View { get; private set; }
         [field: SerializeField] public ColorsSpritesDictionary Sprites { get; private set; }
    }
}