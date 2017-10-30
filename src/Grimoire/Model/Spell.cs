namespace Grimoire.Model
{
    public class Spell : ISpell
    {
        public string Name { get; set; }
        public SpellSchool? School { get; set; }
        public SpellType? Type { get; set; }
        public SpellRange? Range { get; set; }
        public string Incant { get; set; }
        public int IncantTimes { get; set; }

        public string Materials { get; set; }
        public string Effects { get; set; }
        public string Limits { get; set; }
        public string Notes { get; set; }
    }
}
