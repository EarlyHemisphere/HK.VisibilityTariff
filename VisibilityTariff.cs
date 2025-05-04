using System;
using Modding;
using Satchel;
using Satchel.BetterMenus;
using SFCore.Utils;
using UnityEngine;

namespace VisibilityTariff {
    public class VisibilityTariff : Mod, ICustomMenuMod, IGlobalSettings<GlobalSettings> {
        private Menu menuRef = null;
        public static VisibilityTariff instance;

        public VisibilityTariff() : base("Visibility Tariff") { 
            instance = this;
        }

        public static GlobalSettings globalSettings { get; private set; } = new();

        public bool ToggleButtonInsideMenu => true;

        public void OnLoadGlobal(GlobalSettings s) => globalSettings = s;
        public GlobalSettings OnSaveGlobal() => globalSettings;

        public override void Initialize() {
            Log("Initializing");

            ModHooks.AfterSavegameLoadHook += AfterSavegameLoadHook;

            Log("Initialized");
            Log("Shrubb is the goat");
            Log("🗿");
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates) {
            menuRef ??= new Menu(
                name: "Visibility Tariff",
                elements: new Element[] {
                    new CustomSlider(
                        name: "Scale Factor",
                        storeValue: val => globalSettings.scaleFactor = val,
                        loadValue: () => globalSettings.scaleFactor,
                        minValue: 1,
                        maxValue: 30
                    )
                }
            );
            
            return menuRef.GetMenuScreen(modListMenu);
        }

        public void AfterSavegameLoadHook(SaveGameData _) {
            GameManager.instance.gameObject.AddComponent<VignetteTariff>();
        }
    }

    public class GlobalSettings {
        public float scaleFactor = 10f;
    }
}
