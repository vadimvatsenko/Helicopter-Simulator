using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class IP_Base_Weapon : MonoBehaviour, IP_IWeapon
    {
        [Header("Base Weapon Property")] 
        [SerializeField] private bool allowFiring = true;
        [SerializeField] private Transform muzzlePos;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int maxAmmoCount = 100;
        [Space(5)]
        [SerializeField] private GameObject muzzleFlashPrefab;
        [Space(5)]
        //[SerializeField] private AudioClip audioClip; 
    
        protected AudioSource AudioSource;
        protected int CurrentAmmo = 0;

        private void Start()
        {
            CurrentAmmo = maxAmmoCount;
            AudioSource = gameObject.GetComponent<AudioSource>();
        }
        public virtual void FireWeapon()
        {
            Fire();
        }

        protected void Fire()
        {
            if (CurrentAmmo > 0)
            {
                HandleProjectile();
                HandleAudioSource();
                HandleVFX();
            
                CurrentAmmo--;
                CurrentAmmo = Mathf.Clamp(CurrentAmmo, 0, maxAmmoCount);
            }
            else
            {
                Reload();
            }
        }

        public void Reload() => CurrentAmmo = maxAmmoCount;
    
        protected virtual void HandleProjectile()
        {
            Instantiate(projectilePrefab, muzzlePos.position, Quaternion.LookRotation(muzzlePos.forward));
        }
    
        protected virtual void HandleAudioSource()
        {
            AudioSource.Play();
        }
    
        protected virtual void HandleVFX()
        {
        
        }
    }
}
