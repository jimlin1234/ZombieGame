using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    /// <summary>
    /// ���{���X�����a�i�J Portal ��A�D�P�B��������w�����A�ñN���a���Ⲿ�ʨ�s������ Portal �ͦ��I�A�T�O����L�{�y�Z�B���a��m���T�C
    /// </summary>
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
            //�e����...
            DontDestroyOnLoad(gameObject);//���������ɤ��R������(���J�f)�A�_�h���J�s�����ᦹ����|�����N�L�k�q������a�J���
            yield return SceneManager.LoadSceneAsync(sceneToLoad); //�D�P�B���J�s����

            //�����...
            Portal otherPortal = GetOtherPortal(); //�������J��A�M��s���������t�@�� Portal
            UpdatePlayer(otherPortal); //��s���a��m��s������ Portal �ͦ��I

            Destroy(gameObject);//����������ña�J��ƫ�R������(���J�f)
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
