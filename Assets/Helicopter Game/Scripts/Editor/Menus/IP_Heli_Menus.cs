using Helicopter_Game.Scripts.Controllers;
using Helicopter_Game.Scripts.Rigidbodies;
using UnityEngine;
using UnityEditor;

namespace Helicopter_Game.Scripts.Editor.Menus
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
            
            audioGRP.transform.SetParent(helicopter.transform);
            graphicsGRP.transform.SetParent(helicopter.transform);
            colGPR.transform.SetParent(helicopter.transform);
            
            Selection.activeGameObject = helicopter;
        }
    }
}
