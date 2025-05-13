using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace StopNotifyingMe
{
    [BepInPlugin("kingbingus.stopnotifyingme", "StopNotifyingMe", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        void Start() =>
            new Harmony("kingbingus.stopnotifyingme").PatchAll(Assembly.GetExecutingAssembly());

        void Update()
        {
            GameObject? announcementThing = GameObject.Find("Miscellaneous Scripts/PrivateUIRoom_HandRays");

            if (announcementThing != null)
            announcementThing.Destroy();
        }
    }
}
