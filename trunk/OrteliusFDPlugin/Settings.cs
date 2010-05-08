using System.Windows.Forms;
using System;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using PluginCore.Localization;


namespace Ortelius
{
    [Serializable]
    public class Settings
    {
        private String orteliusPath = @"C:\Program Files\Ortelius\Ortelius.exe";
        private String destinationPath = @"./documentation/";
        private Keys orteliusShortcut = Keys.F9;
        
        /// <summary> 
        /// Get and sets the path to Ortelius.exe
        /// </summary>
        [DisplayName("Path to Ortelius.exe")]       
        [Description("The path to the exe file of Ortelius."), DefaultValue(@"C:\Program Files\Ortelius\Ortelius.exe")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public String OrteliusPath 
        {
            get { return this.orteliusPath; }
            set { this.orteliusPath = value; }
        }

       /// <summary> 
        /// Relative path to documentation folder
        /// </summary>
        [DisplayName("Relative path to documentation")] 
        [Description("Relative path to where the documentation folder should be placed."), DefaultValue(@"./documentation")]
        public String DestinationPath 
        {
            get { return this.destinationPath; }
            set { this.destinationPath = value; }
        }

        /// <summary> 
        /// Get and sets the sampleShortcut
        /// </summary>
        [Description("Ortelius shortcut setting"), DefaultValue(Keys.F9)]
        public Keys OrteliusShortcut
        {
            get { return this.orteliusShortcut; }
            set { this.orteliusShortcut = value; }
        }

    }

}
