using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore.Utils;
using UnityEngine;

namespace VisibilityTariff {
    public class VignetteTariff: MonoBehaviour {
        private bool fsmModified = false;
        public void Awake() {
            On.PlayMakerFSM.OnEnable += OnFsmEnable;
        }

        public void Update() {
            GameObject vignette = GameObject.FindGameObjectWithTag("Vignette");
            Modding.Logger.Log("Update");
            

            if (vignette == null) return;

            Modding.Logger.Log(vignette.transform.localScale);

            vignette.transform.localScale = new Vector3(0.1875f, 0.28125f, 0.1875f) * VisibilityTariff.globalSettings.scaleFactor;

            Vector3 mousePos = Input.mousePosition;
            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

            Vector3 worldMousePos = GameCameras.instance.tk2dCam.ScreenCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 18f));
            vignette.transform.position = new Vector3(worldMousePos.x, worldMousePos.y, -20);
        }

        public void OnFsmEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
            orig(self);

            if (!fsmModified && self.FsmName == "Darkness Control") {
                self.InsertAction("Damage", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                self.InsertAction("Recover", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                self.InsertAction("Scene Reset", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                self.InsertAction("Scene Reset 2", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                self.InsertAction("Death", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                self.InsertAction("Pause", new CallMethod {
                    behaviour = this,
                    methodName = "PreventProgression",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
            }
        }

        public void PreventProgression() {
            GameObject vignette = GameObject.FindGameObjectWithTag("Vignette");
            
            if (vignette == null) return;
            
            vignette.LocateMyFSM("Darkness Control").SetState("Idle");
        }
    }
}