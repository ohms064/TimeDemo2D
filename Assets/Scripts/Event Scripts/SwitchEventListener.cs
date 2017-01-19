using UnityEngine;

namespace Assets.Scripts.Event_Scripts {
    public abstract class SwitchEventListener : MonoBehaviour {

        public abstract void onActivated(Switch fromWho);
        public abstract void OnDeactivated( Switch fromWho );
    }
}
