using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; //�ثe���ʧ@
        public void StartAction(IAction action) //�]��Fighter �M Mover ���~�ӤFIAction  ����
        {
            //print(action);
            if (currentAction == action) return; //�p�G���O�@�˪��N�������X�A�p�G���@��(�����Faction)�N����U�h
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
            
        }
    }
}
