using Codice.CM.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Khora.FSM
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public class Transition
        {
            public string transitionName;
            
            [Tooltip("If true, this transition will fire.")]
            public Condition[] conditions;   // could be your own serializable wrapper
            public int nextState;   // index into States[]
        }
        [System.Serializable]
        public class StateData
        {
            public string name;
            public UnityEvent OnEnter;
            public UnityEvent OnUpdate;
            public UnityEvent OnExit;
            public Transition[] transitions;
        }
        [Tooltip("Define all your states here")]
        public StateData[] States;
        [Tooltip("Index of the state to start in")]
        public int initialStateIndex = 0;
        // --- private runtime ---
        StateData _current;
        int _currentIndex;
        void Start()
        {
            ChangeState(initialStateIndex);
        }
        void Update()
        {
            if (_current == null) return;
            // 1) Fire the update hook
            _current.OnUpdate.Invoke();
            // 2) Check each transition in order
            foreach (var t in _current.transitions)
            {
                bool allMet = true;
                foreach (var cond in t.conditions)
                {
                    if (!cond.CheckCondition())
                    {
                        allMet = false;
                        break;
                    }
                }

                if (allMet)
                {
                    ChangeState(t.nextState);
                    break;
                }
            }
        }
        void ChangeState(int newIndex)
        {
            // boundary safety
            if (newIndex < 0 || newIndex >= States.Length) return;
            // exit current
            if (_current != null)
                _current.OnExit.Invoke();
            // enter new
            _currentIndex = newIndex;
            _current = States[newIndex];
            _current.OnEnter.Invoke();
        }
    }
}

