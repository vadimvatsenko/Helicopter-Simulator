using Controllers;
using Engines;
using Rotors;
using UnityEditor;
using UnityEngine;

namespace Editor.Menus
{
    public class HeliMenus : UnityEditor.Editor
    {
        // создаст в шапке меню
        [MenuItem("Helicopter Game/Vehicles/Setup New Helicopter")]
        public static void BuildNewHelicopter()
        {
            // cоздаст объект Helicopter в нём компонент  IP_Heli_Controller в котором есть RequireComponent
            // который в свою очередь добавит скрипты
            GameObject helicopter = new GameObject("Helicopter", typeof(HeliController));
            
            // объект центра тяжести
            GameObject curCOG = new GameObject("COG");
            curCOG.transform.SetParent(helicopter.transform);
            
            // сразу будет активен объект, то есть выбран
            HeliController controller = helicopter.GetComponent<HeliController>();
            controller.Cog = curCOG.transform;
            
            GameObject audioGRP = new GameObject("Audio_GRP");
            GameObject graphicsGRP = new GameObject("Graphics_GRP");
            GameObject colGPR = new GameObject("Colliders_GPR");
            GameObject engineGPR = new GameObject("Engine_GPR");
            GameObject rotorGPR = new GameObject("Rotor_GPR");
            
            SetupRotorGrp(rotorGPR, controller);
            SetupEngineGrp(engineGPR, controller);
            
            audioGRP.transform.SetParent(helicopter.transform);
            graphicsGRP.transform.SetParent(helicopter.transform);
            colGPR.transform.SetParent(helicopter.transform);
            engineGPR.transform.SetParent(helicopter.transform);
            rotorGPR.transform.SetParent(helicopter.transform);
            
            Selection.activeGameObject = helicopter;
        }

        public static void SetupRotorGrp(GameObject rotorGo, HeliController controller)
        {
            HeliRotorController heliRotorController = rotorGo.AddComponent<HeliRotorController>();
            controller.RotorController = heliRotorController;
            
            GameObject mainGpr = new GameObject("Main_Rotor");
            GameObject tailGpr = new GameObject("Tail_Rotor");
            
            HeliMainRotor mainRotor = mainGpr.AddComponent<HeliMainRotor>();
            HeliTailRotor tailRotor = tailGpr.AddComponent<HeliTailRotor>();
            
            mainGpr.transform.SetParent(rotorGo.transform);
            tailGpr.transform.SetParent(rotorGo.transform);
        }

        public static void SetupEngineGrp(GameObject engineGo, HeliController controller)
        {
            GameObject engineGrp = new GameObject("Main_Engine");
            HeliEngine engine = engineGrp.AddComponent<HeliEngine>();
            controller.AddEngine(engine);
            
            engineGrp.transform.SetParent(engineGo.transform);
        }
    }
}
