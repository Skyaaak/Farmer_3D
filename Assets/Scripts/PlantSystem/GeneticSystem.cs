namespace PlantSystem
{
    public class GeneticSystem
    {
        public float MinTrunkHeight { get; }
        public float MaxTrunkHeight { get; }
        public float MinTrunkLenghtRatio { get; }
        public float MaxTrunkLenghtRatio { get; }
        public float MinNumberOfNode { get; }
        public float MaxNumberOfNode { get; }
        public float MinNumberOfStickByNode { get; }
        public float MaxNumberOfStickByNode { get; }
        public float MinAngleOfStick { get; }
        public float MaxAngleOfStick { get; }
        public float MaxStickLength { get; }
        public float MinStickLenghtRatio { get; }
        public float MaxStickLenghtRatio { get; }

        public GeneticSystem()
        {
            MinTrunkHeight = 0.5f;
            MaxTrunkHeight = 0.5f;
            MinTrunkLenghtRatio = 0.5f;
            MaxTrunkLenghtRatio = 0.5f;
            MinNumberOfNode = 0.5f;
            MaxNumberOfNode = 0.5f;
            MinNumberOfStickByNode = 0.5f;
            MaxNumberOfStickByNode = 0.5f;
            MinAngleOfStick = 0.5f;
            MaxAngleOfStick = 0.5f;
            MaxStickLength = 0.5f;
            MinStickLenghtRatio = 0.5f;
            MaxStickLenghtRatio = 0.5f;
        }
    }
}