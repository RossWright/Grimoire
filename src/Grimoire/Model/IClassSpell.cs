namespace Grimoire.Model
{
    public interface IClassSpell : ISpell
    {
        int Level { get; }
        int Cost { get; }
        int Max { get; }
        int Uses { get; }
        UsesPer? UsesPer { get; }
        int ChargeTimes { get; }
    }

    public enum UsesPer { Life, Refresh, Unlimited, Balls }
}
