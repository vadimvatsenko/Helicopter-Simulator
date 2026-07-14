using System.Collections.Generic;
using System.Linq;
using Old_Input;
using UnityEngine;

namespace Weapons
{
    public class HeliWeaponController : MonoBehaviour
    {
        [Header("Weapon Conteroller Propperties")]
        [SerializeField] public bool allowFiring = true;
        
        private List<IWeapon> _weapons = new List<IWeapon>();

        private void Start()
        {
            _weapons = GetComponentsInChildren<IWeapon>().ToList();
        }

        public void UpdateWeapons(InputController input)
        {
            if (allowFiring && input.FireInput)
            {
                _weapons.ForEach(w => w.FireWeapon());
            }
        }
    }
}
