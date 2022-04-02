using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region singleton
    public static PlayerStats instance;

    void Awake()
    {
        instance = this;
    }

    #endregion


    public GrabStats GrabStatsScript;

    public float maxHealth = 100;
    public float currentHealth { get; private set; }


    public Stats armor;
    public Stats speed;
    public Stats damage;
    public Stats range;
    public Stats gathering;
    public Stats jump;

    //private void Awake()
    //{
    //    currentHealth = maxHealth;
    //}

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEqupmentChanged;
        currentHealth = maxHealth;
    }

    void OnEqupmentChanged(Equipment newItem, Equipment oldItem)
    {
        //Debug.Log(newItem.name + " OnEqupmentChanged");
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            speed.AddModifier(newItem.speedModifier);
            damage.AddModifier(newItem.damageModifier);
            range.AddModifier(newItem.rangeModifier);
            gathering.AddModifier(newItem.gatheringModifier);
            jump.AddModifier(newItem.jumpModifier);
            //Debug.Log(newItem.name + " Modiefires " + newItem.armorModifier);
        }

        if (oldItem != null && oldItem != newItem)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            speed.RemoveModifier(oldItem.speedModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            range.RemoveModifier(oldItem.rangeModifier);
            gathering.RemoveModifier(oldItem.gatheringModifier);
            jump.RemoveModifier(oldItem.jumpModifier);
        }
        GrabStatsScript.ChangeStats();
    }


    public void TakeDamage(float damage)
    {
        damage =- (damage * armor.GetValue());

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(transform.name + " DIED");
    }

}
