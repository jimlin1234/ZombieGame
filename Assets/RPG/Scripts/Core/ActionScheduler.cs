using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction; //目前的動作
        public void StartAction(MonoBehaviour action) //因為Fighter 和 Mover 皆繼承了MonoBehaviour
        {
            if (currentAction == action) return; //如果都是一樣的就直接跳出，如果不一樣(切換了action)就執行下去
            if (currentAction != null)
            {
                print("取消" + currentAction);
            }
            
            currentAction = action;
        }
    }
}
