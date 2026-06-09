using OpenCover.Framework.Model;
using UnityEngine;

namespace Helicopter_Game.Scripts.Engines
{
    public class IP_Heli_Engine : MonoBehaviour
    {
        /// <summary>
        /// HP - Hourse Power двигателя
        /// </summary>
        [SerializeField] private float maxHP = 140f;
        /// <summary>
        /// RPM (Revolutions Per Minute) — это количество оборотов в минуту.
        /// </summary>
        [SerializeField] private float maxRPM = 2700f;
        [SerializeField] private float powerDelay = 2f;
        /// <summary>
        /// Кривая анимации
        /// </summary>
        [SerializeField] private AnimationCurve powerCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        public float CurrentHP { get; private set; }
        public float CurrentRPM { get; private set; }
        
        
        public void UpdateEngine(float throttleInput)
        {
            // Calculate HorsePower
            float wantedHP = powerCurve.Evaluate(throttleInput)  * maxHP;
            CurrentHP = Mathf.Lerp(CurrentHP, wantedHP, Time.deltaTime * powerDelay);
            
            // Calculate RPM
            float wantedRPM = powerCurve.Evaluate(throttleInput)  * maxRPM;
            CurrentRPM = Mathf.Lerp(CurrentRPM, wantedRPM, Time.deltaTime * powerDelay);
        }
    }
}
