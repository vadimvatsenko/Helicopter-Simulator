using UnityEngine;
using UnityEngine.Serialization;

namespace Engines
{
    public class HeliEngine : MonoBehaviour
    {
        /// <summary>
        /// HP - Hourse Power двигателя
        /// </summary>
        [SerializeField] private float maxHp = 140f;
        /// <summary>
        /// RPM (Revolutions Per Minute) — это количество оборотов в минуту.
        /// </summary>
        [SerializeField] private float maxRpm = 2700f;
        [SerializeField] private float powerDelay = 2f;
        /// <summary>
        /// Кривая анимации
        /// </summary>
        [SerializeField] private AnimationCurve powerCurve 
            = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        
        /// <summary>
        /// текущая лошадиная сила
        /// </summary>
        public float CurrentHp { get; private set; }
        /// <summary>
        /// текущий крутящий момент
        /// </summary>
        public float CurrentRpm { get; private set; }
        
        
        public void UpdateEngine(float throttleInput)
        {
            // Расчёт Лошадиных сыл
            float wantedHp = powerCurve.Evaluate(throttleInput) * maxHp;
            CurrentHp = Mathf.Lerp(CurrentHp, wantedHp, Time.fixedDeltaTime * powerDelay);
            
            float wantedRpm = powerCurve.Evaluate(throttleInput) * maxRpm;
            // Расчет Оборотов в минуту
            CurrentRpm = Mathf.Lerp(CurrentRpm, wantedRpm, Time.fixedDeltaTime * powerDelay);
        }
    }
}
