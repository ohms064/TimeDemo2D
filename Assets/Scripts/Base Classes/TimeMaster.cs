namespace Assets.Scripts.Base_Classes {
    public class TimeMaster {
        public Skill Freeze { get; set; }
        public Skill Haste { get; set; }
        public Skill Slow { get; set; }
        public Skill Rewind { get; set; }

        public TimeMaster() {
            Freeze = new Skill();
            Haste = new Skill();
            Slow = new Skill();
            Rewind = new Skill();
        }

        public TimeMaster(Skill freeze, Skill haste, Skill slow, Skill rewind ) {
            Freeze = freeze;
            Haste = haste;
            Slow = slow;
            Rewind = rewind;
        }
    }
}