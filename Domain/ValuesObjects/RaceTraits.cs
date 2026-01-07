namespace LostColonyManager.Domain.ValuesObjects
{
    public readonly record struct RaceTraits
    {
        public int Power { get; init; }
        public int Technology { get; init; }
        public int Diplomacy { get; init; }
        public int Culture { get; init; }
    }
}
