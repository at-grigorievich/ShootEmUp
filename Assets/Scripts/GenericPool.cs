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

        public bool IsEmpty => _poolQueue != null ? _poolQueue.Count == 0 : true;

        public GenericPool(T instance, int count, Transform root)
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
                T instance = GameObject.Instantiate(_instance, _root);
                Post(instance);
            }
        }

        public virtual T Get()
        {
            if (_poolQueue.TryDequeue(out T element) == false)
            {
                element = GameObject.Instantiate(_instance);
            }
            
            element.transform.SetParent(null);

            return element;
        }

        public virtual void Post(T element)
        {
            element.transform.SetParent(_root);
            _poolQueue.Enqueue(element);
        }
    }
}