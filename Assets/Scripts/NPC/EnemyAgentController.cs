using Assets.Scripts.Managers;

namespace Assets.Scripts.NPC {
    public class EnemyAgentController : EnemyController {

        UnityEngine.AI.NavMeshAgent _agent;
        bool isFrozen;

        new void Start() {
            base.Start();
            _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            isFrozen = false;
        
        }

        void Update() {
            if(!isFrozen)
                _agent.SetDestination( PlayerManager.player.transform.position );
        }

        public override void Freeze() {
            base.Freeze();
            isFrozen = true;
            _agent.enabled = false;
        }

        public override void Unfreeze() {
            base.Unfreeze();
            isFrozen = false;
            _agent.enabled = true;
        }

    }
}
