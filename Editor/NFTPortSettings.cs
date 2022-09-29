using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json; 
using UnityEditor.PackageManager;

namespace NFTPort.Editor
{ using Internal;
    [System.Serializable]
    public class NFTPortSettings : EditorWindow
    {
        
        static string myAPIString = PortConstants.DefaultAPIKey;
         
        protected static Type WindowType = typeof(NFTPortSettings);
        private static bool windowopen = false;
        private bool ranLatestrel = false;
        static PkgJson releasedPkgJson = null;
        static private bool NFTPortStarted;
        
        GUIStyle _horizontalGroup;
        GUIStyle _sampleButtonStyle;
        static int _selectedSample;
        
        
        [MenuItem("NFTPort/Home")]
        public static void ShowWindow()
        {
            var win = GetWindow<NFTPortSettings>(PortConstants.HomeWindowName);
            SetSize(win);
        }
        
        /// <summary>   
        /// When the package is imported
        /// </summary>
        [UnityEditor.InitializeOnLoadMethod]
        public static void InitializeOnLoadMethod()
        {
            Events.registeredPackages += RegisteredPackagesEventHandler;
            //EditorApplication.delayCall += delayCall;
        }

        static void RegisteredPackagesEventHandler(PackageRegistrationEventArgs packageRegistrationEventArgs)
        {
            ReadFromUserPrefs();

            InstallPortDependencies.OnListCheckCompleteForNewtonSoft(arg0 => DependencyAction(arg0));
            InstallPortDependencies.OnListCheckCompleteForGLTF(arg0 => DependencyAction(arg0));
            InstallPortDependencies.CheckPkgsListForNewtonsoft();
        }

        static void DependencyAction(bool exists)
        {
            if (exists)
            {
              
            }
            else
            {
                //InstallPortDependencies.ShowWindow(); //auto newtonsoft pkg dependency in unity 2021. // gltf only needed on sample scene, checks there.
            }
        }

        [InitializeOnLoad]
        public class Startup
        {  
            static Startup()
            {
                EditorApplication.update += AfterLoad;
            }

            static void AfterLoad() //first load once
            {
                NFTPortStarted = SessionState.GetBool("NFTPortStarted", false);
                if(!NFTPortStarted)
                {
                    ReadFromUserPrefs();
                    GetlatestRelease();
                    SessionState.SetBool("NFTPortStarted", true);
                } 
            }
        }
        

        static void  GetlatestRelease()
        {
            var ls = LatestRel.Initialize();
            if (ls != null)
            {
                ls.OnComplete(pkg => LatestReleaseCallback(pkg));
                ls.Run();
            }
        }

        static void LatestReleaseCallback(PkgJson pkg)
        {
            releasedPkgJson = pkg;

            if (SessionState.GetBool("FirstRelAfterFirstLoad", false) == false)
            {
                if (releasedPkgJson != null)
                {
                    string lv = releasedPkgJson.version;
                    string iv = PkgInfo.GetInstalledPackageVer();
                    if(lv!= iv)
                        Debug.Log("New NFTPort release is available : " + lv + " , your installed version is: " + iv + " , you may update via the Package Manager. View NFTPort/Home.");
                    SessionState.SetBool("FirstRelAfterFirstLoad", true);
                }
            }
        }

        
        void InitStyles()
        {
            _horizontalGroup = new GUIStyle();
            _horizontalGroup.padding = new RectOffset(0, 0, 4, 4);
            
            _sampleButtonStyle = new GUIStyle(GUI.skin.FindStyle("button"));
            _sampleButtonStyle.imagePosition = ImagePosition.ImageAbove;
            _sampleButtonStyle.padding = new RectOffset(10, 10, 15, 15);
            _sampleButtonStyle.fontStyle = FontStyle.Bold;
        }


        private bool apiIsPass = true;
        void OnGUI()
        {
            InitStyles();

            Texture banner = Resources.Load<Texture>("banner");
            GUIContent button_banner = new GUIContent(banner, "NFTPort.xyz");
            if (GUILayout.Button(button_banner, GUILayout.Width(530), GUILayout.Height(270)))
                Application.OpenURL(PortConstants.FeatureList);
            var hover = GUI.tooltip;
            
            Texture2D Portcursor = Resources.Load<Texture2D>("nftport_logo 1");
            if (hover == "NFTPort.xyz")
                Cursor.SetCursor(Portcursor, new Vector2(0,0),0);
                //EditorGUIUtility.AddCursorRect(new Rect(0, 0, 500, 500), MouseCursor.FPS);
            

            GUILayout.Label("Welcome to NFTPort Unity SDK ", EditorStyles.whiteLargeLabel);
            GUILayout.Label("\n" +
                            " Create cross-chain compatible NFT games \n and products in Unity with fast and reliable data.\n" +
                            "", EditorStyles.label);

            
            GUILayout.BeginHorizontal("box");
            var defaultColor = GUI.backgroundColor;

            if (myAPIString == PortConstants.DefaultAPIKey)
            {
                apiIsPass = false;
                GUI.color = UnityEngine.Color.cyan;
            }
            else
            {
                if(APIkeyOk())
                    GUI.color = UnityEngine.Color.green;
                else
                {
                    GUI.color = UnityEngine.Color.red;
                }
            }

            if (!apiIsPass)
            {
                if (GUILayout.Button("hide", GUILayout.Width(42)))
                    apiIsPass = true;
                
                myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString); 
            }
            else
            {
                if (GUILayout.Button("show", GUILayout.Width(42)))
                    apiIsPass = false;
                
                myAPIString = EditorGUILayout.PasswordField("APIKEY", myAPIString); 
            }
            

            GUI.color = defaultColor;
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Save API Key", GUILayout.Height(25)))
            {
                SaveChanges();
                apiIsPass = true;
            }
                

            GuiLine();
            
            EditorGUILayout.LabelField("");

            if (userModel != null)
            {
                EditorGUILayout.LabelField("   Welcome " + userModel.profile.name + "."); 
            }
            EditorGUILayout.LabelField("");
            
            GuiLine();
            
            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.Docs_GettingStarted);
            
            if (GUILayout.Button("Community & Support", GUILayout.Height(25)))
                Application.OpenURL(PortConstants.DiscordInvite);
            
            GuiLine();
            
            GUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("installed version: " + PkgInfo.GetInstalledPackageVer());

            if (!ranLatestrel)
            {
               GetlatestRelease();
               ranLatestrel = true;
            }

            if (releasedPkgJson != null)
            {
                EditorGUILayout.LabelField("Latest release version: " + releasedPkgJson.version);
            }

            GUILayout.EndHorizontal();

            var buttonHeight = 44;
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Star Github", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.Github);
            
            if (GUILayout.Button("Whats New", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.ChangeLog);
            
            if (GUILayout.Button("FAQ", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.FAQ);
            
            if (GUILayout.Button("Report a Bug or Request Feature", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.ReportABug);

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("My Stats", GUILayout.Height(buttonHeight)))
                Application.OpenURL(PortConstants.Dashboard);
            
            if (GUILayout.Button("View my IPFS Uploads", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.MyIPFS);
            
            if (GUILayout.Button("View my API NFTs", GUILayout.Height(buttonHeight)))
                Application.OpenURL(PortConstants.MyNFTs);
            
            GUILayout.EndHorizontal();
            
            /*
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Get More / Go Pro", GUILayout.Height(buttonHeight)))
                Application.OpenURL(PortConstants.ChangeSubscriptionPlan);
            
            if (GUILayout.Button("Change API Key", GUILayout.Height(buttonHeight))) 
                Application.OpenURL(PortConstants.ChangeAPIKeys);

            GUILayout.EndHorizontal();
            */
            
            GuiLine();
            
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Basic examples can be imported via package Manager, Explore more projects below:");
            EditorGUILayout.BeginHorizontal();
            
            Texture button_tex = (Texture)Resources.Load<Texture>("pg-gallery1");
            GUIContent button_tex_con = new GUIContent(button_tex);
            if (GUILayout.Button(button_tex_con,_sampleButtonStyle, GUILayout.Width(260), GUILayout.Height(100))) 
                Application.OpenURL(PortConstants.AdvPlaygroundGallery);

            Texture button_tex2 = (Texture)Resources.Load<Texture>("pg-gallery2");
            GUIContent button_tex_con2 = new GUIContent(button_tex2);
            if (GUILayout.Button(button_tex_con2,_sampleButtonStyle, GUILayout.Width(260), GUILayout.Height(100))) 
                Application.OpenURL(PortConstants.PlaygroundButterflyMint);
            
            EditorGUILayout.EndHorizontal();


        }

        private bool firstload = true;

        void OnEnable()
        {
            if (!windowopen)
            {
                if (!firstload)
                {
                    firstload = false;
                    return;
                    
                }
                else
                {
                    ReadFromUserPrefs();
                }
                
            }
            windowopen = true;
            ranLatestrel = false;
            HighLightReadmeAsset();
        }

        void HighLightReadmeAsset()
        {
            Selection.activeObject=AssetDatabase.LoadMainAssetAtPath("Packages/com.nftport.nftport/Runtime/Readme.asset");
        }

        private void OnDisable()
        {
            windowopen = false;
        }

        public override void SaveChanges()
        {
            WriteToUserPrefs();
            UserStats(); 
        }
        
        static void ShowHomeWindow()
        {
            if(windowopen)
                return;
            
            NFTPortSettings win = GetWindow(WindowType, false, PortConstants.HomeWindowName, true) as NFTPortSettings;
            if (win == null)
            {
                return;  
            }

            windowopen = true;
            SetSize(win);
            win.Show();
        }
        static void SetSize(NFTPortSettings win) 
        {
            win.minSize = new Vector2(530, 830);
            win.maxSize = new Vector2(530, 830);
        } 
        
        static void GuiLine( int i_height = 1 )
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new UnityEngine.Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
        
        #region ReadWrite UserPrefs
        
        private static PortUser.UserPrefs _userPrefs = new PortUser.UserPrefs();
        private static TextAsset targetFile;
        static void ReadFromUserPrefs()
        {
            targetFile = Resources.Load<TextAsset>("NFTPort UserPrefs");
            if (targetFile != null)
            {
                _userPrefs = JsonConvert.DeserializeObject<PortUser.UserPrefs>(targetFile.text);
                myAPIString = _userPrefs.API_KEY;
                PortUser.SetVersion(PkgInfo.GetInstalledPackageVer());
                PortUser.SaveNewApi(myAPIString);
                //PortUser.Initialise();
                UserStats();
            }
            else
            { 
                PortUser._initialised = false;
                myAPIString = PortConstants.DefaultAPIKey;
                if(!windowopen)
                    ShowHomeWindow();
            }

        }
        void WriteToUserPrefs()
        {
            PortUser.SetVersion(PkgInfo.GetInstalledPackageVer());
            PortUser.SaveNewApi(myAPIString);
            base.SaveChanges();
        }

        #endregion

        #region Userstats
        static User_model userModel;
        static void UserStats()
        {
            userModel = null;
            PortUser.SetFromAuto();
            User_Settings
                .Initialize(true)
                .OnError(usermodel=> StatsErrore())
                .OnComplete(usermodel=> userModel = usermodel)
                .Run();
        }

        static void StatsErrore()
        {
            if (!windowopen && !APIkeyOk())
            {
                ShowHomeWindow();
            }
        }
         

        static bool APIkeyOk()
        {
            if (userModel == null)
            {
                PortUser._initialised = false;
                return false;
            }

            if (userModel.response == "OK")
            {
                PortUser.Initialise();
                return true;
            }
            else
            {
                PortUser._initialised = false;
                return false;
            }
        }

        #endregion
         
        
    }
}

