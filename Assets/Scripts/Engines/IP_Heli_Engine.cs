using UnityEngine;

namespace Engines
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
        [SerializeField] private AnimationCurve powerCurve 
            = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        
        /// <summary>
        /// текущая лошадиная сила
        /// </summary>
        public float CurrentHP { get; private set; }
        /// <summary>
        /// текущий крутящий момент
        /// </summary>
        public float CurrentRPM { get; private set; }
        
        
        public void UpdateEngine(float throttleInput)
        {
            // Расчёт Лошадиных сыл
            float wantedHP = powerCurve.Evaluate(throttleInput) * maxHP;
            CurrentHP = Mathf.Lerp(CurrentHP, wantedHP, Time.fixedDeltaTime * powerDelay);
            
            float wantedRPM = powerCurve.Evaluate(throttleInput) * maxRPM;
            // Расчет Оборотов в минуту
            CurrentRPM = Mathf.Lerp(CurrentRPM, wantedRPM, Time.fixedDeltaTime * powerDelay);
        }
    }
}
