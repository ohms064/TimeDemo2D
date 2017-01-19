using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Tools {
    public enum GroundedState {
        GROUNDED,
        ON_AIR,
        SEMI_GROUNDED
    }

    public enum GrabStatus {
        GRABING_OBJECT,
        ROTATING_OBJECT,
        HANDS_FREE
    }
}