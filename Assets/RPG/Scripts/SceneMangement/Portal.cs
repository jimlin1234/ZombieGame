using RPG.SceneMangement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneMangement
{
    /// <summary>
    /// 此程式碼讓玩家進入 Portal 後，非同步切換到指定場景，並將玩家角色移動到新場景的 Portal 生成點，確保轉場過程流暢且玩家位置正確。
    /// 幫各傳送門設定相對的識別符號(destination)，以便在新場景中找到對應的傳送門。
    /// </summary>
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier //枚舉，用於識別不同的傳送門
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1; //scene index 場景識別符
        [SerializeField] Transform spawnPoint; //轉場後player生成點
        [SerializeField] DestinationIdentifier destination; //傳送門識別符

        [SerializeField] float fadeOutTime = 2.0f;
        [SerializeField] float fadeInTime = 1.0f;
        [SerializeField] float fadeWaitTime = 0.5f;
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
            Fader fader = FindObjectOfType<Fader>();//取得Fader物件
            yield return fader.FadeOut(fadeOutTime); //淡出螢幕

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save(); //切換場景前先存檔

            yield return SceneManager.LoadSceneAsync(sceneToLoad); //非同步載入新場景
            
            savingWrapper.Load(); //載入新場景後讀取存檔

            //後場景...
            Portal otherPortal = GetOtherPortal(); //場景載入後，尋找新場景中的另一個 Portal
            UpdatePlayer(otherPortal); //更新玩家位置到新場景的 Portal 生成點

            yield return new WaitForSeconds(fadeWaitTime); //等待一段時間
            yield return fader.FadeIn(fadeInTime); //淡入螢幕
            Destroy(gameObject);//切換場景後並帶入資料後刪除物件(此入口)
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false; //關閉NavMeshAgent，避免直接設定位置時產生衝突
            player.transform.position = otherPortal.spawnPoint.position; 
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true; //重新啟用NavMeshAgent
        }

        private Portal GetOtherPortal() 
        {
           foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue; //假如是自己就繼續找
                if (portal.destination != this.destination) continue; //假如傳送門識別符不一致就繼續找
                return portal;
            }
           return null;
        }
    }
}
