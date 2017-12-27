using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGroundAttacker: MonoBehaviour{
    public int damage;
    public float range;
    private float basicCooldownTracker;
    public float basicCooldownTime;

    public BasicGroundAttacker(int damage, float basicCooldownTime, float range) {
        this.damage = damage;
        this.basicCooldownTime = basicCooldownTime;
        this.range = range;
    }

    public void UpdateBasicCooldown()
    {
        if (basicCooldownTracker > 0)
            basicCooldownTracker -= 1f;
    }

    public bool HasCooldownElapsed()
    {
        if (basicCooldownTracker <= 0)
            return true;
        return false;
    }

    public void ResetBasicCooldown()
    {
        basicCooldownTracker = basicCooldownTime;
    }

    public int Attack()
    {
        return damage;
    }

    //pause attack to manage other abilities to avoid burst
}
