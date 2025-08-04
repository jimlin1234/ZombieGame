using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; //目前的動作
        public void StartAction(IAction action) //因為Fighter 和 Mover 皆繼承了IAction  介面
        {
            //print(action);
            if (currentAction == action) return; //如果都是一樣的就直接跳出，如果不一樣(切換了action)就執行下去
            if (currentAction != null)
            {
                currentAction.Cancel(); //Mover 及 Fighter都有繼承IAction 介面，所以都要實作此方法，依照傳入的IAction物件不同，去執行自己的Cancel方法
            }
            currentAction = action;
            
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
