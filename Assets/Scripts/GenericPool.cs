using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GenericPool<T> where T: MonoBehaviour
    {
        private readonly Transform _root;
        private readonly Queue<T> _poolQueue;

        private readonly T _instance;
        private readonly int _poolLength;

        public GenericPool(T instance, int count, Transform root)
        {
            _instance = instance;
            _root = root;

            _poolQueue = new Queue<T>();
        }

        public void CreatePool()
        {
            for(int i = 0; i < _poolLength; i++)
            {
                T instance = GameObject.Instantiate(_instance, _root);
                _poolQueue.Enqueue(instance);
            }
        }

        public T GetEnemy()
        {
            if (_poolQueue.TryDequeue(out T element)) return null;
            
            element.transform.SetParent(null);

            return element;
        }
    }
}