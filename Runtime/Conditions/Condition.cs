using UnityEngine;

namespace Khora.FSM
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool CheckCondition();
    }
}

