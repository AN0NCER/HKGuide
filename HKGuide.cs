using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
using Resources = HKGuide.ModTools.Resources;
using Object = UnityEngine.Object;

namespace HKGuide
{
    public class HKGuide : Mod
    {
        new public string GetName() => "HKGuide";
        public override string GetVersion() => "v1";

        public override void Initialize()
        {
            LoadResources();
            ModHooks.HeroUpdateHook += OnHeroUpdate;
        }

        public void OnHeroUpdate()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Log("Press Key O");
            }
        }

        /// <summary>
        /// Loading Resources Mods
        /// </summary>
        private void LoadResources()
        {
            GameObject resourceObject = new GameObject("HKGuideResources");
            resourceObject.AddComponent<Resources>();
            Object.DontDestroyOnLoad(resourceObject);
            Resources.Instance.Load();
        }
    }
}
