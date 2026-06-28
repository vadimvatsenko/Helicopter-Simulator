using Helicopter_Game.Scripts.Weapons;
using UnityEngine;

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
    
    protected AudioSource AudioSource;
    protected int CurrentAmmo = 0;

    private void Start()
    {
        CurrentAmmo = maxAmmoCount;
    }
    public void FireWeapon()
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
        throw new System.NotImplementedException();
    }
    
    protected virtual void HandleAudioSource()
    {
        throw new System.NotImplementedException();
    }
    
    protected virtual void HandleVFX()
    {
        throw new System.NotImplementedException();
    }
}
