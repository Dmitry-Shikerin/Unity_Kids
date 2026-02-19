using System;

namespace Sources.BoundedContexts.Domain
{
    [Serializable]
    public struct RectangleSaveData
    {
        public bool IsLast { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public RectangleColors Color { get; set; }
    }
}