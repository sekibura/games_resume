using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float GravityModifier;
    public float JumpTakeOffSpeed;
    public float JumpAttacked;
    public float MaxSpeed;
    public float VelocityMultipler;
}
