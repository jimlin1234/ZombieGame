using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    /// <summary>
    /// 此程式碼用於在場景中生成持久化物件，確保這些物件在場景切換時不會被銷毀，從而保持遊戲狀態的一致性。
    /// </summary>
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab; // 持久化物件的預製體
        static bool hasSpawned = false; //static 用於確保物件只生成一次(開關)

        private void Awake()
        {
            if(hasSpawned) return; // 如果已經生成過，則直接返回

            SpawnPersistentObjects(); // 生成持久化物件
            hasSpawned = true; // 標記為已生成

        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentsObject = Instantiate(persistentObjectPrefab); // 實例化持久化物件
            DontDestroyOnLoad(persistentsObject); // 設置物件生成後在場景切換時不被銷毀
        }
    }
}
