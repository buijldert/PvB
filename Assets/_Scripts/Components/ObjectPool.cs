using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This class is a repository of commonly used prefabs.
    /// </summary>
    [AddComponentMenu("Gameplay/ObjectPool")]
    public class ObjectPool : MonoBehaviour
    {

        public static ObjectPool Instance { get; private set; }

        #region member
        /// <summary>
        /// Member class for a prefab entered into the object pool
        /// </summary>
        /// ObjectPool.instance.GetObjectForType("Cube", true/false); <---use to instantiate
        /// ObjectPool.instance.PoolObject(gameObject) <---use to pool;
        [Serializable]
        public class ObjectPoolEntry
        {
            /// <summary>
            /// the object to pre instantiate
            /// </summary>
            public GameObject prefab;

            /// <summary>
            /// quantity of object to pre-instantiate
            /// </summary>
            public int count;
            public bool startActive;
        }
        #endregion

        /// <summary>
        /// The object prefabs which the pool can handle
        /// by The amount of objects of each type to buffer.
        /// </summary>
        [SerializeField]
        private ObjectPoolEntry[] entries;

        /// <summary>
        /// The pooled objects currently available.
        /// Indexed by the index of the objectPrefabs
        /// </summary>
        [HideInInspector]
        private List<GameObject>[] pool;

        /// <summary>
        /// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
        /// </summary>
        private GameObject containerObject;

        private static ObjectPool instance = null;


        private void OnEnable()
        {
            Instance = this;
        }

        // Use this for initialization
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            containerObject = gameObject;

            //Loop through the object prefabs and make a new list for each one.
            //We do this because the pool can only support prefabs set to it in the editor,
            //so we can assume the lists of pooled objects are in the same order as object prefabs in the array
            pool = new List<GameObject>[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                var objectPrefab = entries[i];

                //create the repository
                pool[i] = new List<GameObject>();

                //fill it
                for (int n = 0; n < objectPrefab.count; n++)
                {

                    var newObj = Instantiate(objectPrefab.prefab) as GameObject;

                    newObj.name = objectPrefab.prefab.name;

                    PoolObject(newObj);
                }
            }
        }



        /// <summary>
        /// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
        /// then null will be returned.
        /// </summary>
        /// <returns>
        /// The object for type.
        /// </returns>
        /// <param name='_objectType'>
        /// Object type.
        /// </param>
        /// <param name='_onlyPooled'>
        /// If true, it will only return an object if there is one currently pooled.
        /// </param>
        public GameObject GetObjectForType(string _objectType, bool _onlyPooled)
        {

            for (int i = 0; i < entries.Length; i++)
            {
                var prefab = entries[i].prefab;

                if (prefab.name != _objectType)
                {
                    continue;
                }  

                if (pool[i].Count > 0)
                {

                    GameObject pooledObject = pool[i][0];

                    pool[i].RemoveAt(0);

                    pooledObject.transform.parent = null;

                    pooledObject.SetActive(entries[i].startActive);

                    return pooledObject;
                }
                if (!_onlyPooled)
                {
                    GameObject newObj = Instantiate(entries[i].prefab) as GameObject;
                    newObj.name = entries[i].prefab.name;
                    return newObj;
                }
            }

            //If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
            return null;
        }

        /// <summary>
        /// Pools the object specified.  Will not be pooled if there is no prefab of that type.
        /// </summary>
        /// <param name='_obj'>
        /// Object to be pooled.
        /// </param>
        public void PoolObject(GameObject _obj)
        {

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].prefab.name != _obj.name)
                {
                    continue;
                }

                pool[i].Add(_obj);

                _obj.transform.SetParent(containerObject.transform, false);

                _obj.SetActive(false);

                return;
            }
        }
    }
}