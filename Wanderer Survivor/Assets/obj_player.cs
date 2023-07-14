using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_player : MonoBehaviour
{
//Statistiques et niveau de statistiques
    public double max_hp;
    public int max_hp_level;
    public double damage;
    public int damage_level;
    public double pickup_range;
    public int pickup_range_level;
    public double area;
    public int area_level;
    public double current_hp;
    public double crit_chance;
    public int crit_chance_level;
    public double crit_damage;
    public int crit_damage_level;
    public float speed;
    public int speed_level;
    public double coin_multiplier;
    public int coin_multiplier_level;
    public double luck;
    public int luck_level;
    public double regen;
    public int regen_level;
    public double xp_coeff;
    public int xp_coeff_level;
    public double armor_multiplier;
    public int armor_level;
    public double cooldown;
    public int cooldown_level;
    public double dodge;
    public int dodge_level;
    public double current_xp;
    public double max_xp;
    public int level;
    public int kill;
    public int coin;
        ////////////////// SPELL ACTIF
    //public Objectpool fireball_objpool;
    public Transform fireball_spawnpoint;
    public double fireball_cooldown;
    private double last_fireball_time;
    public int fireball_level;
    /////
    //public Objectpool firewall_objpool;
    public Transform firewall_spawnpoint;
    public double firewall_cooldown;
    private double last_firewall_time;
    public int firewall_level;  
    /////
    //public Objectpool tornado_objpool;
    public Transform tornado_spawnpoint;
    public double tornado_cooldown;
    private double last_tornado_time;
    public int tornado_level;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
       //Initialisation des statistiques
        max_hp=100;
        current_hp = max_hp;
        damage=1;
        pickup_range=1;
        crit_chance=0.1;
        crit_damage=1.25;
        speed=0f;
        coin_multiplier=1;
        luck=0.05;
        regen=0.05;
        xp_coeff=1;
        armor_multiplier=1;
        cooldown=1;
        current_xp=0;
        max_xp=50;
        level=1;
        coin=0;
        kill=0;
        fireball_cooldown = 5*cooldown;
        tornado_cooldown =  10*cooldown;
        firewall_cooldown = 10*cooldown; 
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
        //Si la vie actuelle est pas au maximum, le joueur regen, si avec la regen il dépasse la vie maximale, il gagne rien.
        if(current_hp<max_hp)
        {
            current_hp+=regen;
        }
        if(current_hp>max_hp)
        {
            current_hp=max_hp;
        }
    }
}
