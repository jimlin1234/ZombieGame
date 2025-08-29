using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    /// <summary>
    /// ���{���X�Ω�b�������ͦ����[�ƪ���A�T�O�o�Ǫ���b���������ɤ��|�Q�P���A�q�ӫO���C�����A���@�P�ʡC
    /// </summary>
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab; // ���[�ƪ��󪺹w�s��
        static bool hasSpawned = false; //static �Ω�T�O����u�ͦ��@��(�}��)

        private void Awake()
        {
            if(hasSpawned) return; // �p�G�w�g�ͦ��L�A�h������^

            SpawnPersistentObjects(); // �ͦ����[�ƪ���
            hasSpawned = true; // �аO���w�ͦ�

        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentsObject = Instantiate(persistentObjectPrefab); // ��Ҥƫ��[�ƪ���
            DontDestroyOnLoad(persistentsObject); // �]�m����ͦ���b���������ɤ��Q�P��
        }
    }
}
