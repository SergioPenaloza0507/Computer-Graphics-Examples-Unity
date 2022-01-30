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
        
        private Dictionary<string, (GameObject,List<GameObject>)> localStock = new Dictionary<string, (GameObject,List<GameObject>)>();
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
            foreach (KeyValuePair<string,(GameObject,List<GameObject>)> pair in localStock)
            {
                pair.Value.Item2?.Clear();
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
                    localStock.Add(source.nameId, (source.prefab, alloc));
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
            (GameObject, List<GameObject>) pointedStock = localStock[nameId];
            try
            {
                GameObject src = pointedStock.Item1;
                if (pointedStock.Item2 == null)
                {
                    pointedStock.Item2 = new List<GameObject>();
                }


                GameObject ret = pointedStock.Item2.FirstOrDefault(x => !x.activeSelf);
                if (ret == null)
                {
                    ret = Allocate(src);
                    pointedStock.Item2.Add(ret);
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
            (GameObject, List<GameObject>) pointedStock = localStock[nameId];
            if (pointedStock.Item1.TryGetComponent(out TComponent _))
            {
                return Spawn<TComponent>(nameId, position, rotation, scale, parent)?.GetComponent<TComponent>();
            }

            return null;
        }
    }
}
