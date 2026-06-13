using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Rotors
{
    public class IP_HeliMain_Rotor : MonoBehaviour, IP_IHeliRotor
    {
        [Header("Main Rotor Properties")] 
        // left rotor // левая и правая лопасть 
        [SerializeField] private Transform lRotor;
        [SerializeField] private Transform rRotor;
        // максимальный угол поворота лопасти
        [SerializeField] private float maxPitch = 35f;

        [SerializeField] private float radius = 2f;

        private Vector2 cyclicVal;
        
        public float CurrentRPMs {get; private set;}
        
        public void UpdateRotors(float dps, IP_Input_Controller input)
        {
            // Здесь угловая скорость из градусов в секунду переводится в привычные пилотам обороты в минуту:
            // dps / 360 — делим градусы на 360 (градусов в одном полном обороте), чтобы узнать, сколько оборотов делает винт за одну секунду.
            // * 60f — умножаем на 60 (секунд в минуте), чтобы получить количество оборотов в минуту (RPM).
            CurrentRPMs = (dps / 360) * 60f;
            //Debug.Log(CurrentRPMs);
            // new
            transform.Rotate(Vector3.up, dps * Time.deltaTime * 0.5f);
            // new
            Vector3 descNormal = Vector3.Normalize(transform.up + new Vector3(-cyclicVal.x, 0f, -cyclicVal.y));
            // new
            cyclicVal = input.CyclicInput;
            
            lRotor.localRotation = Quaternion.Euler(-input.StickyCollectiveInput * maxPitch, 0, 0);
            rRotor.localRotation = Quaternion.Euler(input.StickyCollectiveInput * maxPitch, 0, 0);
            
        }
    }
}
