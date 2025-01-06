using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TempImguiGraphics : MonoBehaviour
{
    private bool isFullscreen = false;
    private int selectedResolutionIndex = 0;
    private int selectedQualityLevel = 0;
    private int selectedMSAAIndex = 0;
    private int selectedShadowQuality = 0;
    private float renderScale = 1.0f;

    private Resolution[] resolutions;
    private string[] msaaOptions = new string[] { "None", "2x", "4x", "8x" };
    private string[] shadowQualityOptions = new string[] { "Low", "Medium", "High" };

    // Flag to show/hide the graphics options menu
    public bool showGraphicsMenu = false;

    // Scroll position for the scroll view
    private Vector2 scrollPosition = Vector2.zero;

    void Start()
    {
        resolutions = Screen.resolutions;
    }

    void OnGUI()
    {
        // Show the menu only if the flag is true
        if (showGraphicsMenu)
        {
            // Begin scroll view for scrolling content
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(300), GUILayout.Height(500));

            // Apply button at the top of the menu
            if (GUILayout.Button("Apply"))
            {
                ApplySettings();
            }
            GUILayout.Space(10); // Space after the Apply button at the top

            GUILayout.Label("Graphics Settings", GUILayout.Height(30));

            // Fullscreen toggle with space before it
            isFullscreen = GUILayout.Toggle(isFullscreen, "Fullscreen");
            GUILayout.Space(10); // Add space after fullscreen toggle

            // Add a label to separate the resolution section
            GUILayout.Label("Resolution", GUILayout.Height(20));
            GUILayout.Space(5); // Small space before resolution dropdown

            // Resolution dropdown
            string[] resolutionOptions = new string[resolutions.Length];
            for (int i = 0; i < resolutions.Length; i++)
            {
                resolutionOptions[i] = resolutions[i].width + "x" + resolutions[i].height;
            }
            selectedResolutionIndex = GUILayout.SelectionGrid(selectedResolutionIndex, resolutionOptions, 1);
            GUILayout.Space(10); // Add space after resolution dropdown

            // Add a label to separate the quality settings section
            GUILayout.Label("Quality Settings", GUILayout.Height(20));
            GUILayout.Space(5); // Small space before quality dropdown

            // Quality level dropdown
            selectedQualityLevel = GUILayout.SelectionGrid(selectedQualityLevel, QualitySettings.names, 1);
            GUILayout.Space(10); // Add space after quality level dropdown

            // Add a label to separate the MSAA section
            GUILayout.Label("Anti-Aliasing (MSAA)", GUILayout.Height(20));
            GUILayout.Space(5); // Small space before MSAA dropdown

            // MSAA dropdown for URP with space before it
            selectedMSAAIndex = GUILayout.SelectionGrid(selectedMSAAIndex, msaaOptions, 1);
            GUILayout.Space(10); // Add space after MSAA dropdown

            // Add a label to separate the shadow quality section
            GUILayout.Label("Shadow Quality", GUILayout.Height(20));
            GUILayout.Space(5); // Small space before shadow quality dropdown

            // Shadow quality dropdown
            selectedShadowQuality = GUILayout.SelectionGrid(selectedShadowQuality, shadowQualityOptions, 1);
            GUILayout.Space(10); // Add space after shadow quality dropdown

            // Add a label to separate the render scale section
            GUILayout.Label("Render Scale", GUILayout.Height(20));
            GUILayout.Space(5); // Small space before render scale slider

            // Render scale slider (value between 0.5 and 2)
            renderScale = GUILayout.HorizontalSlider(renderScale, 0.5f, 2.0f);
            GUILayout.Label("Render Scale: " + renderScale.ToString("F2"));
            GUILayout.Space(10); // Add space after render scale slider

            // Apply button with space before it
            if (GUILayout.Button("Apply"))
            {
                ApplySettings();
            }
            GUILayout.Space(10); // Add space after Apply button

            // Close button
            if (GUILayout.Button("Close"))
            {
                showGraphicsMenu = false; // Hide the menu when Close is clicked
            }

            // End the scroll view
            GUILayout.EndScrollView();
        }
    }

    private void ApplySettings()
    {
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullscreen);

        QualitySettings.SetQualityLevel(selectedQualityLevel);

        UniversalRenderPipelineAsset pipelineAsset = (UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset;

        // Apply MSAA setting
        switch (selectedMSAAIndex)
        {
            case 0:
                pipelineAsset.msaaSampleCount = 1;
                break;
            case 1:
                pipelineAsset.msaaSampleCount = 2;
                break;
            case 2:
                pipelineAsset.msaaSampleCount = 4;
                break;
            case 3:
                pipelineAsset.msaaSampleCount = 8;
                break;
        }

        // Apply shadow quality setting
        switch (selectedShadowQuality)
        {
            case 0: // Low
                pipelineAsset.shadowDistance = 30f;
                pipelineAsset.shadowCascadeCount = 1;
                break;
            case 1: // Medium
                pipelineAsset.shadowDistance = 50f;
                pipelineAsset.shadowCascadeCount = 2;
                break;
            case 2: // High
                pipelineAsset.shadowDistance = 100f;
                pipelineAsset.shadowCascadeCount = 4;
                break;
        }

        // Apply render scale
        pipelineAsset.renderScale = renderScale;

        // Apply HDR support based on quality
        pipelineAsset.supportsHDR = selectedQualityLevel > 2;
    }

    // Method to toggle the menu visibility
    public void ToggleGraphicsMenu()
    {
        showGraphicsMenu = !showGraphicsMenu;
    }
}
