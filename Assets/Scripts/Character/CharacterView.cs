using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private HitPointsComponentData hitPointsComponentData;

        public HitPointsComponent HitPointsComponent { get; private set; }

        private void Awake()
        {
            HitPointsComponent = hitPointsComponentData.Create();
        }
    }
}