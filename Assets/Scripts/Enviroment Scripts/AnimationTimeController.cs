using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enviroment_Scripts {
    public class AnimationTimeController : MonoBehaviour, IFreezable {
        private Animator _animator;

        // Use this for initialization
        void Start () {
            _animator = GetComponent<Animator>();
        }

        public void Freeze() {
            _animator.speed = 0.0f;
        }

        public void FrozenRotation( float rotation ) {
            return;
        }

        public void Unfreeze() {
            _animator.speed = 1.0f;
        }
    }
}
