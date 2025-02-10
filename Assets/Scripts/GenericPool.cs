using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class GenericPool<T,K>
    {
        protected readonly Transform _root;
        protected readonly Queue<T> _poolQueue;

        protected readonly K _instance;
        protected readonly int _poolLength;

        public bool IsEmpty => _poolQueue != null ? _poolQueue.Count == 0 : true;

        public GenericPool(K instance, int count, Transform root)
        {
            _instance = instance;
            _root = root;
            _poolLength = count;

            _poolQueue = new Queue<T>();
        }

        public void CreatePool()
        {
            for(int i = 0; i < _poolLength; i++)
            {
                T instance = CreateInstance();
                Post(instance);
            }
        }

        public virtual T Get()
        {
            if (_poolQueue.TryDequeue(out T element) == false)
            {
                element = CreateInstance();
            }  

            return element;
        }

        public virtual void Post(T element)
        {
            _poolQueue.Enqueue(element);
        }

        protected abstract T CreateInstance();
    }
}