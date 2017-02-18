/* Programmer: Josh Gernold
 * Date 1/31/17
 * 
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public GUISkin skin;

    private float gldepth = -0.5f;
    private float startTime = 0.1f;
    // reference to a material if want to use one
    public Material mat;
    // sets up the triangles and virtacles and fps bools
    private long tris = 0;
    private long verts = 0;
    private float savedTimeScale;

    private bool showFps;
    private bool showTris;
    private bool showVtx;
    private bool showFsgraph;
    // designates colors for fps
    public Color lowFPSColor = Color.red;
    public Color highFPSColor = Color.green;

    public int lowFPS = 30;
    public int highFPS = 50;

    public GameObject start;

    public Color statColor = Color.yellow;
    // displays credits and allows for more to be made
    public string[] credits = 
    {
		"A Viper Productions ",
		"Programming by Josh Gernold",
    };
    public Texture[] crediticons;

    public enum Page
    {
        None, Main, Options, Credits
    }

    private Page currentPage;

    private float[] fpsarray;
    private float fps;
    // sets up toolbar so it has the names of buttons
    private int toolbarInt = 0;
    private string[] toolbarstrings = { "Audio", "Graphics", "Stats", "System" };


    void Start()
    {   // creates the pause screen
        fpsarray = new float[Screen.width];
        Time.timeScale = 1;
        //PauseGame();
    }
    // loads up the differnt lengths of buttons 
    void OnPostRender()
    {
        if (showFsgraph && mat != null)
        {
            GL.PushMatrix();
            GL.LoadPixelMatrix();
            for (var i = 0; i < mat.passCount; ++i)
            {
                mat.SetPass(i);
                GL.Begin(GL.LINES);
                for (int x = 0; x < fpsarray.Length; ++x)
                {
                    GL.Vertex3(x, fpsarray[x], gldepth);
                }
                GL.End();
            }
            GL.PopMatrix();
            ScrollFPS();
        }
    }
    // an array of what the fps can be 
    void ScrollFPS()
    {
        for (int x = 1; x < fpsarray.Length; ++x)
        {
            fpsarray[x - 1] = fpsarray[x];
        }
        if (fps < 1000)
        {
            fpsarray[fpsarray.Length - 1] = fps;
        }
    }
    // creates the dashboard
    static bool IsDashboard()
    {
        return Application.platform == RuntimePlatform.OSXDashboardPlayer;
    }
    // if escape is pressed pauses the game
    void LateUpdate()
    {
        if (showFps || showFsgraph)
        {
            FPSUpdate();
        }

        if (Input.GetKeyDown("escape"))
        {
            switch (currentPage)
            {
                case Page.None:
                    PauseGame();
                    break;

                case Page.Main:
                    if (!IsBeginning())
                        UnPauseGame();
                    break;

                default:
                    currentPage = Page.Main;
                    break;
            }
        }
    }
    // breakes inbetween switching from main menu options and credits so no breaking happens
    void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;
        }
        ShowStatNums();
        if (IsGamePaused())
        {
            GUI.color = statColor;
            switch (currentPage)
            {
                case Page.Main: MainPauseMenu(); break;
                case Page.Options: ShowToolbar(); break;
                case Page.Credits: ShowCredits(); break;
            }
        }
    }

    // displays the different buttons and takes a break inbetwen
    void ShowToolbar()
    {
        BeginPage(300, 300);
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarstrings);
        switch (toolbarInt)
        {
            case 0: VolumeControl(); break;
            case 3: ShowDevice(); break;
            case 1: Qualities(); QualityControl(); break;
            case 2: StatControl(); break;
        }
        EndPage();
    }
    // displays credits 
    void ShowCredits()
    {
        BeginPage(300, 300);
        foreach (string credit in credits)
        {
            GUILayout.Label(credit);
        }
        foreach (Texture credit in crediticons)
        {
            GUILayout.Label(credit);
        }
        EndPage();
    }
    // back button in bottom corner
    void ShowBackButton()
    {
        if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20), "Back"))
        {
            currentPage = Page.Main;
        }
    }
    // lookes to see what type of GPU CPU is in pc
    void ShowDevice()
    {
        GUILayout.Label("Unity player version " + Application.unityVersion);
        GUILayout.Label("Graphics: " + SystemInfo.graphicsDeviceName + " " +
            SystemInfo.graphicsMemorySize + "MB\n" +
            SystemInfo.graphicsDeviceVersion + "\n" +
            SystemInfo.graphicsDeviceVendor);
        GUILayout.Label("Shadows: " + SystemInfo.supportsShadows);
        GUILayout.Label("Image Effects: " + SystemInfo.supportsImageEffects);
        GUILayout.Label("Render Textures: " + SystemInfo.supportsRenderTextures);
    }
    // displays teh differnt types of quality
    void Qualities()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                GUILayout.Label("Fastest");
                break;
            case 1:
                GUILayout.Label("Fast");
                break;
            case 2:
                GUILayout.Label("Simple");
                break;
            case 3:
                GUILayout.Label("Good");
                break;
            case 4:
                GUILayout.Label("Beautiful");
                break;
            default:
                GUILayout.Label("Fantastic");
                break;
        }
    }
    // displays buttons and functions them
    void QualityControl()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Decrease"))
        {
            QualitySettings.DecreaseLevel();
        }
        if (GUILayout.Button("Increase"))
        {
            QualitySettings.IncreaseLevel();
        }
        GUILayout.EndHorizontal();
    }
    // displays volume bar to adjust 
    void VolumeControl()
    {
        GUILayout.Label("Volume");
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
    }
    // shows fps and fps graph when clicked
    void StatControl()
    {
        GUILayout.BeginHorizontal();
        showFps = GUILayout.Toggle(showFps, "FPS");
        showFsgraph = GUILayout.Toggle(showFsgraph, "FPS Graph");
        GUILayout.EndHorizontal();
    }
    // updates fps while game is running
    void FPSUpdate()
    {
        float delta = Time.smoothDeltaTime;
        if (!IsGamePaused() && delta != 0.0)
        {
            fps = 1 / delta;
        }
    }
    // displays the stats numbers in top right corner
    void ShowStatNums()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 100, 10, 100, 200));
        if (showFps)
        {
            string fpsstring = fps.ToString("#,##0 fps");
            GUI.color = Color.Lerp(lowFPSColor, highFPSColor, (fps - lowFPS) / (highFPS - lowFPS));
            GUILayout.Label(fpsstring);
        }// ignore things below
        if (showTris || showVtx)
        {
            GUI.color = statColor;
        }
        if (showTris)
        {
            GUILayout.Label(tris + "tri");
        }
        if (showVtx)
        {
            GUILayout.Label(verts + "vtx");
        }
        GUILayout.EndArea();
    }
    // determines how big gui is when paused
    void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
    }

    void EndPage()
    {
        GUILayout.EndArea();
        if (currentPage != Page.Main)
        {
            ShowBackButton();
        }
    }

    bool IsBeginning()
    {
        return (Time.time < startTime);
    }

    // displays buttons to click on and traverse 
    void MainPauseMenu()
    {
        BeginPage(200, 200);
        if (GUILayout.Button(IsBeginning() ? "Play" : "Continue"))
        {
            UnPauseGame();

        }
        if (GUILayout.Button("Options"))
        {
            currentPage = Page.Options;
        }
        if (GUILayout.Button("Credits"))
        {
            currentPage = Page.Credits;
        }
        if (GUILayout.Button("Quit Game"))
        {
            Application.Quit();
        }
        EndPage();
    }
    // pauses time in game when game is paused and stops audio listeners and stops camara movbement
    void PauseGame()
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioListener.pause = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camMouseLook>().enabled = false;
        currentPage = Page.Main;
    }
    // resets time and audio listener and reactivates camera movement
    void UnPauseGame()
    {
        Time.timeScale = savedTimeScale;
        AudioListener.pause = false;
        currentPage = Page.None;

        if (IsBeginning() && start != null)
        {
            start.SetActive(true);
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camMouseLook>().enabled = true;
    }

    bool IsGamePaused()
    {
        return (Time.timeScale == 0);
    }

    void OnApplicationPause(bool pause)
    {
        if (IsGamePaused())
        {
            AudioListener.pause = true;
        }
    }
}