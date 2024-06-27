using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "PlayerState", order = 0)]
public class PlayerState : ScriptableObject
{
    public enum States { walking, sprinting };
    public States activeState = States.walking;
}