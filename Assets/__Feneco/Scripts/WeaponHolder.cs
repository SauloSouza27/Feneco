using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    public WeaponBase weapon;
    float castTime;
    float activeTime;
    float cooldownTime;

    enum AbilityState
    {
        ready,
        casttime,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    void Update()
    {
        switch (state)
        {
            case AbilityState.casttime:
                if (castTime > 0)
                {
                    castTime -= Time.deltaTime;
                }
                else
                {
                    weapon.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = weapon.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    weapon.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = weapon.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && state == AbilityState.ready)
        {
            state = AbilityState.casttime;
            weapon.CastTime(gameObject);
            castTime = weapon.castTime;
        }
    }
}