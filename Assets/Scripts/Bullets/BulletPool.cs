using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : GenericPool<BulletView, BulletView>
    {
        public BulletPool(BulletView instance, int count, Transform root) : base(instance, count, root) { }

        public override BulletView Get()
        {
            var element = base.Get();

            element.transform.SetParent(null);
            element.SetActive(true);

            return element;
        }

        public override void Post(BulletView element)
        {
            base.Post(element);

            element.transform.SetParent(_root);
            element.SetActive(false);
        }

        protected override BulletView CreateInstance()
        {
            return GameObject.Instantiate(_instance, _root);
        }
    }
}