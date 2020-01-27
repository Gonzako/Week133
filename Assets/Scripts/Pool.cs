using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GonzakoUtils.DataStructures
{

    /// <summary>
    /// Generalized pool data stucture 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> where T : Object
    {
        #region PublicFields
        /// <summary>
        /// Base object used for the pooling
        /// </summary>
        public T Blueprint { get; set;}
        public Transform parent { get; set; }
        /// <summary>
        /// Return the number of items on the pool
        /// </summary>
        public int Count => avaliableItems.Count;

        /// <summary>
        /// Returns <see langword="true"/> if the heap is empty, otherwise
        /// <see langword="false"/>.
        /// </summary>
        public bool Empty => avaliableItems.Count == 0; 

        #endregion



        #region Events
        public event Action<T> OnPush = delegate { };
        public event Action<T> OnPop = delegate { };
        #endregion


        //Objects stored in the pool, might switch to Stack but i like everyone to feel included
        private readonly Queue<T> avaliableItems;


        #region publicMethods
        /// <summary>
        /// Straight Up Queue.Dequeue
        /// </summary>
        /// <returns></returns>
        public T getNextObj()
        {
            if (avaliableItems.Count < 1)
            {
                Populate(5);
                OnPop.Invoke(avaliableItems.Peek());
                return avaliableItems.Dequeue(); 
                
            }
            else
            {
                OnPop.Invoke(avaliableItems.Peek());
                return avaliableItems.Dequeue();
            }
        }

        public bool enPool(T obj)
        {
            if (obj != null)
            {
                OnPush.Invoke(obj);
                avaliableItems.Enqueue(obj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<T> getAllpooledItems()
        {
            return avaliableItems.ToList();
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates empty <see cref="Pool{T}"/> of type <typeparamref name="T"/>
        /// </summary>
        public Pool() : this(count: 0) { }



        /// <summary>
        /// Creates pool of type <typeparamref name="T"/> and populates it with <paramref name="count"/>  objects
        /// </summary>
        /// <param name="count">number of items at start</param>
        public Pool(int count) : this(count, default(T)) { }

        /// <summary>
        /// Creates a populated pool of type <typeparamref name="T"/> that are all identical copies of 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="clone"></param>
        public Pool(int count, T clone)
        {
            avaliableItems = new Queue<T>();
            Blueprint = clone;


            Populate(count);
        }

        public Pool(int count, T clone, Transform parent = null)
        {
            avaliableItems = new Queue<T>();
            Blueprint = clone;

            this.parent = parent;

            Populate(Blueprint, count, parent);
        }
        #endregion

        private bool Populate(int count)
        {

            return Populate(Blueprint, count);
        }

        private bool Populate(T blueprint, int count, Transform transform = null)
        {
            parent = this.parent;
            T startObj = CreateObject(blueprint);

            if (startObj == null) { throw new Exception(); }

            avaliableItems.Enqueue(startObj);
            OnPush.Invoke(startObj);

            for(int i = 1; i < count; i++)
            {
                startObj = CreateObject(blueprint);
                avaliableItems.Enqueue(startObj);
                OnPush.Invoke(startObj);
            }
            return true;
        }

        private T CreateObject( T blueprint)
        {
            return Object.Instantiate(blueprint as Object, parent) as T;

        }


    }
}
