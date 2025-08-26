using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    /// <summary>
    /// 此程式碼讓玩家進入 Portal 後，非同步切換到指定場景，並將玩家角色移動到新場景的 Portal 生成點，確保轉場過程流暢且玩家位置正確。
    /// </summary>
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1; //scene index
        [SerializeField] Transform spawnPoint; //轉場後player生成點
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            //前場景...
            DontDestroyOnLoad(gameObject);//切換場景時不刪除物件(此入口)，否則載入新場景後此物件會消失就無法從此物件帶入資料
            yield return SceneManager.LoadSceneAsync(sceneToLoad); //非同步載入新場景

            //後場景...
            Portal otherPortal = GetOtherPortal(); //場景載入後，尋找新場景中的另一個 Portal
            UpdatePlayer(otherPortal); //更新玩家位置到新場景的 Portal 生成點

            Destroy(gameObject);//切換場景後並帶入資料後刪除物件(此入口)
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal() 
        {
           foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                return portal;
            }
           return null;
        }
    }
}
