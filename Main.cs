using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace StopNotifyingMe
{
    [BepInPlugin("kingbingus.stopnotifyingme", "StopNotifyingMe", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        static Plugin instance;
        bool KillNotifications = false;

        void Start() =>
            new Harmony("kingbingus.stopnotifyingme").PatchAll(Assembly.GetExecutingAssembly()); instance = this;
        
        internal void DestroyUIRoom(GameObject holderGameObject)
        {
            PrivateUIRoom? com = holderGameObject.GetComponent<PrivateUIRoom>();

            if (com)
                com.StopOverlay();
            
            holderGameObject.Destroy();
        }

        void Update()
        {
            GameObject? announcementThing = GameObject.Find("Miscellaneous Scripts/PrivateUIRoom_HandRays");

            if (announcementThing != null)
                DestroyUIRoom(announcementThing)
        }

        void OnEnable()
        {
            // choose to destroy all notifications or not
            KillNotifications = Config.Bind(
                "Global",
                "KillNotifications",
                "true",
                "(true/false) Stop notifications from appearing").Value == "true"
            ;
        }
    }

    [HarmonyPatch(typeof(PrivateUIRoom), "Start")]
    class RemoveNotif
    {
        void Postfix(PrivateUIRoom __instance) {
            if (Plugin.instance.KillNotifications)
                Plugin.instance.DestroyUIRoom(__instance.gameObject) // probably (idk im coding this on a chromebook)
        }
    }
}
