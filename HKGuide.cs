using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace HKGuide
{
    public class HKGuide : Mod
    {
        new public string GetName() => "HKGuide";
        public override string GetVersion() => "v1";

        public override void Initialize()
        {
            ModHooks.HeroUpdateHook += OnHeroUpdate;
        }

        public void OnHeroUpdate()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Log("Press Key O");
            }
        }
    }
}
