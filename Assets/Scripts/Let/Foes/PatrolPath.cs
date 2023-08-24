using UnityEngine;

namespace Let.Foes
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private Transform[] pathPoints;

        public Transform[] PathPoints
        {
            get => pathPoints.Length == 0 ? null : pathPoints;
            set => pathPoints = value;
        }
    }
}