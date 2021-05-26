using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSP;
using KSP.UI;
using UnityEngine.UI;
using KSP.UI.Screens;
using UnityEngine;

namespace KASM
{
    [KSPAddon(startup: KSPAddon.Startup.EditorAny, once: false)]
    public class KASM : MonoBehaviour
    {
        private ApplicationLauncherButton toolbarButton;
        private static RectTransform window;

        private AssetBundle assetBundle;
        private AssetBundle AssetBundle
        {
            get
            {
                if (!assetBundle)
                    assetBundle = Utilities.LoadAssetBundle("resources");

                return assetBundle;
            }
            set
            {
                assetBundle = value;
            }
        }

        private void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(LoadToolbarButton);
        }

        private void LoadToolbarButton()
        {
            if (!toolbarButton)
            {
                Texture buttonIcon = GameDatabase.Instance.GetTexture(Utilities.iconPath + "_disable", false);
                toolbarButton = ApplicationLauncher.Instance.AddModApplication(
                                onTrue: OnEditorEnable,
                                onFalse: OnEditorDisable,
                                onHover: null,
                                onHoverOut: null,
                                onEnable: null,
                                onDisable: null,
                                visibleInScenes: ApplicationLauncher.AppScenes.VAB | 
                                                 ApplicationLauncher.AppScenes.SPH,
                                texture: buttonIcon);
            }
        }

        private void OnEditorEnable()
        {
            toolbarButton.SetTexture(GameDatabase.Instance.GetTexture(Utilities.iconPath, false));
            Debug.Log(Utilities.debugName + "Editor Enable");

            if (window)
            {
                window.gameObject.SetActive(true);
            }
        }

        private void OnEditorDisable()
        {
            toolbarButton.SetTexture(GameDatabase.Instance.GetTexture(Utilities.iconPath + "_disable", false));
            Debug.Log(Utilities.debugName + "Editor Disable");

            if (window)
            {
                window.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!window)
            {
                if (UIMasterController.Instance)
                {
                    GameObject mainUI = Utilities.SafeLoadFromAssetBundle<GameObject>(AssetBundle, "MainUI");

                    window = Instantiate(mainUI).GetComponent<RectTransform>();
                    window.gameObject.SetActive(false);

                    Canvas canvas = window.GetComponent<Canvas>();
                    canvas.worldCamera = UIMasterController.Instance.appCanvas.worldCamera;
                    canvas.planeDistance = 625;

                    TestClass testClass = window.gameObject.AddComponent<TestClass>();
                    testClass.text = Utilities.GetComponentOnChild<Text>(window.transform, "Panel/Text_target");
                    Utilities.GetComponentOnChild<Button>(window.transform, "Panel/Button_target").onClick.AddListener(testClass.OnPress);

                    Utilities.AddComponentOnChild<DragWindow>(window.transform, "Panel/TitleBar");
                    Utilities.AddComponentOnChild<ScaleWindow>(window.transform, "Panel/ScaleButton");

                    Debug.Log(Utilities.debugName + "Canvas setup done");
                }
            }
        }
    }
}
