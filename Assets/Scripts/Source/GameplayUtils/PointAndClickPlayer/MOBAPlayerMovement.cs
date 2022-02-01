using UnityEngine;
using UnityEngine.AI;

namespace MOBAGame.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MOBAPlayerMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask walkableSurfaces;
        [SerializeField] private float movementSpeed;
        [SerializeField] private GameObject indicatorPrefab;

        private NavMeshAgent agent;
        private bool previousHasPath;
        private ParticleSystem indicator;
        private bool canMove = true;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = movementSpeed;
            indicator = Instantiate(indicatorPrefab, transform.position, indicatorPrefab.transform.rotation, null)
                .GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (!previousHasPath)
            {
                if (agent.hasPath)
                {
                    SendMessage("OnStartedMoving", SendMessageOptions.DontRequireReceiver);
                }
            }

            if (previousHasPath)
            {
                if (!agent.hasPath)
                {
                    SendMessage("OnStopped", SendMessageOptions.DontRequireReceiver);
                }
            }

            previousHasPath = agent.hasPath;
        }

        public void Input_Click(InputButtonInfo info)
        {
            if (info.ButtonState == ButtonState.Press)
            {
                ClickDestination(info.GetData<Vector3>());
            }
        }

        public void OnAttack(MOBAPlayerAttackHandler.AttackInfo info)
        {
            Freeze();
            Invoke(nameof(UnFreeze), info.attackDuration);
        }

        private void Freeze()
        {
            agent.destination = transform.position;
            canMove = false;
        }

        private void UnFreeze()
        {
            agent.isStopped = false;
            canMove = true;
        }

        private void ClickDestination(Vector2 mousePos)
        {
            if (!canMove) return;
            Ray r = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(r.origin, r.direction, Color.cyan, 5f);
            if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity,
                    walkableSurfaces))
            {
                agent.speed = movementSpeed;
                agent.destination = hit.point;
                PlaceIndicator(hit.point);
            }
        }

        private void PlaceIndicator(Vector3 position)
        {
            indicator.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            indicator.transform.position = position;
            indicator.Play(true);
        }
    }
}
