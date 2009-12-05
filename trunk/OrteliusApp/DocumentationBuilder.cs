/*
 * Created by SharpDevelop.
 * User: moe
 * Date: 23-02-2008
 * Time: 09:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace Ortelius
{
	/// <summary><![CDATA[
	/// Description of build.
	/// ]]></summary>
	public class DocumentationBuilder
	{
		
		public string SystemSvar = "";
		private string delimeter = "*";
		private string classType = "";
		
		private Regex accessPublicTest = new Regex(".*public.*");
		private Regex accessProtectedTest = new Regex(".*protected.*");
		private Regex accessPublicAndProtectedTest = new Regex(".*(public|protected).*");
		private Regex commentPublicAndProtectedTest = new Regex(@".*(//|/\*|\*).*(public|protected)");
		private Regex lastWhiteSpace = new Regex(@"[\t| ]*$");
		private Regex packageTest = new Regex(@"^[\t| ]*package");
		
		
		private Regex funcTest = new Regex("function +");
		private Regex funcGetTest = new Regex("function +get +");
		private Regex funcSetTest = new Regex("function +set +");
		
		//private XmlDocument resultXml;
		
		
		


		
		public DocumentationBuilder()
		{
//			resultXml = new XmlDocument();
//			XmlDeclaration dec = resultXml.CreateXmlDeclaration("1.0", null, null);
//			resultXml.AppendChild(dec);
//			addToXml("docElements").InnerText = "HEJ";
		}

//		private XmlElement addToXml(string nodeName){
//			XmlElement newNode = resultXml.CreateElement(nodeName);
//			resultXml.AppendChild(newNode);
//			return newNode;
//		}
		
		public string AddClass(string[] asFileLines)
		{
			string classXml = "";
			asFileLines = cleanUpLines(asFileLines);
			classXml += getImportInfo(asFileLines);
			classXml += getClassInfo(asFileLines);				
			classXml += getOtherInfo(asFileLines);
			return classXml;
		}
		
		
		#region Creates the different doc xml
		
		///<summary>
		///
		///</summary
		private string getImportInfo(string[] asFileLines)
		{	
			string resultText = "";				
			
			for(int i = 0; i<asFileLines.Length;i++ ){
				string fileLine = asFileLines[i];
				
				if(fileLine.IndexOf("import ") == 0){
					
					int namePosStart = fileLine.IndexOf("import ")+7;
					string text = fileLine.Substring(namePosStart,(fileLine.Length-namePosStart));
					text = text.TrimEnd(';');
					resultText += "<import><packageName>"+text+"</packageName></import>\r\n";
					resultText.TrimEnd(("\r\n ;").ToCharArray());
					
					
				}
			}
			
			return resultText;
		}
		
		
		///<summary><![CDATA[
		///
		///</summary
		private string getClassInfo(string[] asFileLines)
		{	
			string packageName = "";
			string resultText = "";				
							
					
			for(int i = 0; i<asFileLines.Length;i++ ){
				string fileLine = asFileLines[i];
				if(packageTest.IsMatch(fileLine)) packageName = stripElement(fileLine,@"package *",@" +|{ *");
				//lav også som regexp
				if(fileLine.IndexOf("class ") != -1 && fileLine.IndexOf("*")==-1){
					resultText += "<inheritanceHierarchy/>\r\n";
					if(fileLine.IndexOf(" extends ") != -1) resultText += "<extends>"+stripElement(fileLine,@"(.*extends +)",@"([ +|{].*)")+"</extends>\r\n";
					if(fileLine.IndexOf(" implements ") != -1) resultText += "<implements>"+stripElement(fileLine,@"(.*implements +)",@"([ +|{].*)")+"</implements>\r\n";
								
					if(fileLine.IndexOf("static ") != -1) resultText += "<modifier>static</modifier>\r\n";
					if(fileLine.IndexOf("dynamic ") != -1) resultText += "<modifier>dynamic</modifier>\r\n";
					
					
					resultText += "<package>"+packageName+"</package>\r\n";
					
					resultText += "<name>"+stripElement(fileLine,@" *(dynamic +)?(final +)?((public|internal) +)?(dynamic +)?(final +)?(class +)",@"( +extends)?( +\S*)?( +implements)?( +\S*)? *{? *")+"</name>\r\n";
					
					resultText += "<summary><![CDATA["+getSummery(asFileLines,i)+"]]></summary>\r\n";
					//tjek for events
					resultText += getStandAloneTags(asFileLines,i);
					resultText += getEventTags(asFileLines,i);
					classType= "class";
					
				}else if(fileLine.IndexOf("interface ") != -1 && fileLine.IndexOf("*")==-1){
					resultText += "<package>"+packageName+"</package>\r\n";
					
					resultText += "<name>"+stripElement(fileLine,@" *((public|internal) +)?(interface +)",@"( +extends)?( +\S*)? *{? *")+"</name>\r\n";
					
					resultText += "<summary><![CDATA["+getSummery(asFileLines,i)+"]]></summary>\r\n";
					//tjek for events
			 		resultText += getStandAloneTags(asFileLines,i);
			 		resultText += getEventTags(asFileLines,i);
					classType= "interface";
				}
			}
			
			return resultText;
		}
		
		

		///<summary><![CDATA[
		///
		///</summary
		private string getOtherInfo(string[] asFileLines){
			
			
			string resultText = "";
			ArrayList publicLineIndexes = new ArrayList();
			//make index of public lines
			for(int i = 0; i<asFileLines.Length;i++ ){
				string fileLine = asFileLines[i];
				if((accessPublicAndProtectedTest.IsMatch(fileLine) || (classType=="interface" && funcTest.IsMatch(fileLine)))&& !commentPublicAndProtectedTest.IsMatch(fileLine) ) {
					if(fileLine.IndexOf("//")!=-1){
						//get everything before commenttag
						asFileLines[i] = asFileLines[i].Substring(0,asFileLines[i].IndexOf("//"));
					}
					resultText += createElementDoc(i,asFileLines);
				}
			}
			return resultText;
		}
		
		
		
		///<summary><![CDATA[
		///
		///</summary
		private string createElementDoc(int lineIndex,string[] asFileLines){
			
			string accesString = "public";	
			
			string resultText = "";
			string fileLine = asFileLines[lineIndex];
			
							
			
			//methods
			if(funcTest.IsMatch(fileLine) && !funcGetTest.IsMatch(fileLine)  && !funcSetTest.IsMatch(fileLine)){	
				
				
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<method access=\""+accesString+"\">\r\n";
				
				resultText += "<name>"+stripElement(fileLine,@" *(static +)?(override +)?((protected|public|internal) +)?(override +)?(static +)?(function +)",@"\(.*")+"</name>\r\n";
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				
				resultText += "<codeLine>"+fileLine.Replace("{","")+"</codeLine>\r\n";
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";	
				
				try{
					//loop through
					string paramString = stripElement(fileLine,@".*(function +)(.*\()",@"\).*");
					if(paramString !=""){
					string[] allParams = paramString.Split(',');
					foreach(string param in allParams){
						resultText += "<param>\r\n";
						int colonIndex = param.IndexOf(":");
						
						if(colonIndex == -1) colonIndex = param.Length-1;
						else resultText += "<type>" +param.Substring((colonIndex+1),(param.Length-colonIndex-1)) +"</type>\r\n";
						
						string pName = param.Substring(0,colonIndex).TrimStart(' ');
						
						resultText += "<name>" +pName +"</name>\r\n";
						resultText += "<summary><![CDATA["+getDescription(asFileLines,lineIndex,"@param "+pName)+"]]></summary>\r\n";
						resultText += "</param>\r\n";
					}
				
					}
				}catch(Exception){
					SystemSvar += "\nLine #"+lineIndex+" \""+ asFileLines[lineIndex]+"\"";
				}
				
					
					
					
					if(fileLine.IndexOf("):") > 1){
						resultText += "<returns>\r\n";
						resultText += "<type>"+stripElement(fileLine,@".*\) *: *",@" *{? *")+"</type>\r\n";
						resultText += "<summary><![CDATA["+getDescription(asFileLines,lineIndex,"@return")+getDescription(asFileLines,lineIndex,"@returns")+"]]></summary>\r\n";
						resultText += "</returns>\r\n";
					}
					
				 resultText += getStandAloneTags(asFileLines,lineIndex);
					
					resultText += "</method>\r\n";
					
				}
		
			else if(fileLine.IndexOf("function get ") != -1){								
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<property access=\""+accesString+"\"  readWrite=\"Read\">\r\n";
				
				resultText += "<name>"+stripElement(fileLine,@" *(static +)?(override +)?((protected|public|internal) +)?(override +)?(static +)?(function +get +)",@"\(.*")+"</name>\r\n";
				
				if(fileLine.IndexOf("):") != -1) resultText += "<type>"+stripElement(fileLine,@".*\) *: *",@" *{.*")+"</type>\r\n";
				
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += "<codeLine><![CDATA["+fileLine.Replace(";","")+"]]></codeLine>\r\n";
				resultText += "</property>\r\n";
			}	
			else if(fileLine.IndexOf("function set ") != -1){
				
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<property access=\""+accesString+"\" readWrite=\"Write\">\r\n";
				
				resultText += "<name>"+stripElement(fileLine,@" *(static +)?(override +)?((protected|public|internal) +)?(override +)?(static +)?(function +set +)",@"\(.*")+"</name>\r\n";
				
							
				resultText += "<type>"+stripElement(fileLine,@".*\(.*[^\)]:",@" *\).*")+"</type>\r\n";
									
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";			
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += "<codeLine><![CDATA["+fileLine.Replace(";","")+"]]></codeLine>\r\n";
				resultText += "</property>\r\n";
			}	
			
				else if(fileLine.IndexOf("var ") != -1 || fileLine.IndexOf("const ") != -1){	
				
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<property access=\""+accesString+"\" readWrite=\"ReadWrite\">\r\n";
				
				string propName = stripElement(fileLine,@" *((public|protected) +)?(static +)?((public|protected) +)?((const|var) +)(static +)?",@" *[:| |=|;].*");
				resultText += "<name>"+propName+"</name>\r\n";
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";	
				
				if(fileLine.IndexOf(propName+":")!=-1) resultText += "<type>"+stripElement(fileLine,@".*"+propName+":",@" *=.*")+"</type>\r\n";
				
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += "<codeLine><![CDATA["+stripElement(fileLine,"",@"[{|;]")+"]]></codeLine>\r\n";
				if(fileLine.IndexOf("=") != -1) resultText += "<defaultValue><![CDATA["+stripElement(fileLine,@".*= *",@";+ *")+"]]></defaultValue>\r\n";
				
				resultText += getStandAloneTags(asFileLines,lineIndex);
				resultText += "</property>\r\n";
				
			}
			return resultText;
			
		}
		
		
		
		
		#endregion
		
		
		#region Creates the different parts
		
		///<summary><![CDATA[
		///
		///</summary
		private string getModifiers(string fileLine)
		{	
			string result = "";
				if(fileLine.IndexOf("static ") != -1) result += "<modifier>static</modifier>\r\n";				
				if(fileLine.IndexOf("override ") != -1) result += "<modifier>override</modifier>\r\n";		
				if(fileLine.IndexOf("dynamic ") != -1) result += "<modifier>dynamic</modifier>\r\n";	
				if(fileLine.IndexOf("abstract ") != -1) result += "<modifier>dynamic</modifier>\r\n";
				return result;
		}
		///<summary>
		///Getting the clas summary - getSummery, getDescription & getMultiLineDescription looks very alike
		///</summary>
		private string getSummery(string[] asFileLines,int elementIndex)
		{	
			
			char[] trimChar = {'\n','\r'};
			string tag = "/*";
			string resultText = "";
			int decIndex = elementIndex-1;
			int tagIndex = elementIndex;
						//skip empty line
			while(decIndex>0 && asFileLines[decIndex].TrimEnd(trimChar)=="" ){				
				decIndex --;
			}
						
			while(decIndex>=0 && (asFileLines[decIndex].IndexOf(delimeter) == 0 || asFileLines[decIndex].IndexOf("/*") == 0)){
				if(asFileLines[decIndex].IndexOf(tag)!= -1){
					tagIndex = decIndex+1;
					break;
				}
				decIndex --;
			}
			
			while(tagIndex < elementIndex){
				
				if(asFileLines[tagIndex].IndexOf("*@")==0 || asFileLines[tagIndex].IndexOf("*/")!=-1 ) break;
				else {
				string text = removeCommentChars(asFileLines[tagIndex]);
				if(text != "") resultText +=  text +"<br/>";
				}
				tagIndex ++;
			}
			
			resultText = resultText.TrimEnd('\r');
			resultText = resultText.TrimEnd('\n');
			resultText = resultText.TrimEnd('\r');
			return resultText;
		}
		
		
		///<summary>
		///getSummery, getDescription & getMultiDescription looks very alike
		///</summary>
		private string getDescription(string[] asFileLines,int elementIndex,string tag)
		{
			
			char[] trimChar = {'\n','\r'};
			string resultText = "";
			int decIndex = elementIndex-1;
			int tagIndex = elementIndex;
			try{
						//skip empty line
			while(decIndex>0 && asFileLines[decIndex].TrimEnd(trimChar)=="" ){				
				decIndex --;
			}
				while(decIndex>=0 && (asFileLines[decIndex].IndexOf(delimeter) == 0 || asFileLines[decIndex]=="")){
				if(asFileLines[decIndex].IndexOf(tag)!= -1){
					tagIndex = decIndex;
					break;
				}
				decIndex --;
			}
			bool firstFlag = true;
			while(tagIndex < elementIndex){
				
				if(!firstFlag && (asFileLines[tagIndex].IndexOf("*@")==0 || asFileLines[tagIndex].IndexOf("*/")!=-1 )) break;
				else {
				string text = removeCommentChars(asFileLines[tagIndex]);
				if(text != ""){
					
					
					
				if(firstFlag) text = text.Substring(tag.Length);
					 resultText +=  text.TrimStart(' ') +"<br/>";
					firstFlag = false;
				}
				}
				tagIndex ++;
			}
			
			resultText = resultText.TrimEnd('\n');
			resultText = resultText.TrimEnd('\r');
			resultText = resultText.TrimEnd('\n');
			}catch(Exception){
				SystemSvar += "\nLine #"+tagIndex+" \""+asFileLines[tagIndex]+"\"";
				
			}
			
			return resultText;
		}
		
		///<summary>
		///Gets the description of more than one occurence of a tag - getSummery, getDescription & getMultiDescription looks very alike
		///</summary>
		private string[] getMultiDescription(string[] asFileLines,int elementIndex,string tag){
			
			if(elementIndex<=0) return new string[]{};
			string resultText="";
			ArrayList allResults = new ArrayList();
			char[] trimChar = {'\n','\r'};
			
			int index = elementIndex-1;
			int tagIndex = elementIndex;
			
			//skip empty line
			while(index>0 && asFileLines[index].TrimEnd(trimChar)=="" ){				
				index --;
			}
			//find line where doc starts
			while(index>0 && asFileLines[index].IndexOf(delimeter) == 0 ){
				index --;
			}
			
			try{
			bool recordFlag = false;
			while(index <= elementIndex){
				string asLine = removeCommentChars(asFileLines[index]);
				if((asFileLines[index].IndexOf("*/")== 0 ||  asLine.IndexOf("@")== 0 || index == elementIndex) && recordFlag){
					//stop recording
					resultText = resultText.TrimEnd(trimChar);
					allResults.Add(resultText);
					recordFlag = false;
				}
				if(asLine.IndexOf("@"+tag)== 0){
					//start recording
					string text = removeCommentChars(asFileLines[index]);
					
					text = text.Substring(tag.Length+1);
					
					resultText =  text.TrimStart(' ') +"<br/>";
					recordFlag = true;
				} else if(recordFlag){					
					resultText +=asLine+"<br/>";					
				}			
				index ++;
			}
			
			}catch(Exception){
				SystemSvar +=  "\nLine #"+tagIndex+" \""+asFileLines[tagIndex]+"\"";
			}
			return (string[])allResults.ToArray(typeof(string));
		}
		
		private string getStandAloneTags(string[] asFileLines,int elementIndex)
		{
			string[] tags = {"see","version","author","todo","langversion","keyword","playerversion","langversion","throws","sends","example"};
			string resultText = "";
			foreach(string tag in tags){
				string[] tagDoc = getMultiDescription(asFileLines, elementIndex, tag);
				foreach(string docString in tagDoc){
					resultText += "<"+tag+"><![CDATA["+docString+"]]></"+tag+">\r\n";
				}
			}
			
			return resultText;
		}
		
		private string getEventTags(string[] asFileLines,int elementIndex)
		{
			string resultText = "";
				string[] tagDoc = getMultiDescription(asFileLines, elementIndex, "@event");
												
				foreach(string docString in tagDoc){
					string tempDocString = docString.TrimStart(' ');
					int delimIndex = tempDocString.IndexOf(" ");
					if(delimIndex == -1) delimIndex = tempDocString.Length;
					string eventName = tempDocString.Substring(0,delimIndex);
					string summery = "";
					if(tempDocString.Length > (delimIndex+1) ) summery =tempDocString.Substring(delimIndex+1);
					resultText += "<event>\r\n<name>"+eventName+"</name>\r\n<summary><![CDATA["+summery+"]]></summary>\r\n</event>\r\n";
				}
		
			
			return resultText;
		}
		
		
		
		#endregion
		
		
		#region Generel methods
		
		//
		private string[] cleanUpLines(string[] asFileLines)
		{	
			for(int i = 0; i<asFileLines.Length;i++ ){
				asFileLines[i] = removeIndent(asFileLines[i]);
				asFileLines[i] = asFileLines[i].Replace("<br>","<br />");
				asFileLines[i] = asFileLines[i].Replace("\t"," ");
				
				Regex emptyStartChars = new Regex(@"^[\t| ]*");
				asFileLines[i] =  emptyStartChars.Replace(asFileLines[i], "");
				
				Regex astAddSpace = new Regex(@"\*\s*@");
				asFileLines[i] =  astAddSpace.Replace(asFileLines[i], "*@");
		
				
				//I the function uses parameter in more than one line
				int ekstraIndex = 1;
				while(asFileLines[i].IndexOf("function")!=-1 && asFileLines[i].IndexOf(")")==-1){
					asFileLines[i] += asFileLines[i+ekstraIndex];
					ekstraIndex++;
					if(ekstraIndex>20){
						break;
					}
				}
			}
			
			
			return asFileLines;
		}
		
		
		///<summary><![CDATA[
		///
		///</summary
		private string removeIndent(string asLine)
		{	
			asLine = asLine.Trim(' ');
			char[] trimChar = {'\t',' '};
			asLine = asLine.TrimStart(trimChar);
						
			return asLine;
		}
		
		
		///<summary><![CDATA[
		///Removes comment characters
		///</summary
		private string removeCommentChars(string summeryText)
		{			
			char[] trimChar = {'\t',' ','/','*','\\'};
			return summeryText.TrimStart(trimChar);			
		}
		
		string stripElement(string linje,string startRegexp,string slutRegexp){
			Regex rx1 = new Regex(startRegexp);
			Regex rx2 = new Regex(slutRegexp);
			linje =  rx1.Replace(linje, "");
			linje =  rx2.Replace(linje, "");
			linje =  lastWhiteSpace.Replace(linje, "");
			
			return linje;
		}
		
		#endregion
		
		
		
	}
}
