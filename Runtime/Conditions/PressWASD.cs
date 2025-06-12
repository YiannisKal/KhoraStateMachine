using Khora.FSM;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/PressWASD")]
public class PressWASD : Condition
{
    
    public override bool CheckCondition()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }
}
