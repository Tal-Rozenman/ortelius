/*
 * Created by SharpDevelop.
 * User: marten
 * Date: 15-07-2011
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

namespace Ortelius
{
	/// <summary>
	/// Description of JSDocumentationBuilder.
	/// </summary>
	public class JSDocumentationBuilder : IDocumentationBuilder
	{
		private string systemSvar = "";
		public string SystemSvar
		{
	      get
	      {
	         return systemSvar;
	      }
	
	      set
	      {
	         systemSvar = value;
	      }
	   }
		private string startTag = "/**";
		private string endTag = "*/";
		private bool openClassTag = false;		
		
		private	Regex propertyPattern = new Regex(@"\s*\*\s*@\s*(property)");
		private	Regex methodPattern = new Regex(@"\s*\*\s*@\s*(method)");
		private	Regex classPattern = new Regex(@"\s*\*\s*@\s*(class)");
		private	Regex namespacePattern = new Regex(@"\s*\*\s*@\s*(namespace)");
		
		private string namespaceXml = "<package></package>";
		private string modifiedXml = "";
		private string[] asFileLines;
		
		
		public JSDocumentationBuilder()
		{
		}
		
		public XmlNodeList AddFile(string[] _asFileLines,DateTime modifiedTime)
		{
			string classXml = "";
			modifiedXml = "<modified ticks=\""+modifiedTime.Ticks+"\">"+String.Format("{0:d/M yyyy}", modifiedTime)+"</modified>\r\n";
			asFileLines = Utils.cleanUpLines(_asFileLines,false);
			for(int i = 0;i < asFileLines.Length;i++){
				if(asFileLines[i].IndexOf(startTag) == 0){	
					int endIndex = i;
					for(int h = i; h<asFileLines.Length;h++ ){
						if(asFileLines[h].IndexOf(endTag) != -1){
								endIndex = h;
								break;
							}
					}
				   	classXml += extractJavadocData(i,endIndex);
				   	i = endIndex;
				}
			}
			
			XmlDocument xml = new XmlDocument();
			xml.LoadXml("<nodes>"+classXml+endClassNode()+"</nodes>");
			XmlNodeList classes = xml.SelectNodes("/nodes/class");

			return classes;
		}

		///<summary>
		///
		///</summary>
		private string extractJavadocData(int startIndex,int endIndex)
		{			
			string resultText = "";
			//find the name and type (class property or method)
			for(int i = startIndex; i < endIndex;i++ ){				
					if(methodPattern.IsMatch(asFileLines[i])){
						resultText += createMethodNode(startIndex,endIndex,i);
					}
					else if(propertyPattern.IsMatch(asFileLines[i])){						
						resultText += createPropertyNode(endIndex,i);
					}
					else if(namespacePattern.IsMatch(asFileLines[i])){						
						getNamespaceData(endIndex);
					}
					else if(classPattern.IsMatch(asFileLines[i])){
						if(openClassTag) endClassNode();
						resultText += startClassNode(endIndex,i);
					}
			}
			return resultText;
		}
		
		private string createMethodNode(int startIndex,int endIndex,int nameLine)
		{
			string accesString = "public";	
			string resultText = "<method access=\""+accesString+"\">\r\n";
			string mName = Utils.getOneLineMultiDescription(asFileLines,endIndex,"method");
			resultText += "<name>"+mName+"</name>\r\n";
			resultText += generelStuff();
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,nameLine)+"]]></summary>\r\n";
			resultText += Utils.getStandAloneTags(asFileLines,endIndex);
			
			string codeline = "";
			for(int i = startIndex; i < endIndex;i++ ){
				if(asFileLines[i].IndexOf("@param") != -1){
					string paramData = asFileLines[i].Substring(asFileLines[i].IndexOf("@param")+7);
					string type = Utils.stripElement(paramData,@"\s*{",@"}.*");
					string pName = Utils.stripElement(paramData,@"\s*{.*} ",@" .*");
					
					resultText += "<param>\r\n";
					resultText += "<type fullPath=\"#\">" +type +"</type>\r\n";
					resultText += "<name>" +pName +"</name>\r\n";
					resultText += "<summary><![CDATA["+Utils.getOneLineMultiDescription(asFileLines,endIndex,"param {"+type+"} "+pName)+"]]></summary>\r\n";
					resultText += "</param>\r\n";
					codeline += " "+pName+",";
				}else if(asFileLines[i].IndexOf("@return") != -1){
					string paramData = asFileLines[i].Substring(asFileLines[i].IndexOf("@return")+8);
					string type = Utils.stripElement(paramData,@"\s*{",@"}.*");
					
					resultText += "<returns>\r\n";
					resultText += "<type fullPath=\"#\">" +type +"</type>\r\n";
					resultText += "<summary><![CDATA["+Utils.getOneLineMultiDescription(asFileLines,endIndex,"return {"+type+"} ")+"]]></summary>\r\n";
					resultText += "</returns>\r\n";
				}
				
			}
			
			resultText += "<codeLine>"+mName+"("+codeline.TrimEnd(',').Trim(' ')+")</codeLine>\r\n";
			resultText += "</method>\r\n";
			return resultText;
		
		}
		
		private string createPropertyNode(int endIndex,int nameLine)
		{
			string accesString = "public";	
			string resultText = "<property access=\""+accesString+"\">\r\n";
			resultText += "<name>"+Utils.getOneLineMultiDescription(asFileLines,endIndex,"property")+"</name>\r\n";
			resultText += generelStuff();
			resultText += "<codeLine/>\r\n";
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,nameLine)+"]]></summary>\r\n";
			resultText += Utils.getStandAloneTags(asFileLines,endIndex);
			resultText += "</property>\r\n";			

			
			return resultText;
		}
		
		private string startClassNode(int endIndex,int nameLine)
		{
			openClassTag = true;
			string resultText = "<class>"+"\r\n"+modifiedXml+"\r\n";
			resultText += namespaceXml;
			resultText += "<name>"+Utils.getOneLineMultiDescription(asFileLines,endIndex,"class")+"</name>\r\n";
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,nameLine)+"]]></summary>\r\n";
			resultText += Utils.getId();
			return resultText;
		}
		
		private string endClassNode()
		{
			string result = "</class>\r\n";
			return result;
		}
		
		private void getNamespaceData(int endIndex)
		{
			namespaceXml = "<package>"+Utils.getMultiDescription(asFileLines,endIndex,"namespace")+"</package>";
		}
		
		private string generelStuff()
		{
			string result = "<modifiers/>\r\n";
			result += Utils.getId();
			return result;
		}
		
		
	}
}
