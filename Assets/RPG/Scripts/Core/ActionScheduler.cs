using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction; //�ثe���ʧ@
        public void StartAction(MonoBehaviour action) //�]��Fighter �M Mover ���~�ӤFMonoBehaviour
        {
            if (currentAction == action) return; //�p�G���O�@�˪��N�������X�A�p�G���@��(�����Faction)�N����U�h
            if (currentAction != null)
            {
                print("����" + currentAction);
            }
            
            currentAction = action;
        }
    }
}
