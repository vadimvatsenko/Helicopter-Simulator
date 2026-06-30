using Controllers;
using Engines;
using Rotors;
using UnityEditor;
using UnityEngine;

namespace Editor.Menus
{
    public class IP_Heli_Menus : UnityEditor.Editor
    {
        // создаст в шапке меню
        [MenuItem("Helicopter Game/Vehicles/Setup New Helicopter")]
        public static void BuildNewHelicopter()
        {
            // cоздаст объект Helicopter в нём компонент  IP_Heli_Controller в котором есть RequireComponent
            // который в свою очередь добавит скрипты
            GameObject helicopter = new GameObject("Helicopter", typeof(IP_Heli_Controller));
            
            // объект центра тяжести
            GameObject curCOG = new GameObject("COG");
            curCOG.transform.SetParent(helicopter.transform);
            
            // сразу будет активен объект, то есть выбран
            IP_Heli_Controller controller = helicopter.GetComponent<IP_Heli_Controller>();
            controller.Cog = curCOG.transform;
            
            GameObject audioGRP = new GameObject("Audio_GRP");
            GameObject graphicsGRP = new GameObject("Graphics_GRP");
            GameObject colGPR = new GameObject("Colliders_GPR");
            GameObject engineGPR = new GameObject("Engine_GPR");
            GameObject rotorGPR = new GameObject("Rotor_GPR");
            
            SetupRotorGRP(rotorGPR, controller);
            SetupEngineGRP(engineGPR, controller);
            
            audioGRP.transform.SetParent(helicopter.transform);
            graphicsGRP.transform.SetParent(helicopter.transform);
            colGPR.transform.SetParent(helicopter.transform);
            engineGPR.transform.SetParent(helicopter.transform);
            rotorGPR.transform.SetParent(helicopter.transform);
            
            Selection.activeGameObject = helicopter;
        }

        public static void SetupRotorGRP(GameObject rotorGo, IP_Heli_Controller controller)
        {
            IP_HeliRotor_Controller heliRotorController = rotorGo.AddComponent<IP_HeliRotor_Controller>();
            controller.RotorController = heliRotorController;
            
            GameObject mainGPR = new GameObject("Main_Rotor");
            GameObject tailGPR = new GameObject("Tail_Rotor");
            
            IP_HeliMain_Rotor mainRotor = mainGPR.AddComponent<IP_HeliMain_Rotor>();
            IP_HeliTail_Rotor tailRotor = tailGPR.AddComponent<IP_HeliTail_Rotor>();
            
            mainGPR.transform.SetParent(rotorGo.transform);
            tailGPR.transform.SetParent(rotorGo.transform);
        }

        public static void SetupEngineGRP(GameObject engineGo, IP_Heli_Controller controller)
        {
            GameObject engineGRP = new GameObject("Main_Engine");
            IP_Heli_Engine engine = engineGRP.AddComponent<IP_Heli_Engine>();
            controller.AddEngine(engine);
            
            engineGRP.transform.SetParent(engineGo.transform);
        }
    }
}
