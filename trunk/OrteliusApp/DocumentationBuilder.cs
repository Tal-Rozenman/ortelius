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
		private string startTag = "/**";
		private string endTag = "*/";
		private string classType = "";
		private string currentPackages = "";
		
		
		
		private int idCounter = 1;
		
		private Regex accessPublicTest = new Regex(".*public.*");
		private Regex accessProtectedTest = new Regex(".*protected.*");
		private Regex accessPublicAndProtectedTest = new Regex(".*(public|protected).*");
		private Regex commentPublicAndProtectedTest = new Regex(@".*(//|/\*|\*).*(public|protected)");
		private Regex lastWhiteSpace = new Regex(@"[\t| ]*$");
		private Regex packageTest = new Regex(@"^[\t| ]*package");		
		
		private	Regex publicClassTest = new Regex(@"^[^\*/]*public[ |\t][^\*/]*class[ |\t]");
		private	Regex interfaceTest = new Regex(@"^[^\*/]*public[ |\t][^\*/]*interface[ |\t]");
		
		private Regex funcTest = new Regex(@"^[^\*/]*function +");
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
		
		public string AddClass(string[] asFileLines,DateTime modifiedTime)
		{
			currentPackages = "";
			idCounter = 1;
			string classXml = "<modified ticks=\""+modifiedTime.Ticks+"\">"+String.Format("{0:d/M yyyy}", modifiedTime)+"</modified>";
			asFileLines = cleanUpLines(asFileLines);
//			string testAS = "";
//			foreach(string line in asFileLines) testAS += line+"\n";
//			MessageBox.Show(testAS);
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
					resultText += "<import><packageName fullPath=\"\">"+text+"</packageName></import>\r\n";
					resultText.TrimEnd(("\r\n ;").ToCharArray());
					currentPackages += text+",";
				}
			}
			
			currentPackages = currentPackages.TrimEnd(',');
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
				//interfaceTest.IsMatch(fileLine)
				
				if(publicClassTest.IsMatch(fileLine)){
					resultText += "<inheritanceHierarchy/>\r\n";
					if(fileLine.IndexOf(" extends ") != -1) resultText += "<extends>"+stripElement(fileLine,@"(.*extends +)",@"([ +|{].*)")+"</extends>\r\n";
					if(fileLine.IndexOf(" implements ") != -1) resultText += "<implements>"+stripElement(fileLine,@"(.*implements +)",@"([ +|{].*)")+"</implements>\r\n";
								
					if(fileLine.IndexOf("static ") != -1) resultText += "<modifier>static</modifier>\r\n";
					if(fileLine.IndexOf("dynamic ") != -1) resultText += "<modifier>dynamic</modifier>\r\n";
					
					
					resultText += "<package>"+packageName+"</package>\r\n";
					currentPackages += (packageName=="")?  ",*" : ","+packageName+".*";
					
					resultText += "<name>"+stripElement(fileLine,@" *(\[.*\] +)?(dynamic +)?(final +)?((public|internal) +)?(dynamic +)?(final +)?(class +)",@"( +extends)?( +\S*)?( +implements)?( +\S*)? *{? *")+"</name>\r\n";
					resultText += getId();
					
					resultText += "<summary><![CDATA["+getSummery(asFileLines,i)+"]]></summary>\r\n";
					//tjek for events
					resultText += getStandAloneTags(asFileLines,i);
					resultText += getEventTags(asFileLines,i);
					classType= "class";
					
				}else if(interfaceTest.IsMatch(fileLine)){
					resultText += "<package>"+packageName+"</package>\r\n";
					
					resultText += "<name>"+stripElement(fileLine,@" *((public|internal) +)?(interface +)",@"( +extends)?( +\S*)? *{? *")+"</name>\r\n";
					resultText += getId();
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
				
				resultText += "<name>"+stripElement(fileLine,@" *(static +)?(override +)?(virtual +)?((protected|public|internal) +)?(override +)?(static +)?(virtual +)?(function +)",@"\(.*")+"</name>\r\n";
				resultText += getId();
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				
				resultText += formatCodeline(fileLine);
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
						
						
						
						string pName = param.Substring(0,colonIndex).TrimStart(' ');
						if(param.IndexOf(":")!=-1) resultText += getTypeXml(param,pName);
						
						resultText += "<name>" +pName +"</name>\r\n";
						resultText += "<summary><![CDATA["+getDescription(asFileLines,lineIndex,"@param "+pName+" ")+"]]></summary>\r\n";
						resultText += "</param>\r\n";
					}
				
					}
				}catch(Exception){
					SystemSvar += "\r\nLine #"+lineIndex+": \""+ asFileLines[lineIndex]+"\"";
				}
				
					
					
					
		Regex returnParamTest = new Regex(@".*\) *:");
		
		
					if(returnParamTest.IsMatch(fileLine)){
						resultText += "<returns>\r\n";
						resultText += formatType(stripElement(fileLine,@".*\) *: *",@" *{? *"));
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
				resultText += getId();
				if(fileLine.IndexOf("):") != -1){
					string typeName =stripElement(fileLine,@".*\) *: *",@" *{.*");
					resultText += formatType(typeName);;
				}
				
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += formatCodeline(fileLine);
				resultText += "</property>\r\n";
			}	
			else if(fileLine.IndexOf("function set ") != -1){
				
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<property access=\""+accesString+"\" readWrite=\"Write\">\r\n";
				
				string pName = stripElement(fileLine,@" *(static +)?(override +)?((protected|public|internal) +)?(override +)?(static +)?(function +set +)",@"\(.*");
				resultText += "<name>"+pName+"</name>\r\n";	
				resultText += getId();
				string typeName = stripElement(fileLine,@".*\(.*[^\)]:",@" *\).*");
				
				resultText += formatType(typeName);
				
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";			
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += formatCodeline(fileLine);
				resultText += "</property>\r\n";
			}	
			
				else if(fileLine.IndexOf("var ") != -1 || fileLine.IndexOf("const ") != -1){	
				
				if(accessProtectedTest.IsMatch(fileLine))accesString = "protected";
				resultText += "<property access=\""+accesString+"\" readWrite=\"ReadWrite\">\r\n";
				
				string propName = stripElement(fileLine,@" *((public|protected) +)?(static +)?((public|protected) +)?((const|var) +)(static +)?",@" *[:| |=|;].*");
				resultText += "<name>"+propName+"</name>\r\n";
				resultText += getId();
				resultText += "<modifiers>\r\n"+getModifiers(fileLine)+"</modifiers>\r\n";	
				
				if(fileLine.IndexOf(propName+":")!=-1) resultText += getTypeXml(fileLine,propName)+"\r\n";
				
				resultText += "<summary><![CDATA["+getSummery(asFileLines,lineIndex)+"]]></summary>\r\n";
				resultText += formatCodeline(stripElement(fileLine,"",@"[{|;]"));
				if(fileLine.IndexOf("=") != -1) resultText += "<defaultValue><![CDATA["+formatCode(stripElement(fileLine,@".*= *",@";+ *"))+"]]></defaultValue>\r\n";
				
				resultText += getStandAloneTags(asFileLines,lineIndex);
				resultText += "</property>\r\n";
				
			}
			return resultText;
			
		}
		
		
		string getTypeXml(string line,string name){
			line = stripElement(line,@".*:",@" *[=|;|)|,].*");
			return formatType(line);
		}
			
		string formatType(string typeName){
			typeName = typeName.Replace("<","&lt;").Replace(">","&gt;").Replace(" ","");
			return "<type fullPath=\"\"  context=\""+currentPackages+"\"><![CDATA[" +typeName+"]]></type>\r\n";
		}
		
		string formatCodeline(string codeLine){
			return "<codeLine><![CDATA[" +formatCode(codeLine)+"]]></codeLine>\r\n";
		}
		
		string formatCode(string codeLine){				
			codeLine = codeLine.Replace("<","&lt;").Replace(">","&gt;").Replace(";","");
			
			Regex codeLineEnd = new Regex(@"[;|{|/].*");
			codeLine =  codeLineEnd.Replace(codeLine, "");
					
			codeLine = codeLine.TrimEnd(' ').TrimEnd('{');
			return "<![CDATA[" +codeLine+"]]>";
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
				if(fileLine.IndexOf("override ") != -1) result += "<modifier>overriden</modifier>\r\n";		
				if(fileLine.IndexOf("dynamic ") != -1) result += "<modifier>dynamic</modifier>\r\n";	
				if(fileLine.IndexOf("abstract ") != -1) result += "<modifier>dynamic</modifier>\r\n";
				return result;
		}
		///<summary>
		///Getting the clas summary - getSummery, getDescription & getMultiLineDescription looks very alike
		///</summary>
		private string getSummery(string[] asFileLines,int elementIndex)
		{	
			
			char[] trimChar = {'\n','\r','\t',' '};
			string resultText = "";
			int decIndex = elementIndex-1;
			int tagIndex = elementIndex;
			
			//skip empty line
			while(decIndex>0 && asFileLines[decIndex].TrimEnd(trimChar)=="" ){				
				decIndex --;
			}
			//find the first line of the documentation
			while(decIndex>=0 && (asFileLines[decIndex].IndexOf(delimeter) == 0 || asFileLines[decIndex].IndexOf(startTag) == 0)){
				if(asFileLines[decIndex].IndexOf(startTag)!= -1){
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
			string[] tags = {"see","version","author","todo","langversion","keyword","playerversion","throws","exception","deprecated","sends","example","since"};
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
					resultText += "<event>\r\n<name>"+eventName+"</name>\r\n"+getId()+"<summary><![CDATA["+summery+"]]></summary>\r\n</event>\r\n";
				}
		
			
			return resultText;
		}
		
		
		
		#endregion
		
		
		#region Generel methods
		
		
		//
		private string[] cleanUpLines(string[] asFileLines)
		{	
			int curlyBracketCounter = 0;
			bool removeTheRest = false;
			bool multiLineComment = false;
			bool javaDocComment = false;
			
			for(int i = 0; i<asFileLines.Length;i++ ){
				asFileLines[i] = removeIndent(asFileLines[i]);
				
				//ignore if javadoc comments
				int javadocCommentIndex = asFileLines[i].IndexOf(startTag);
				if(!javaDocComment && javadocCommentIndex!=-1 && 
				   (javadocCommentIndex<=asFileLines[i].IndexOf("/*")) && 
				   (asFileLines[i].IndexOf("//")==-1 || javadocCommentIndex<asFileLines[i].IndexOf("//"))  ){
					javaDocComment = true;
				}
				
				int javadocCommentIndexEnd = asFileLines[i].IndexOf("*/");
				if(javaDocComment && javadocCommentIndexEnd!=-1){
					javaDocComment = false;
				}
				
				if(!javaDocComment){
					
					//remove inline multiline comments
					Regex imcPattern = new Regex(@"\/\*.*(\*\/)");
					asFileLines[i] =  imcPattern.Replace(asFileLines[i], "");
					
					//remove multiline comments
					int multiLineCommentIndexStart = asFileLines[i].IndexOf("/*");
					if(!multiLineComment && multiLineCommentIndexStart!=-1 && 
					   asFileLines[i].IndexOf(startTag)==-1 && 
					   (asFileLines[i].IndexOf("//")==-1 || multiLineCommentIndexStart<asFileLines[i].IndexOf("//"))){
						multiLineComment = true;
					}
					 
					int multiLineCommentIndexEnd = asFileLines[i].IndexOf("*/");
					if(multiLineComment && multiLineCommentIndexEnd!=-1){
						if(multiLineCommentIndexStart==-1) multiLineCommentIndexStart=0;
						asFileLines[i] = asFileLines[i].Substring(multiLineCommentIndexEnd+2,(asFileLines[i].Length - (multiLineCommentIndexEnd+2)));
						multiLineComment = false;
					} 
//					if(javaDocComment && multiLineCommentIndexEnd!=-1){
//						javaDocComment = false;
//					}
					
					//remove single line comments
					int commentIndex = asFileLines[i].IndexOf("//");
					if(commentIndex!=-1){
						asFileLines[i] = asFileLines[i].Substring(0,commentIndex);
					}
					
					//keep count on when the class is ending to avoid package with more than one class
					//This situation occurs in some singleton implementations
					if(!multiLineComment){
						int bracketPos = asFileLines[i].IndexOf("{");
						while(bracketPos != -1){
							curlyBracketCounter++;
							bracketPos++;
							if(bracketPos<asFileLines[i].Length) bracketPos = asFileLines[i].IndexOf("{",bracketPos);
							else bracketPos = -1;
						}
						bracketPos = asFileLines[i].IndexOf("}");
						while(bracketPos != -1){
							curlyBracketCounter--;
							if(curlyBracketCounter<=1) removeTheRest = true;
							bracketPos++;
							if(bracketPos<asFileLines[i].Length) bracketPos = asFileLines[i].IndexOf("}",bracketPos);
							else bracketPos = -1;
						}
					}
					
				}
				
				if(removeTheRest || multiLineComment){
					asFileLines[i] = "";
				}else{
					asFileLines[i] = asFileLines[i].Replace("<br>","<br />");
					asFileLines[i] = asFileLines[i].Replace("\t"," ");
					
					Regex emptyStartChars = new Regex(@"^[\t| ]*");
					asFileLines[i] =  emptyStartChars.Replace(asFileLines[i], "");
					
					Regex lastAddSpace = new Regex(@"\*\s*@");
					asFileLines[i] =  lastAddSpace.Replace(asFileLines[i], "*@");
			
					
					//I the function uses parameter in more than one line
					int ekstraIndex = 1;
					while(funcTest.IsMatch(asFileLines[i]) && asFileLines[i].IndexOf(")")==-1){
						asFileLines[i] += asFileLines[i+ekstraIndex];
						ekstraIndex++;
						if(ekstraIndex>20){
							break;
						}
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
			char[] trimChar = {'\t',' '};
			asLine = asLine.TrimStart(trimChar);
			asLine = asLine.TrimEnd(trimChar);
						
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
		
		private string getId(){
			return "<fid>_"+(idCounter++).ToString()+"</fid>\r\n";
		}
		#endregion
		
		
		
	}
}
