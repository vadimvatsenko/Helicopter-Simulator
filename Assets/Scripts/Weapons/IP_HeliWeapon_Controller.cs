using System.Collections.Generic;
using System.Linq;
using Old_Input;
using UnityEngine;

namespace Weapons
{
    public class IP_HeliWeapon_Controller : MonoBehaviour
    {
        [Header("Weapon Conteroller Propperties")]
        [SerializeField] public bool allowFiring = true;
        
        private List<IP_IWeapon> _weapons = new List<IP_IWeapon>();

        private void Start()
        {
            _weapons = GetComponentsInChildren<IP_IWeapon>().ToList();
        }

        public void UpdateWeapons(IP_Input_Controller input)
        {
            if (allowFiring && input.FireInput)
            {
                _weapons.ForEach(w => w.FireWeapon());
            }
        }
    }
}
