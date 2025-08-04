using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public interface IAction
    {
        public void Cancel(); //Mover 及 Fighter都有繼承，所以都要實作此方法
    }
}
