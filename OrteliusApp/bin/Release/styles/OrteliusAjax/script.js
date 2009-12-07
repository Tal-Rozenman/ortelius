var allClasses = new Array();

var currentElement;

var winLoc = window.location;

var currentButton;
var layerHistory = new Array();

var origUrl = location.href

window.onload = function(){
	showElement("introText")
}

//window.setInterval(tjekUrl,100)
function tjekUrl(){
	//alert(window.location.hash)
	if("#"+currentElement != winLoc.hash){
		window.status = winLoc.hash;
		//doBack()
	}
}


function toggleTreeElement(elementId){
	//alert(elementId)
	if(elementId.className == "packageTreeVisible")	elementId.className = "packageTreeHidden";
	else elementId.className = "packageTreeVisible";
	}
	
function showHideAllTreeElement(doShow){
	
	}

function showElement(elementId){
	if(elementId.indexOf("@") ==0){
		var url = elementId.replace("@","");
		window.open("http://www.google.com/search?q="+url+"+Actionscript");
		return;
	}
	//document.body.innerHTML += "<a name=\""+elementId+"\"></a>";
	
	winLoc.hash = "#"+elementId
	
	//alert(winLoc.hash)
	
	//window.event.returnValue=false;
	//history.forward()
		
	if(elementId!=null){
		getElement(elementId)
		layerHistory.push(currentElement);
		currentElement = elementId

		//marker knap
		if(document.getElementById(elementId+"Button")){
			if(currentButton!=null) document.getElementById(currentButton).className = "nonChoosen"
			currentButton = elementId+"Button"
			document.getElementById(elementId+"Button").className = "choosen"
			}
		}
		
		return false;
	}


	
	function goBack(){	
	//history.back();
	doBack();
	}
	
	function doBack(){	
	
	elementId = layerHistory.pop();	
		if(elementId!=null){
		winLoc.hash = "#"+elementId
	
	getElement(elementId)
	
currentElement = elementId;
		}
	
	
	}
	
//AJAX stuff ////////////////////////////////	
function getElement(elementId)
{
	
xmlHttp=GetXmlHttpObject();

if (xmlHttp==null){
return
} 
url="ortfiles/"+elementId+".html";//?sid="+Math.random()
//alert(url)
xmlHttp.onreadystatechange=stateChanged 
xmlHttp.open("GET",url,true);
xmlHttp.send(null);
}


function stateChanged() {
	
	if(xmlHttp.readyState != 4) return
	if(xmlHttp.responseText == null) return;

var svar = xmlHttp.responseText;

document.getElementById("content").innerHTML = svar;
} 

function GetXmlHttpObject()
{ 
var objXMLHttp=null
if (window.XMLHttpRequest)
{
objXMLHttp=new XMLHttpRequest()
}
else if (window.ActiveXObject)
{
objXMLHttp=new ActiveXObject("Microsoft.XMLHTTP")
}
return objXMLHttp
} 
