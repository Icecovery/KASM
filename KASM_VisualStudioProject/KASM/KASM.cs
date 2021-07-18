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
        private static ApplicationLauncherButton toolbarButton;
        private static RectTransform window;

        private static AssetBundle assetBundle;
        private static AssetBundle AssetBundle
        {
            get
            {
                if (!assetBundle)
                    assetBundle = Utilities.LoadAssetBundle("kasm_resources");

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
            GameEvents.onGUIEditorToolbarReady.Add(LoadMainUI);
        }

        private void LoadToolbarButton()
        {
            if (!toolbarButton)
            {
                Texture buttonIcon = GameDatabase.Instance.GetTexture(Utilities.iconPath + "_disable", false);
                toolbarButton = ApplicationLauncher.Instance.AddModApplication(
                                onTrue: OnEditorTrue,
                                onFalse: OnEditorFalse,
                                onHover: null,
                                onHoverOut: null,
                                onEnable: null,
                                onDisable: null,
                                visibleInScenes: ApplicationLauncher.AppScenes.VAB | 
                                                 ApplicationLauncher.AppScenes.SPH,
                                texture: buttonIcon);
            }

            if (toolbarButton)
            {
                toolbarButton.SetFalse();
            }
        }

        private void LoadMainUI()
        {
            if (!window)
            {
                if (UIMasterController.Instance)
                {
                    // load window prefab
                    window = Instantiate(Utilities.SafeLoadFromAssetBundle<GameObject>(AssetBundle, "Window")).GetComponent<RectTransform>();

                    // set parent to app canvas
                    Canvas appCanvas = UIMasterController.Instance.appCanvas;
                    window.transform.SetParent(appCanvas.transform);
                    window.transform.localScale = Vector3.one;
                    window.transform.localPosition = Vector3.zero;
                    window.anchoredPosition -= window.rect.size * new Vector2(0.5f, -0.5f) / appCanvas.scaleFactor;

                    window.gameObject.SetActive(false);

                    // === window setup ===
                    DragWindow dragWindow = Utilities.AddComponentOnChild<DragWindow>(window.transform, "TitleBar");
                    dragWindow.canvas = appCanvas;
                    dragWindow.windowTransform = window;

                    ScaleWindow scaleWindow = Utilities.AddComponentOnChild<ScaleWindow>(window.transform, "ScaleButton");
                    scaleWindow.canvas = appCanvas;
                    scaleWindow.windowTransform = window;

                    Utilities.GetComponentOnChild<Button>(window.transform, "TitleBar/CloseButton").onClick.AddListener(delegate { toolbarButton.SetFalse(); });

                    ResizePanel resizePanel = Utilities.AddComponentOnChild<ResizePanel>(window.transform, "SidePanel/ResizeBar");
                    resizePanel.canvas = appCanvas;
                    resizePanel.pannel = Utilities.GetComponentOnChild<RectTransform>(window.transform, "SidePanel");

                    ProcessorEditor processorEditor = window.gameObject.AddComponent<ProcessorEditor>();
                    processorEditor.nodePanelTemplate = Utilities.GetComponentOnChild<Transform>(window.transform, "SidePanel/Scroll View/Viewport/Content/Template").gameObject;

                    Utilities.GetComponentOnChild<InputField>(window.transform, "SidePanel/SearchBar").onValueChanged.AddListener(processorEditor.NodePanelSearch);

                    // === eo window setup ===

                    Utilities.Log("Window setup done");
                }
            }
        }

        private void OnEditorTrue()
        {
            toolbarButton.SetTexture(GameDatabase.Instance.GetTexture(Utilities.iconPath, false));
            Utilities.Log("Editor Enable");

            if (window)
            {
                window.gameObject.SetActive(true);
            }
        }

        private void OnEditorFalse()
        {
            toolbarButton.SetTexture(GameDatabase.Instance.GetTexture(Utilities.iconPath + "_disable", false));
            Utilities.Log("Editor Disable");

            if (window)
            {
                window.gameObject.SetActive(false);
            }
        }
    }
}
