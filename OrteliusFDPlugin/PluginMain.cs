using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;
using Ortelius.Resources;
using PluginCore.Localization;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore.Helpers;
using PluginCore;

namespace Ortelius
{
	public class PluginMain : IPlugin
	{
        private String pluginName = "Ortelius";
        private String pluginGuid = "8b67c2e4-b14e-4751-b48e-3f9c6e1e694b";
        private String pluginHelp = "ortelius.marten.dk";
        private String pluginDesc = "Run Ortelius";
        private String pluginAuth = "Marten Olgaard";
        private String settingFilename;
        private Settings settingObject;
        private Image pluginImage;

	    #region Required Properties

        /// <summary>
        /// Name of the plugin
        /// </summary> 
        public String Name
		{
			get { return this.pluginName; }
		}

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public String Guid
		{
			get { return this.pluginGuid; }
		}

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public String Author
		{
			get { return this.pluginAuth; }
		}

        /// <summary>
        /// Description of the plugin
        /// </summary> 
        public String Description
		{
			get { return this.pluginDesc; }
		}

        /// <summary>
        /// Web address for help
        /// </summary> 
        public String Help
		{
			get { return this.pluginHelp; }
		}

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public Object Settings
        {
            get { return this.settingObject; }
        }
		
		#endregion
		
		#region Required Methods
		
		/// <summary>
		/// Initializes the plugin
		/// </summary>
		public void Initialize()
		{
            this.InitBasics();
            this.LoadSettings();
            this.AddEventHandlers();
            this.InitLocalization();
            this.CreateMenuItem();
        }
		
		/// <summary>
		/// Disposes the plugin
		/// </summary>
		public void Dispose()
		{
            this.SaveSettings();
		}
		
		/// <summary>
		/// Handles the incoming events
		/// </summary>
		public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority)
		{
            switch (e.Type)
            {
                

                // Catches Project change event and display the active project path
                case EventType.Command:
                    string cmd = (e as DataEvent).Action;
                    if (cmd == "ProjectManager.Project")
                    {
                        IProject project = PluginBase.CurrentProject;
                        
                    }
                    break;
            }
		}
		
		#endregion

        #region Custom Methods
        
        /// <summary>
        /// Opens the plugin panel if closed
        /// </summary>
        public void JumpToOrtelius(Object sender, System.EventArgs e)
        {
        	IProject project = PluginBase.CurrentProject;
        	string processParams = "";
        	if (project != null){
        		string folderName = Path.GetDirectoryName(project.ProjectPath);        	
        		processParams += "/folder:"+folderName;
        		processParams += " /destination:"+Path.GetFullPath( Path.Combine( folderName, this.settingObject.DestinationPath ) );
        		processParams +=" /name:"+project.Name;        		
        	}
        	
        	if(File.Exists(this.settingObject.OrteliusPath)) System.Diagnostics.Process.Start(this.settingObject.OrteliusPath,processParams);
        	else MessageBox.Show("The parh to Ortelius.exe are not correct.\nChange the path in Program Settings (F10)>\nMake sure you have installed Ortelius, which is not a part of the plugin","The path are wrong",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        
       
        /// <summary>
        /// Initializes important variables
        /// </summary>
        public void InitBasics()
        {
            String dataPath = Path.Combine(PathHelper.DataDir, "Ortelius");
            if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
            this.settingFilename = Path.Combine(dataPath, "Settings.fdb");
            
            Assembly _assembly = Assembly.GetExecutingAssembly();
			Stream _imageStream = _assembly.GetManifestResourceStream("ikon");
			Bitmap theDefaultImage = new Bitmap(_imageStream);

            this.pluginImage = theDefaultImage;
        }

        /// <summary>
        /// Adds the required event handlers
        /// </summary> 
        public void AddEventHandlers()
        {
            // Set events you want to listen (combine as flags)
            EventManager.AddEventHandler(this, EventType.FileSwitch | EventType.Command);
        }

        /// <summary>
        /// Initializes the localization of the plugin
        /// </summary>
        public void InitLocalization()
        {
            LocaleVersion locale = PluginBase.MainForm.Settings.LocaleVersion;
            switch (locale)
            {
               
                default : 
                    // Plugins should default to English...
                    LocaleHelper.Initialize(LocaleVersion.en_US);
                    break;
            }
            this.pluginDesc = LocaleHelper.GetString("Info.Description");
        }

        /// <summary>
        /// Creates a menu item for the plugin and adds a ignored key
        /// </summary>
        public void CreateMenuItem()
        {
            ToolStripMenuItem viewMenu = (ToolStripMenuItem)PluginBase.MainForm.FindMenuItem("ToolsMenu");
            viewMenu.DropDownItems.Add(new ToolStripMenuItem(LocaleHelper.GetString("Label.ViewMenuItem"), this.pluginImage, new EventHandler(this.JumpToOrtelius), this.settingObject.OrteliusShortcut));
            PluginBase.MainForm.IgnoredKeys.Add(this.settingObject.OrteliusShortcut);
        }


        /// <summary>
        /// Loads the plugin settings
        /// </summary>
        public void LoadSettings()
        {
            this.settingObject = new Settings();
            if (!File.Exists(this.settingFilename)) this.SaveSettings();
            else
            {
                Object obj = ObjectSerializer.Deserialize(this.settingFilename, this.settingObject);
                this.settingObject = (Settings)obj;
            }
        }

        /// <summary>
        /// Saves the plugin settings
        /// </summary>
        public void SaveSettings()
        {
            ObjectSerializer.Serialize(this.settingFilename, this.settingObject);
        }

		#endregion



	}
	
}
