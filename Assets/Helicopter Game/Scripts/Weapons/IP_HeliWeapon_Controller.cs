using System;
using System.Collections.Generic;
using System.Linq;
using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Weapons
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
            if (allowFiring)
                _weapons.ForEach(w => w.FireWeapon());
        }
    }
}
