using ColossalFramework;
using HarmonyLib;
using ICities;
using ModsCommon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Resources;
using System.Text;
using UnityEngine;
using static ModsCommon.SettingsHelper;

namespace AreaCostAdjuster
{
    public class Mod : BasePatcherMod<Mod>
    {
        public override string NameRaw => "Area Cost Adjuster";
        public override string Description => Localize.Mod_Description;

        protected override ulong StableWorkshopId => 2863765284ul;
        protected override ulong BetaWorkshopId => 0ul;

        public override List<ModVersion> Versions { get; } = new List<ModVersion>
        {
            new ModVersion(new Version("1.0"), new DateTime(2022,9,16)),
        };
        protected override Version RequiredGameVersion => new Version(1, 15, 1, 4);

#if BETA
        public override bool IsBeta => true;
#else
        public override bool IsBeta => false;
#endif
        protected override string IdRaw => nameof(AreaCostAdjuster);
        protected override ResourceManager LocalizeManager => Localize.ResourceManager;

        protected override void GetSettings(UIHelperBase helper)
        {
            var settings = new Settings();
            settings.OnSettingsUI(helper);
        }

        protected override void SetCulture(CultureInfo culture) => Localize.Culture = culture;

        protected override bool PatchProcess()
        {
            var success = AddPostfix(typeof(Patcher), nameof(Patcher.AdjustPedestrianCost), PropertyType.Getter, typeof(DistrictPark), nameof(DistrictPark.pedestrianZoneMaintenanceCost));
            return success;
        }
    }

    public static class Patcher
    {
        public static void AdjustPedestrianCost(ref int __result)
        {
            __result = __result * Settings.PedestrianCost / 100;
        }
    }

    public class Settings : BaseSettings<Mod>
    {
        public static SavedInt PedestrianCost { get; } = new SavedInt(nameof(PedestrianCost), SettingsFile, 100, true);

        protected override void FillSettings()
        {
            base.FillSettings();

            var group = GeneralTab.AddGroup(CommonLocalize.Settings_General);

            AddIntField(group, Localize.PedestrianAreaMultiplier, PedestrianCost, 100, 0, 200);
        }
    }
}
