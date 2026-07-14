using UnityEngine;

namespace Weapons.Weapon_Types
{
    public class RapidFireWeapon : BaseWeapon
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