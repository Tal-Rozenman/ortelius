using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text;

using System.Reflection;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Saxon.Api;


namespace Ortelius
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		private static string startPath="";
		private static string startName="";
		private static string destinationPath="";
		
		[STAThread]
		public static void Main(string[] args)
		{
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			
		}
		
		public MainForm()
		{
			InitializeComponent();			
			InitStuff();
		}
		
		bool changeFlag = false;
		ProjectSettings projSettings;
		GenerelSettings appSettings;
		DocumentationBuilder asDocumentation;
		
		private string systemSvar ="";
		
		private XmlDocument allDocXml;
		
		//Initialize
		void InitStuff()
		{
			projSettings = new ProjectSettings();
			appSettings = new GenerelSettings();
			
			populateStyleCombo();
			changeFlag = false;
			newVersion.Visible = false;	
			//checkForUpdates();
			
		}
		/// <summary>
		/// Checking if there is a new version available
		/// </summary>
		private void checkForUpdates(){
			Assembly asm = Assembly.GetExecutingAssembly();
			string version = asm.GetName().Version.ToString();
			versionLabel.Text = version;
			string url = "http://ortelius.marten.dk/latest_version.aspx?version="+version;
			string result = null;
			
			try
			{
			    WebClient client = new WebClient();
				result = client.DownloadString( url );
				if(result!=version){
					newVersion.Text = "Version "+result+" is available";
					newVersion.Visible = true;	
				}
			}
			catch(Exception ex)
			{
			    //MessageBox.Show( ex.Message );
			}
		}
		
		void populateStyleCombo(){
			
			String[] Files = Directory.GetFileSystemEntries(Path.GetDirectoryName(Application.ExecutablePath)+"/styles");
			foreach(string Element in Files){
				if(File.Exists(Element)){
					if(Path.GetExtension(Element)==".xsl"){
						styleCB.Items.Add(Path.GetFileNameWithoutExtension(Element));
					}
				}
			}
			int basicIndex = styleCB.FindString("OrteliusFrame");
			if(basicIndex == -1) basicIndex = 0;
			styleCB.SelectedIndex = basicIndex;
		}
		
		#region Add files
	
		
		///<summary>
		/// Starter importen
		///</summary>
		private void AddASFile(object sender, System.EventArgs e)
		{
			
			string fileName = "";
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			if( projSettings.LastFolderName=="")openFileDialog1.InitialDirectory = "Desktop" ;
			else openFileDialog1.InitialDirectory = projSettings.LastFolderName;
			openFileDialog1.Filter = "ActionScript files(*.as)|*.as" ;	
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;
			openFileDialog1.Title = "Add ActionScript file ";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				projSettings.LastFolderName = Path.GetDirectoryName(openFileDialog1.FileName);
				if(openFileDialog1.FileName!= null){
					fileName = openFileDialog1.FileName;
					if(projSettings.AddAsFile(fileName)){
						changeFlag = true;
						renderList();
					}
					else if (Path.GetExtension(fileName) != ".as") MessageBox.Show("Wrong file format");
				}
			}
		}
		
		///<summary>
		/// Make the list of as files
		///</summary>
		private void renderList()
		{
			listBox1.BeginUpdate();			
			listBox1.Items.Clear();
			foreach ( string filNavn in projSettings.AllASFiles ){
				listBox1.Items.Add(filNavn);
			}
			listBox1.EndUpdate();
		}
		
		#endregion
			
				
		#region Build documentation	
		void BuildDocumentationToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			
			if(projSettings.DestinationPath == "" || !Directory.Exists(projSettings.DestinationPath)){
				MessageBox.Show("You need to select a destination folder","No destination folder", MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			allDocXml = new XmlDocument();
			
			systemSvar = "";
			BuildButton.Enabled = false;
			progressBar1.Enabled = true;
			progressBar1.Value = 2;
			progressBar1.Refresh();
			
			BuildDocumentationXml();
						
			progressBar1.Value = 40;
			progressBar1.Refresh();
			
			SaveXml();				
			progressBar1.Value = 65;
			progressBar1.Refresh();
			
			
			createHtmlFromXsl();
			progressBar1.Value = 90;
			progressBar1.Refresh();
			
						
			copyExtraFiles();			
			progressBar1.Value = 100;
			progressBar1.Refresh();
			
			try{
				File.Delete(projSettings.DestinationPath+projSettings.DocXmlFileName);
			}catch(Exception){
			}
			
			string endMessage = "The build is now finished";
			if( systemSvar != ""){
				MessageBox.Show(endMessage+ "\n\nDuring the build a number of errors occured.\nYou can find details in the error.log file","Build finished, but with errors", MessageBoxButtons.OK,MessageBoxIcon.Warning);
				
				StreamWriter skrivObjekt = new StreamWriter(projSettings.DestinationPath+"/errors.log", false,System.Text.Encoding.UTF8);
				skrivObjekt.Write(systemSvar);
				skrivObjekt.Close();
		
				
			}else if(!showAfterBuildCB.Checked ) MessageBox.Show(endMessage,"Build finished", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
			
			progressBar1.Value = 0;
			progressBar1.Refresh();
			
			BuildButton.Enabled = true;
			progressBar1.Enabled = false;
			
			if(showAfterBuildCB.Checked ) {
				string resultDoc = projSettings.DestinationPath+projSettings.DocHtmlFileName;
				System.Diagnostics.Process.Start(resultDoc);
			}
			
		}
		
		///<summary>
		/// Make the list of as files
		///</summary
		private void BuildDocumentationXml()
		{
			
			DataTypeUtil allDataTypes = DataTypeUtil.Instance;
			XmlDeclaration dec = allDocXml.CreateXmlDeclaration("1.0", null, null);
			allDocXml.AppendChild(dec);
			
			XmlElement contentNode = allDocXml.CreateElement("docElements");
			allDocXml.AppendChild(contentNode);
						
			XmlElement pathNode = allDocXml.CreateElement("basePath");
			pathNode.InnerText = "file:/"+xmlPath.Text.Replace("\\","/")+"/";
			contentNode.AppendChild(pathNode);
			
			XmlElement newNode1 = allDocXml.CreateElement("introHeader");
			newNode1.InnerText = introHeader.Text;
			contentNode.AppendChild(newNode1);
			  
		 
    		XmlElement newNode2 = allDocXml.CreateElement("introText");
			contentNode.AppendChild(newNode2);
			XmlCDataSection CData = allDocXml.CreateCDataSection(introText.Text.Replace("\r\n","<br/>\r\n"));
		    newNode2.AppendChild(CData);
			
			XmlElement createdNode = allDocXml.CreateElement("created");
			createdNode.InnerText = String.Format("{0:d/M yyyy}", DateTime.Now);
			contentNode.AppendChild(createdNode);
			
			asDocumentation = new DocumentationBuilder();
			
			//loop trough all files
			int incFiles = 0;
			foreach ( string filNavn in projSettings.AllASFiles ){
				if(File.Exists(filNavn)){
				incFiles++;
				progressBar1.Value = (incFiles/projSettings.AllASFiles.Count)*38+2;				
				progressBar1.Refresh();
				DateTime modifiedTime = File.GetLastWriteTime(filNavn);
				
				string classXml = asDocumentation.AddClass(File.ReadAllLines(filNavn, Encoding.Default),modifiedTime);
				try{
					if(classXml != "" && classXml != null){
						XmlElement classNode = allDocXml.CreateElement("class");
						classNode.InnerXml = classXml;
						//add datatype to list
						allDataTypes.AddDataType(classNode.SelectSingleNode("name").InnerText,classNode.SelectSingleNode("package").InnerText,null);//,classNode.SelectSingleNode("fid").InnerText
						contentNode.AppendChild(classNode);
					}else{
						systemSvar += "File not added (no content): "+filNavn+"\r\n\r\n";
						asDocumentation.SystemSvar = "";
					}
				}
				catch(Exception e){
					systemSvar +="Exception error in: "+filNavn+"\r\n"+e.ToString()+"\r\n\r\n" ;
				}
				
				if(asDocumentation.SystemSvar!=""){
					systemSvar += "Error in "+filNavn+": "+asDocumentation.SystemSvar+"\r\n\r\n";
					asDocumentation.SystemSvar = "";
				}
			}
			}
			
		}
		
		///<summary>
		/// Make the list of as files
		///</summary
		private void SaveXml()
		{
			XmlRestructure docXmlRestructure = new XmlRestructure(allDocXml);
			
			
			docXmlRestructure.UpdateSetterGetters();			
			progressBar1.Value = 45;
			progressBar1.Refresh();
			
			docXmlRestructure.CreateInheritedElements();			
			progressBar1.Value = 50;
			progressBar1.Refresh();
			
			docXmlRestructure.CreateNestedPackages();			
			progressBar1.Value = 55;
			progressBar1.Refresh();
			
			docXmlRestructure.CreateClassIndex();			
			progressBar1.Value = 57;
			progressBar1.Refresh();
			
			docXmlRestructure.UpdateTypesFullpath();			
			progressBar1.Value = 61;
			progressBar1.Refresh();
			
			
			systemSvar +=docXmlRestructure.Errors;
				
			try{
			StreamWriter skrivObjekt = new StreamWriter(projSettings.DestinationPath+projSettings.DocXmlFileName, false,System.Text.Encoding.UTF8);
			skrivObjekt.Write(docXmlRestructure.DocumentationXml.OuterXml );
			skrivObjekt.Close();		
			}
			catch(Exception){
				systemSvar +="\r\nCouldn't write XML file";
			}
			
			progressBar1.Value = 65;
			progressBar1.Refresh();
					
		}
		
		
		void copyExtraFiles()
		{
			string destPath = projSettings.DestinationPath+"/"+projSettings.StyleName;
			string srcPath = Path.GetDirectoryName(Application.ExecutablePath)+"/styles/"+projSettings.StyleName;
			if(Directory.Exists(srcPath)) copyDirectory(srcPath,destPath);
		}
		
		 void copyDirectory(string Src,string Dst){
			String[] Files;
			
			if(Dst[Dst.Length-1]!=Path.DirectorySeparatorChar)
			Dst+=Path.DirectorySeparatorChar;
			if(!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
			Files=Directory.GetFileSystemEntries(Src);
			foreach(string Element in Files){
				// Sub directories
				if(Directory.Exists(Element) && Element.IndexOf(".svn")==-1)
				copyDirectory(Element,Dst+Path.GetFileName(Element));
				// Files in directory
				else
					try{
				File.Copy(Element,Dst+Path.GetFileName(Element),true);
				}
				catch(Exception){}
			}
		}
		
		
		void createHtmlFromXsl(){
			string resultDoc = projSettings.DestinationPath+projSettings.DocHtmlFileName;
			if(projSettings.StyleName == "OrteliusXml") resultDoc = projSettings.DestinationPath+"/orteliusXml.xml";
			string xmlDoc = projSettings.DestinationPath+projSettings.DocXmlFileName;
			string xslDoc = Path.GetDirectoryName(Application.ExecutablePath)+"/styles/"+projSettings.StyleName+".xsl";
						
			try{
				Processor processor = new Processor();
				System.IO.StreamReader reader = new System.IO.StreamReader(xmlDoc, System.Text.Encoding.UTF8);
				System.IO.TextWriter stringWriter = new System.IO.StringWriter();
				
				stringWriter.Write(reader.ReadToEnd());
				stringWriter.Close();
				
				reader.Close();
				
			
				System.IO.TextReader stringReader = new System.IO.StringReader(stringWriter.ToString());
				System.Xml.XmlTextReader reader2 = new System.Xml.XmlTextReader(stringReader);
				reader2.XmlResolver = null;
				
				// Load the source document
				XdmNode input = processor.NewDocumentBuilder().Build(reader2);
			
				// Create a transformer for the stylesheet.
				XsltTransformer transformer = processor.NewXsltCompiler().Compile(new System.Uri(xslDoc)).Load();
				transformer.InputXmlResolver = null; 
				
				// Set the root node of the source document to be the initial context node				
				transformer.InitialContextNode = input;
								
				// Create a serializer				
				Serializer serializer = new Serializer();
								
				serializer.SetOutputFile(resultDoc);

				// Transform the source XML to System.out.				
				transformer.Run(serializer);
			}
			catch(Exception e){
				
				//MessageBox.Show(e.ToString());
				systemSvar += "Error in xslt rendering:\r\n"+e.ToString();
			}
			
		}
		
		
		#endregion
		
		
		#region Save and load settings and projects
		
		void ChooseDestination(object sender, EventArgs e)
		{			
			
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			
			if(xmlPath.Text!="" && xmlPath.Text!= null) folderDialog.SelectedPath = xmlPath.Text;
			if(folderDialog.ShowDialog() == DialogResult.OK)
			{
				if(folderDialog.SelectedPath != null){				
					changeFlag = true;					
					projSettings.DestinationPath = folderDialog.SelectedPath;
					xmlPath.Text = projSettings.DestinationPath;
				}
			}
			
		}
		
		
		
		void SaveProjectToolStripMenuItemClick(object sender, EventArgs e)
		{
			saveProject(true);
		}
		
		void saveProject(bool saveAsFlag){
			
			changeFlag = false;
			if(saveAsFlag || appSettings.CurrentProject == ""){
				SaveFileDialog fileDialog = new SaveFileDialog();
								
					
				if(appSettings.CurrentProject != ""){
					fileDialog.FileName = appSettings.CurrentProject;
				}
				else fileDialog.InitialDirectory = "Desktop" ;
				fileDialog.Filter = "Ortelius project files(*.orp)|*.orp" ;	
				//fileDialog.FilterIndex = 1 ;
				fileDialog.Title = "Ortelius file";
				
				if(fileDialog.ShowDialog() == DialogResult.OK)
				{
					if(fileDialog.FileName!= null)
					{
						appSettings.CurrentProject = fileDialog.FileName;
						saveProjectSettings();
						
					}
					
				}
				}else{
				saveProjectSettings();
			}
		}
		
		
		///<summary>
		///
		///</summary>
		void saveProjectSettings(){
			if(appSettings.CurrentProject=="") return;
			try{
			FileStream myStream = new FileStream(appSettings.CurrentProject,FileMode.OpenOrCreate,FileAccess.Write);
			BinaryFormatter binSkriver = new BinaryFormatter();
			binSkriver.Serialize(myStream,projSettings);
			myStream.Close();
			}catch(Exception){
				MessageBox.Show("Project couldn't be saved at: "+appSettings.CurrentProject,"Project couldn't be saved");
			}
		}
		
		
			void LoadProjectToolStripMenuItemClick(object sender, EventArgs e)
		{	
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.InitialDirectory = "Desktop" ;
			openFileDialog1.Filter = "Ortelius project files(*.orp)|*.orp" ;	
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;
			openFileDialog1.Title = "Project file";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if(openFileDialog1.FileName!= null)
				{	
					appSettings.CurrentProject = openFileDialog1.FileName;
					LoadProject();
					
				}
				
			}	
			
		}
		
		void LoadProject(){
			
				
				loadProjectSettings();
					renderList();
					xmlPath.Text = projSettings.DestinationPath;
					int basicIndex = styleCB.FindString(projSettings.StyleName);
					if(basicIndex != -1){
						styleCB.SelectedIndex = basicIndex;
					}
					
		}
			
			
		///<summary>Gemmer indstillings hashtablen i en fil</summary>
		void loadProjectSettings(){
			
			if(File.Exists(appSettings.CurrentProject)){
				//opret stream
				FileStream myStream=new FileStream(appSettings.CurrentProject,FileMode.Open,FileAccess.Read);
				myStream.Seek(0,SeekOrigin.Begin);
				BinaryFormatter binLaeser=new BinaryFormatter();
				
				ProjectSettings tempSettings = projSettings;
				
				try{
					projSettings = (ProjectSettings) binLaeser.Deserialize(myStream);
					introHeader.Text = projSettings.IntroHeader;
					introText.Text = projSettings.IntroText;
					
					//MessageBox.Show(projSettings.ShowAfterBuild.ToString());
					showAfterBuildCB.Checked = projSettings.ShowAfterBuild;
					changeFlag = false;
				}
				catch(Exception){
					projSettings = tempSettings;
					MessageBox.Show("The file hasent the right format","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
				myStream.Close();
			}
			
		}
		
		
	
		
		void SaveSettings()
		{
			string settingFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\Ortelius";
			
			//string settingFile = Path.GetDirectoryName(Application.ExecutablePath)+"/settings.ors";
			if(!Directory.Exists(settingFile)){
				Directory.CreateDirectory(settingFile);
			}
				
			settingFile += @"\settings.ors";
			FileStream myStream = new FileStream(settingFile,FileMode.OpenOrCreate,FileAccess.Write);
			BinaryFormatter binSkriver = new BinaryFormatter();
			binSkriver.Serialize(myStream,appSettings);
			myStream.Close();
		}
		
		void LoadSettings(){
			string settingFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\Ortelius\settings.ors";
			
			if(File.Exists(settingFile)){
				try{
				FileStream myStream=new FileStream(settingFile,FileMode.Open,FileAccess.Read);
				myStream.Seek(0,SeekOrigin.Begin);
				BinaryFormatter binLaeser=new BinaryFormatter();
				appSettings = (GenerelSettings) binLaeser.Deserialize(myStream);
				myStream.Close();
					LoadProject();
				}
				catch(Exception e){
					MessageBox.Show(e.ToString());
				}					
			}
		}
		
		void CloseToolStripMenuItemClick(object sender, EventArgs e)
		{
			string saveWarning = "Do you want to save the current changes?";
			if(changeFlag){
				DialogResult saveAnswer = MessageBox.Show(saveWarning,"Save changes?",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
				if( saveAnswer == DialogResult.Cancel) return;
				else if( saveAnswer == DialogResult.Yes){
					saveProject(false);
				}
			}
			
			this.Close();
			
		}
		
		void OrteliusClosing(object sender, FormClosingEventArgs e)
		{
			try{
				SaveSettings();
			}catch(Exception){}
		}
		
		
		void NewProjectToolStripMenuItemClick(object sender, EventArgs e)
		{
			string saveWarning = "Do you want to continue without saving your changes?";
			if(changeFlag){
				if(MessageBox.Show(saveWarning,"Confirm", MessageBoxButtons.YesNo) == DialogResult.No)return;
			}
			
			projSettings.ResetProjectSettings();
			xmlPath.Text = "";
			appSettings.CurrentProject = "";
			introText.Text = "";
			introHeader.Text = "";	
			populateStyleCombo();
			renderList();
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			saveProject(false);
		}
		
		#endregion
		
		//remove class
		void RemoveClass(object sender, System.EventArgs e)
		{
			doRemoveClass();
		}
		//remove class
		private void doRemoveClass(){
			int lowInd = listBox1.Items.Count-1;
			ListBox.SelectedIndexCollection allSIndices = listBox1.SelectedIndices;
			foreach(int selectInd in allSIndices){
				//int selectInd = listBox1.SelectedIndex;
				if( selectInd > -1){	
					projSettings.RemoveAsFile(listBox1.Items[selectInd].ToString());
					if(lowInd > selectInd) lowInd =selectInd;
				
				}
			}
			renderList();
			if(lowInd>=listBox1.Items.Count) lowInd = listBox1.Items.Count-1;
			if(lowInd > 0) listBox1.SelectedIndex = lowInd;
		}
		
		private void ClassList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e){	
			if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)	{		
				doRemoveClass();
			}else if(e.KeyCode == Keys.A && e.Control){
				for (int i = 0; i < listBox1.Items.Count; i++)listBox1.SetSelected(i,true);

			}
		}
		
		//add folder
		void AddFolder(object sender, System.EventArgs e)
		{
			
			
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			
			if( projSettings.LastFolderName=="")folderDialog.SelectedPath = "Desktop";
			else folderDialog.SelectedPath = projSettings.LastFolderName;
			
			
			if(folderDialog.ShowDialog() == DialogResult.OK)
			{
				if(folderDialog.SelectedPath != null){
					changeFlag = true;
					projSettings.LastFolderName = folderDialog.SelectedPath;
					addFolderFiles(folderDialog.SelectedPath);
				}
			}
			
			renderList();
		}
		
		
					
			
		void addFolderFiles(string asPath){	
			
			String[] Files = Directory.GetFileSystemEntries(asPath);
			foreach(string Element in Files){
				if(Directory.Exists(Element) )addFolderFiles(Element);
				else{
					if(projSettings.AddAsFile(Element)) changeFlag = true;
					
				}
			}
		}
		
		//load
		void MainFormLoad(object sender, EventArgs e){
			
			
			
			//if Ortelius has been opened by an assoiacted file (.orp) then open the content of this file
			//or if it has been opened with commandline arguments (e.g. from flashdevelop)
			string[] args = Environment.GetCommandLineArgs();
			
			string paramName = "";
			string delimeter = "";
			bool loadSettingsTest = true;
		
			foreach (string param in args){
				if(param.IndexOf("/folder:")== 0 || param.IndexOf("/name:")== 0 || param.IndexOf("/destination:")== 0) paramName = "";
					//MessageBox.Show(paramName +" - "+param);
				if(param.IndexOf(".orp")!= -1 && param.IndexOf(".orp")== param.Length-4){
				//	MessageBox.Show("Orp: "+param);
					appSettings.CurrentProject = param;
					LoadProject();
					loadSettingsTest = false;
				}
				else if(param.IndexOf("/folder:")== 0 || paramName=="/folder:"){
//					MessageBox.Show("Folder: "+param);
					delimeter = (paramName == "/folder:") ? " ":"";
					startPath += delimeter+param.Replace("/folder:","");
					paramName = "/folder:";
				}
				else if(param.IndexOf("/name:")== 0 || paramName=="/name:"){
//					MessageBox.Show("Name: "+param);						
					delimeter = (paramName == "/name:") ? " ":"";
					startName += delimeter+param.Replace("/name:","");
					paramName = "/name:";
				}else if(param.IndexOf("/destination:")== 0 || paramName=="/destination:"){	
//					MessageBox.Show("Destination: "+param);					
					delimeter = (paramName == "/destination:") ? " ":"";
					destinationPath += delimeter+param.Replace("/destination:","");
					paramName = "/destination:";
				}else{
//					MessageBox.Show("Nothing: "+param);
					paramName = "";
				}
				

			}
		
			if(startPath!=""){
				loadSettingsTest = false;
				
				if(destinationPath== "") destinationPath = startPath+"\\documentation";
				projSettings.DestinationPath = destinationPath;
				xmlPath.Text = projSettings.DestinationPath;
				projSettings.LastFolderName = startPath;
				
				
				if(!Directory.Exists(projSettings.DestinationPath)){
					Directory.CreateDirectory(projSettings.DestinationPath);
					appSettings.CurrentProject = projSettings.DestinationPath+"/documentation.orp";
					changeFlag = true;
				}
				else {
					//look for orp file
					String[] Files = Directory.GetFileSystemEntries(projSettings.DestinationPath);
					string orpFile = "";
					foreach(string Element in Files){
				//		MessageBox.Show("Element: "+Element);
						if(Element.IndexOf(".orp") != -1 ){
							orpFile = Element;
							
						}
					}
					if(orpFile==""){
						appSettings.CurrentProject = projSettings.DestinationPath+"/documentation.orp";
						changeFlag = true;
						
					}else{
						appSettings.CurrentProject = orpFile;
						LoadProject();
					}

				}
				
				
				addFolderFiles(startPath);
				renderList();			
				
			}
			
			if(startName!=null && (introHeader.Text==null || introHeader.Text== "")){
				introHeader.Text = startName;
				projSettings.IntroHeader = introHeader.Text;
				changeFlag = true;
			}
			
			if(loadSettingsTest) LoadSettings();
			
			if(changeFlag){
				
				if(appSettings.CurrentProject !="") saveProjectSettings();
				changeFlag = false;
			}
		}
		
				
		void StyleCBSelectedIndexChanged(object sender, System.EventArgs e)
		{
			projSettings.StyleName = styleCB.Items[styleCB.SelectedIndex].ToString();
		}
		
				
		void MinimizeForm(object sender, EventArgs e)
		{
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
		}
		
		
		private Point mouseOffset;
		private bool isMouseDown = false;
		
		private void frm_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e)
		{
		int xOffset;
		int yOffset;
		    if (e.Button == MouseButtons.Left)
		    {
			    xOffset = Location.X-Control.MousePosition.X;//-e.X - SystemInformation.FrameBorderSize.Width;
			    yOffset = Location.Y-Control.MousePosition.Y;//e.Y ;
			
			    mouseOffset = new Point(xOffset, yOffset);
			    isMouseDown = true;
		    }
		}

		private void frm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		    if (isMouseDown)
		    {
		        Point mousePos = Control.MousePosition;
		        mousePos.Offset(mouseOffset.X, mouseOffset.Y);
		        Location = mousePos;
		    }
		}
		
		private void FrmGlavna_MouseUp(object sender, 
		System.Windows.Forms.MouseEventArgs e)
		{
		    if (e.Button == MouseButtons.Left)
		    {
		        isMouseDown = false;
		    }
		}
		
		
		
		void PictureBox3Click(object sender, EventArgs e)
		{
			openSite();
		}
		
		private void openSite(){
			string target= "http://ortelius.marten.dk";
		    try{
		         System.Diagnostics.Process.Start(target);
		        }
		    catch (System.ComponentModel.Win32Exception noBrowser) 
		        {
		         if (noBrowser.ErrorCode==-2147467259)
		          MessageBox.Show(noBrowser.Message);
		        }
		    catch (System.Exception other)
		        {
		          MessageBox.Show(other.Message);
		        }
		}
		
		
		void IntroTextTextChanged(object sender, EventArgs e)
		{
			changeFlag = true;			
			projSettings.IntroText = introText.Text;
		}
		
		void IntroHeaderTextChanged(object sender, EventArgs e)
		{
			changeFlag = true;
			projSettings.IntroHeader = introHeader.Text;
			
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			changeFlag = true;
			projSettings.ShowAfterBuild = showAfterBuildCB.Checked;
			
		}
		
		void GoToDocumentationToolStripMenuItemClick(object sender, EventArgs e)
		{
			string resultDoc = projSettings.DestinationPath+projSettings.DocHtmlFileName;
			System.Diagnostics.Process.Start(resultDoc);
		}
		

		
		void NewVersionClick(object sender, EventArgs e)
		{
			openSite();
		}
		
		void MainFormShown(object sender, EventArgs e)
		{
			checkForUpdates();
		}
		
		
		
		void DonateToolStripMenuItemClick(object sender, EventArgs e)
		{
			string target= "http://ortelius.marten.dk/donate.aspx";
		    try{
		         System.Diagnostics.Process.Start(target);
		        }
		    catch (System.ComponentModel.Win32Exception noBrowser) 
		        {
		         if (noBrowser.ErrorCode==-2147467259)
		          MessageBox.Show(noBrowser.Message);
		        }
		    catch (System.Exception other)
		        {
		          MessageBox.Show(other.Message);
		        }
		}
		
	
		
	}
	
	
	
}

