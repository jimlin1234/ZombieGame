using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1; //scene index
        [SerializeField] Transform spawnPoint; //�����player�ͦ��I
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);//���������ɤ��R������(���J�f)
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);//����������R������(���J�f)
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
