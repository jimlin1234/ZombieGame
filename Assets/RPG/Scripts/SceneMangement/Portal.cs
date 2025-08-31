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
    /// ���{���X�����a�i�J Portal ��A�D�P�B��������w�����A�ñN���a���Ⲿ�ʨ�s������ Portal �ͦ��I�A�T�O����L�{�y�Z�B���a��m���T�C
    /// ���U�ǰe���]�w�۹諸�ѧO�Ÿ�(destination)�A�H�K�b�s���������������ǰe���C
    /// </summary>
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier //�T�|�A�Ω��ѧO���P���ǰe��
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1; //scene index �����ѧO��
        [SerializeField] Transform spawnPoint; //�����player�ͦ��I
        [SerializeField] DestinationIdentifier destination; //�ǰe���ѧO��

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
            //�e����...
            DontDestroyOnLoad(gameObject);//���������ɤ��R������(���J�f)�A�_�h���J�s�����ᦹ����|�����N�L�k�q������a�J���
            Fader fader = FindObjectOfType<Fader>();//���oFader����
            yield return fader.FadeOut(fadeOutTime); //�H�X�ù�

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save(); //���������e���s��

            yield return SceneManager.LoadSceneAsync(sceneToLoad); //�D�P�B���J�s����
            
            savingWrapper.Load(); //���J�s������Ū���s��

            //�����...
            Portal otherPortal = GetOtherPortal(); //�������J��A�M��s���������t�@�� Portal
            UpdatePlayer(otherPortal); //��s���a��m��s������ Portal �ͦ��I

            yield return new WaitForSeconds(fadeWaitTime); //���ݤ@�q�ɶ�
            yield return fader.FadeIn(fadeInTime); //�H�J�ù�
            Destroy(gameObject);//����������ña�J��ƫ�R������(���J�f)
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false; //����NavMeshAgent�A�קK�����]�w��m�ɲ��ͽĬ�
            player.transform.position = otherPortal.spawnPoint.position; 
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true; //���s�ҥ�NavMeshAgent
        }

        private Portal GetOtherPortal() 
        {
           foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue; //���p�O�ۤv�N�~���
                if (portal.destination != this.destination) continue; //���p�ǰe���ѧO�Ť��@�P�N�~���
                return portal;
            }
           return null;
        }
    }
}
