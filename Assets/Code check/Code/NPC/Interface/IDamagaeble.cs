using UnityEngine;

public interface IDamageable 
{
    void Damage(float damageAmount);
    void Die();///// scared lvl?

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }


}
