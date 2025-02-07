using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : GenericPool<BulletView>
    {
        public BulletPool(BulletView instance, int count, Transform root) : base(instance, count, root) { }

        public override BulletView Get()
        {
            var result = base.Get();
            result.SetActive(true);
            return result;
        }

        public override void Post(BulletView element)
        {
            base.Post(element);
            element.SetActive(false);
        }
    }
}