namespace Grimoire.Model
{
    public class ClassSpell : IClassSpell
    {
        public ClassSpell(ISpell spell)
        {
            _spell = spell;
        }
        private readonly ISpell _spell;

        public string Name => _spell.Name;
        public SpellSchool? School => _spell.School;
        public SpellType? Type => _spell.Type;
        public SpellRange? Range => _spell.Range;
        public string Incant => _spell.Incant;
        public int IncantTimes => _spell.IncantTimes;
        public string Materials => _spell.Materials;
        public string Effects => _spell.Effects;
        public string Limits => _spell.Limits;
        public string Notes => _spell.Notes;

        public int Level { get; set; }
        public int Cost { get; set; }
        public int Max { get; set; }
        public int Uses { get; set; }
        public UsesPer? UsesPer { get; set; }
        public int ChargeTimes { get; set; }
    }
}
