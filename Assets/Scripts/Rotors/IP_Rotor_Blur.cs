using System.Collections.Generic;
using Old_Input;
using UnityEngine;

namespace Rotors
{
    public class IP_Rotor_Blur : MonoBehaviour, IP_IHeliRotor
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>
        /// текстур всего 10, они будут изменяться в зависимости от скорости лопастей
        /// </summary>
        [Header("Rotor Blur Properties")] 
        [SerializeField] private List<GameObject> rotorsList = new List<GameObject>();

        /// <summary>
        /// GameObject blur - это 3D обьект Quad на него натянута текстура с материалом, эта текстура будет менятся
        /// </summary>
        [SerializeField] private GameObject blur;
        [SerializeField] private Material blurMaterial;
        [Space()] 
        [SerializeField] private List<Texture2D> blurTextures = new List<Texture2D>();
        [Space()] 
        [SerializeField] private float maxDps = 3000f;
        
        /// <summary>
        /// Обновляет визуальное состояние ротора вертолета в зависимости от скорости его вращения.
        /// Нормализует текущие обороты (DPS) относительно максимальных, вычисляет индекс 
        /// соответствующей текстуры размытия (Motion Blur) и применяет её к материалу ротора.
        /// </summary>
        /// <param name="currentDps">Текущая скорость вращения ротора в градусах в секунду (Degrees Per Second).</param>
        /// <param name="input">Контроллер ввода вертолета, содержащий команды игрока (в текущей логике метода не используется).</param>

        private void OnEnable()
        {
            blurMaterial.SetTexture(MainTex, blurTextures[0]);
        }

        private void OnDisable()
        {
            blurMaterial.SetTexture(MainTex, blurTextures[0]);
        }
        
        public void UpdateRotors(float dps, IP_Input_Controller input)
        {
            // Шаг 1: Приводим текущую скорость к диапазону от 0.0 до 1.0
            float normalizedDps = Mathf.InverseLerp(0f, maxDps, dps);

            // 2. Масштабирует нормализованную скорость под размер коллекции текстур и округляет её вниз 
            // с помощью Mathf.FloorToInt, чтобы получить точный целочисленный индекс. 
            // Метод Mathf.Clamp страхует от выхода за пределы диапазона [0, blurTextures.Count - 1], 
            // предотвращая падение ошибки ArgumentOutOfRangeException.

            int blurTextureIndex = Mathf.FloorToInt(normalizedDps * (blurTextures.Count - 1));
            // чтобы не выйти за диапазон текстур
            blurTextureIndex = Mathf.Clamp(blurTextureIndex, 0, blurTextures.Count - 1);

            if (blurMaterial && blurTextures.Count > 0)
            {
                // Шаг 3: Устанавливаем выбранную текстуру размытия в материал ротора
                blurMaterial.SetTexture("_MainTex", blurTextures[blurTextureIndex]);
            }
            
            if (blurTextureIndex > 2 && blurTextures.Count > 0)
            {
                //blurMaterial.SetColor("_MainTex", new Color(255f, 255f, 255f, 255f));
                HandleVisibleBlades(false);
            }
            else
            {
                HandleVisibleBlades(true);
            }
        }

        private void HandleVisibleBlades(bool visible)
        {
            foreach (var blade in rotorsList)
            {
                blade.SetActive(visible);
            }
        }
    }
}
