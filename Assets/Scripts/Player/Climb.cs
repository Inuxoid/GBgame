using System.Collections;
using StateMachines.PlayerSM;
using UnityEngine;

namespace Player
{
    public class Climb : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform player;
        [SerializeField] private float maxOffsetToStartClimb = 1.5f;
        [SerializeField] private float offsetWhileClimb = 1f;
        [SerializeField] private bool startedClimbing;
        [SerializeField] private AnimationCurve climbCurve;
        [SerializeField] private PlayerSM sm;

        private static readonly int IsClimbing = Animator.StringToHash("isClimbing");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = transform.parent;
            sm = GetComponentInParent<PlayerSM>();
        }

        public bool StartedClimbing
        {
            get => startedClimbing;
            private set => startedClimbing = value;
        }

        public bool IsCanClimb(Vector3 firstPoint) =>
          StartedClimbing == false && Vector3.Distance(player.transform.position, firstPoint) < maxOffsetToStartClimb;

        public void StartClimb(Vector3[] points)
        {
            sm.cachedInput = 0f;
            StartCoroutine(ClimbTimer(points));
        }

        private IEnumerator ClimbTimer(Vector3[] points)
        {
            StartedClimbing = true;
            //sm.rb.useGravity = false;
            animator.SetBool(IsClimbing, true);
            int index = 0;
            float climbTime = 0f;
            while (StartedClimbing)
            {
                if (IsReachPoint(points[index]))
                {
                    if (index == points.Length - 1)
                        StartedClimbing = false;
                    else
                        index++;
                }
                else
                    ClimbTo(points[index], climbCurve.Evaluate(climbTime));

                yield return null;

                climbTime += Time.deltaTime;
            }
            animator.SetBool(IsClimbing, false);
            //sm.rb.useGravity = true;
        }

        private bool IsReachPoint(Vector3 point) =>
          Vector3.Distance(player.transform.position, point) < offsetWhileClimb;

        private void ClimbTo(Vector3 to, float currentTime)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, to, currentTime);
        }
    }
}