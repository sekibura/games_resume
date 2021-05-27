using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable :MonoBehaviour
{
    public abstract void ApplyDamage(int damageValue, Vector3 playerPosition);
}
