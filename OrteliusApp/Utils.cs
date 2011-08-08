/*
 * Created by SharpDevelop.
 * User: moe
 * Date: 15-07-2011
 * Time: 10:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace Ortelius
{
	/// <summary>
	/// Description of Utils.
	/// </summary>
	public class Utils
	{
		private static int idCounter = 0;
		
		private static string delimeter = "*";
		private static string startTag = "/**";
		
		private static Regex lastWhiteSpace = new Regex(@"[\t| ]*$");
		//
		public static string[] cleanUpLines(string[] asFileLines)
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
		public static string removeIndent(string asLine)
		{	
			char[] trimChar = {'\t',' '};
			asLine = asLine.TrimStart(trimChar);
			asLine = asLine.TrimEnd(trimChar);
						
			return asLine;
		}
		
		
		///<summary><![CDATA[
		///Removes comment characters
		///</summary
		public static string removeCommentChars(string summeryText)
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
		
		public static string getId(){
			return "<fid>_"+(idCounter++).ToString()+"</fid>\r\n";
		}
	}
}
