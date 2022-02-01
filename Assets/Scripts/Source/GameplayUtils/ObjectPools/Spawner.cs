using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        #region Fields
        [SerializeField] private SpawnerSource[] sources;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private bool initializeOnAwake;
        
        private Dictionary<string, List<GameObject>> localStock = new Dictionary<string,List<GameObject>>();
        #endregion

        private void Awake()
        {
            if (initializeOnAwake)
            {
                FillLocalStock();
            }
        }

        private void FillLocalStock()
        {
            foreach (KeyValuePair<string,List<GameObject>> pair in localStock)
            {
                pair.Value.Clear();
            }
            
            localStock.Clear();
            
            foreach (SpawnerSource source in sources)
            {
                if (source.shared) continue;
                if (!localStock.ContainsKey(source.nameId))
                {
                    List<GameObject> alloc = new List<GameObject>();
                    for (int i = 0; i < source.startCount; i++)
                    {
                        alloc.Add(Allocate(source.prefab));
                    }
                    localStock.Add(source.nameId, alloc);
                }
            }
        }

        private GameObject Allocate(GameObject prefab)
        {
            GameObject alloc = Instantiate(prefab, startPosition, Quaternion.identity);
            alloc.SetActive(false);
            return alloc;
        }

        public GameObject SpawnBase(string nameId, Vector3 position, Quaternion rotation, Vector3 scale,
            Transform parent = null)
        {
            if (!localStock.ContainsKey(nameId)) return null;
            List<GameObject> pointedStock = localStock[nameId];
            try
            {
                GameObject src = sources.First(x => x.nameId == nameId).prefab;
                if (pointedStock == null)
                {
                    pointedStock = new List<GameObject>();
                }


                GameObject ret = pointedStock.FirstOrDefault(x => !x.activeSelf);
                if (ret == null)
                {
                    ret = Allocate(src);
                    pointedStock.Add(ret);
                }

                Transform retTransform = ret.transform;
                retTransform.position = position;
                retTransform.rotation = rotation;
                retTransform.localScale = scale;
                retTransform.parent = parent;

                return ret;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TComponent Spawn<TComponent>(string nameId, Vector3 position, Quaternion rotation, Vector3 scale,
            Transform parent = null) where TComponent : Component
        {
            if (!localStock.ContainsKey(nameId)) return null;
            List<GameObject> pointedStock = localStock[nameId];
            return Spawn<TComponent>(nameId, position, rotation, scale, parent)?.GetComponent<TComponent>();
        }
    }
}
