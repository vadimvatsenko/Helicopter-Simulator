using UnityEngine;
using Helicopter_Game.Scripts.Weapons;

namespace Helicopter_Game.Scripts.Weapons.Weapon_Types
{
    public class IP_RapidFire_Weapon : IP_Base_Weapon
    {
        [Header("Rapid Fire Properties")] 
        [SerializeField] private float fireRate = 0.15f;
        private float _lastFireTime;
        public override void FireWeapon()
        {
            if (Time.time >= _lastFireTime + fireRate)
            {
                Fire();
                _lastFireTime = Time.time;
            }
        }
    }
}