/*
 * Created by SharpDevelop.
 * User: marten
 * Date: 15-07-2011
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Ortelius
{
	/// <summary>
	/// Description of JSDocumentationBuilder.
	/// </summary>
	public class JSDocumentationBuilder
	{
		
		private string startTag = "/**";
		private string endTag = "*/";
		private bool openClassTag = false;		
		
		private	Regex propertyPattern = new Regex(@"\s*\*\s*@\s*(property)");
		private	Regex methodPattern = new Regex(@"\s*\*\s*@\s*(method)");
		private	Regex classPattern = new Regex(@"\s*\*\s*@\s*(class)");
		private	Regex namespacePattern = new Regex(@"\s*\*\s*@\s*(namespace)");
		
		private string namespaceXml = "";
		private string modifiedXml = "";
		private string[] asFileLines;
		
		
		public JSDocumentationBuilder()
		{
		}
		
		public string AddClass(string[] asFileLines,DateTime modifiedTime)
		{
			string classXml = "";
			modifiedXml = "<modified ticks=\""+modifiedTime.Ticks+"\">"+String.Format("{0:d/M yyyy}", modifiedTime)+"</modified>\r\n";
			this.asFileLines = Utils.cleanUpLines(asFileLines);
			
			for(int i = 0;i < asFileLines.Length;i++){
				if(asFileLines[i].IndexOf(startTag) == 0){					
					int endIndex = i;
					for(int h = i; h<asFileLines.Length;h++ ){
						if(asFileLines[h].IndexOf(endTag) != -1){
								endIndex = i;
								break;
							}
					}
				   	classXml += extractJavadocData(i,endIndex);
				}
			}
			return classXml+endClassNode();
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
						resultText += createMethodNode(endIndex,i);
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
		
		private string createMethodNode(int endIndex,int nameLine)
		{
			string accesString = "public";	
			string resultText = "<method access=\""+accesString+"\">\r\n";
			resultText += "<name>"+Utils.getMultiDescription(asFileLines,endIndex,"method")+"</name>\r\n";
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,i)+"]]></summary>\r\n";
			resultText += Utils.getStandAloneTags(asFileLines,endIndex);
			resultText += "</method>\r\n";
		}
		
		private string createPropertyNode(int endIndex,int nameLine)
		{
			string accesString = "public";	
			string resultText = "<property access=\""+accesString+"\">\r\n";
			resultText += "<name>"+Utils.getMultiDescription(asFileLines,endIndex,"property")+"</name>\r\n";
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,i)+"]]></summary>\r\n";
			resultText += Utils.getStandAloneTags(asFileLines,endIndex);
			resultText += "</property>\r\n";			
		}
		
		private string startClassNode(int endIndex,int nameLine)
		{
			openClassTag = true;
			string resultText = "<class>"+"\r\n"+modifiedXml+"\r\n";
			resultText += namespaceXml;
			resultText += "<name>"+Utils.getMultiDescription(asFileLines,endIndex,"class")+"</name>\r\n";
			resultText += "<summary><![CDATA["+Utils.getSummery(asFileLines,nameLine)+"]]></summary>\r\n";
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
		
	}
}
