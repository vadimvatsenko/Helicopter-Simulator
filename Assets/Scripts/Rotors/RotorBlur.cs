using System.Collections.Generic;
using Old_Input;
using UnityEngine;

namespace Rotors
{
    public class RotorBlur : MonoBehaviour, IHeliRotor
    {
        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

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
        [SerializeField] private float maxDps = 2700f;
        
        /// <summary>
        /// Обновляет визуальное состояние ротора вертолета в зависимости от скорости его вращения.
        /// Нормализует текущие обороты (DPS) относительно максимальных, вычисляет индекс 
        /// соответствующей текстуры размытия (Motion Blur) и применяет её к материалу ротора.
        /// </summary>
        /// <param name="currentDps">Текущая скорость вращения ротора в градусах в секунду (Degrees Per Second).</param>
        /// <param name="input">Контроллер ввода вертолета, содержащий команды игрока (в текущей логике метода не используется).</param>

        private void OnEnable()
        {
            blurMaterial.SetTexture(BaseMap, blurTextures[0]);
        }

        private void OnDisable()
        {
            blurMaterial.SetTexture(BaseMap, blurTextures[0]);
        }
        
        public void UpdateRotors(float dps, InputController input)
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

            Debug.Log(blurTextureIndex);
            
            if (blurMaterial && blurTextures.Count > 0)
            {
                Debug.Log("Setting blur material");
                // Шаг 3: Устанавливаем выбранную текстуру размытия в материал ротора
                blurMaterial.SetTexture(BaseMap, blurTextures[blurTextureIndex]);
            }
            
            if (blurTextureIndex > 2 && blurTextures.Count > 0)
            {
                
                //blurMaterial.SetColor("_MainTex", new Color(255f, 255f, 255f, 255f));
                HandleVisibleBlades(false);
            }
            else
            {
                //blurPrefab.gameObject.SetActive(false);
                HandleVisibleBlades(true);
            }
        }

        private void HandleVisibleBlades(bool visible)
        {
            foreach (var blade in rotorsList)
            {
                blade.SetActive(visible);
            }
            
            blur.gameObject.SetActive(!visible);
        }
    }
}
