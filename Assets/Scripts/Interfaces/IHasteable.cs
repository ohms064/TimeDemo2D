using System.Collections;

namespace Assets.Scripts.Interfaces {
    public interface IHasteable {
        void Haste();
        void Unhaste();
        IEnumerator HasteMode();
    }
}
