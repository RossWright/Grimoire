namespace Grimoire.Model
{
    public interface ISpell
    {
        string Name { get; }
        SpellSchool? School { get; }
        SpellType? Type { get; }
        SpellRange? Range { get; }
        string Incant { get; }
        int IncantTimes { get; }

        string Materials { get; }
        string Effects { get; }
        string Limits { get; }
        string Notes { get; }
    }

    public enum SpellSchool { Command, Death, Flame, Neutral, Protection, Sorcery, Spirit, Subdual }
    public enum SpellType { Enchantment, Neutral, MagicBall, MetaMagic, Verbal }
    public enum SpellRange { Self, Ball, Touch, TouchOthers, SelfTouchOthers, _20ft, _50ft }
}
