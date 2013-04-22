/**
*@namespace 
*/


/**
* 
* 
* @author Marten Ølgaard
* @created 22/4/2013
* @copyright Adnuvo
* @todo 
* @class Ortelius
* @static
*/
var Ortelius = Ortelius || (function () {

    var _r = new Object();


    _r.allClasses = new Array();

    _r.shownHiddenDetails;

    _r.currentElement;
    _r.currentPath = new Array();

    _r.winLoc = window.location;

    _r.currentButtons = new Array(null, null, null);
    _r.layerHistory = new Array();

    _r.allIndexLists = new Array("classList", "indexByName", "indexByModifiedTime");

    //_r.origUrl = location.href

    _r.sideSkiftet = function (event) {
        _r.showElement(event.value.replace("/", ""));
    }

    _r.init = function () {
        _r.shownHiddenDetails = [
    { "name": "showInherited_publicmethod", "show": true }, 
    { "name": "showInherited_protectedmethod", "show": true }, 
    { "name": "showInherited_internalmethod", "show": true },  
    { "name": "showInherited_privatemethod", "show": true }, 
    { "name": "showInherited_publicproperties", "show": true }, 
    { "name": "showInherited_protectedproperties", "show": true }, 
    { "name": "showInherited_internalproperties", "show": true },
    { "name": "showInherited_privateproperties", "show": true },
    { "name": "div|dependency", "show": false },    
    { "name": "div|see", "show": false }, 
    { "name": "div|details", "show": false }, 
    { "name": "div|example", "show": false }, 
    { "name": "div|publicmethod", "show": true }, 
    { "name": "div|import", "show": false }, 
    { "name": "div|publicproperties", "show": true }, 
    { "name": "div|parameters", "show": true }, 
    { "name": "div|methoddetails", "show": false }, 
    { "name": "div|propdetails", "show": false}, 
    { "name": "div|protectedmethod", "show": false }, 
    { "name": "div|protectedproperties", "show": false },
    { "name": "div|internalmethod", "show": false }, 
    { "name": "div|internalproperties", "show": false },
    { "name": "div|privatemethod", "show": false }, 
    { "name": "div|privateproperties", "show": false }];


    $.address.externalChange(_r.sideSkiftet);
    var addr = $.address.value().replace("/", "");
    if (addr == "") addr = "introText";
    _r.showElement(addr);

}


_r.changeIndex = function (elementId){
	for(var i=0;i<_r.allIndexLists.length;i++){
		document.getElementById(_r.allIndexLists[i]).style.visibility = "hidden";
		document.getElementById(_r.allIndexLists[i]+"Button").className = "nonChoosen";
	}
	document.getElementById(elementId).style.visibility = "visible";
	document.getElementById(elementId+"Button").className = "choosen";
	
}


_r.toggleDetails = function (elementId){
	var imgElement = document.getElementById(elementId.replace("div|","img|"));
	var element = document.getElementById(elementId);
	
	if(element.className == "detailsVisible"){
		_r.setDetailCookie(elementId,false);
		element.className = "hiddenElement";
		imgElement.src = "OrteliusAjax/foldud.gif";
	}else{
		_r.setDetailCookie(elementId,true);
		element.className = "detailsVisible";
		imgElement.src = "OrteliusAjax/foldind.gif";
	}
}



_r.toggleTreeElement = function (elementId){
	
	//change open / close ikon

	var imgElement = document.getElementById(elementId.replace("div|","img|"));
	
	element = document.getElementById(elementId);
	if(element.className == "packageTreeVisible"){
		element.className = "hiddenElement";
		imgElement.src = "OrteliusAjax/foldud.gif";
	}
	else{
		imgElement.src = "OrteliusAjax/foldind.gif";
		element.className = "packageTreeVisible";
	}
	}
	
_r.showHideAllTreeElement = function (doShow){
	
	}

_r.showElement = function (elementId) {

    $.address.value(elementId);

	   // alert(elementId);
	if(elementId.indexOf("#")==0){
		var url = elementId.replace("#","");
		window.open("http://www.google.com/search?q="+url+"+script");
		return;
	}
	
	
	//remove highlights
	for (var i = 0; i < _r.currentPath.length; i++) {
	    document.getElementById(_r.currentPath[i]).className = "nonChoosen";
	}
	_r.currentPath = new Array();
	
	//highlight packagepath
	var elementParts = elementId.split(".");
	var elementIdTemp = "a|";
	for(var i=0;i<elementParts.length-1;i++){
		elementIdTemp+=elementParts[i];
		
		if(document.getElementById(elementIdTemp)){
			document.getElementById(elementIdTemp).className = "choosen";
			_r.currentPath.push(elementIdTemp);	
		}
		
		elementIdTemp+="_"
	}

		
	if(elementId!=null){
		_r.getElement(elementId)
		_r.layerHistory.push(_r.currentElement);
		_r.currentElement = elementId

		//marker knap
		if(document.getElementById(elementId+"Button")){			
			if(_r.currentButtons[0] !=null) document.getElementById(_r.currentButtons[0]).className = "nonChoosen"
			_r.currentButtons[0] = elementId+"Button"
			document.getElementById(_r.currentButtons[0]).className = "choosen";
			//
			
			if(_r.currentButtons[1] !=null) document.getElementById(_r.currentButtons[1]).className = "nonChoosen"
			_r.currentButtons[1] = elementId+"ByName"
			document.getElementById(_r.currentButtons[1]).className = "choosen"
			//
			
			if(_r.currentButtons[2] !=null) document.getElementById(_r.currentButtons[2]).className = "nonChoosen"
			_r.currentButtons[2] = elementId+"ByTime"
			document.getElementById(_r.currentButtons[2]).className = "choosen"
			}
		}
		
		
		return false;
	}


	
_r.goBack = function (){	
		//history.back();
		elementId = _r.layerHistory.pop();	
		_r.showElement(elementId);
		_r.layerHistory.pop();	
	}
	

//COOKIE STUFF///////////////////////////////

_r.setDetailCookie = function (elementId,isShowened){
	
for(var i=0;i<_r.shownHiddenDetails.length;i++){
		if(_r.shownHiddenDetails[i].name == elementId)	_r.shownHiddenDetails[i].show=isShowened;
	}
}


_r.updateDetails = function (){
	for(var i=0;i<_r.shownHiddenDetails.length;i++){
	    if (_r.shownHiddenDetails[i].name.indexOf("showInherited")!=-1) {
	        _r.showHideInherited(_r.shownHiddenDetails[i].show,_r.shownHiddenDetails[i].name.replace("showInherited_",""));
         }
        else if (document.getElementById(_r.shownHiddenDetails[i].name)) {
	        document.getElementById(_r.shownHiddenDetails[i].name).className = (_r.shownHiddenDetails[i].show) ? "detailsVisible" : "hiddenElement";
	        document.getElementById(_r.shownHiddenDetails[i].name.replace("div|", "img|")).src = (_r.shownHiddenDetails[i].show) ? "OrteliusAjax/foldind.gif" : "OrteliusAjax/foldud.gif";
	    }
	}	
}

_r.xmlHttp = null;
_r.stateChanged = null;
	
//AJAX stuff ////////////////////////////////	
_r.getElement = function (elementId)
{
	
    _r.xmlHttp = _r.GetXmlHttpObject();

if (_r.xmlHttp==null){
return
} 
url="ortfiles/"+elementId+".html";//?sid="+Math.random()
//alert(url)
_r.xmlHttp.onreadystatechange = _r.stateChanged
_r.xmlHttp.open("GET",url,true);
_r.xmlHttp.send(null);
}


_r.stateChanged = function () {
	
	if(_r.xmlHttp.readyState != 4) return
	if(_r.xmlHttp.responseText == null) return;

var svar = _r.xmlHttp.responseText;

document.getElementById("content").innerHTML = svar;
_r.updateDetails();
} 

_r.GetXmlHttpObject = function ()
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

_r.toggleIsInherited = function(type) {
    for (var i = 0; i < _r.shownHiddenDetails.length; i++) {
        if (_r.shownHiddenDetails[i].name == "showInherited_"+type) {
            _r.showHideInherited(!_r.shownHiddenDetails[i].show,type);
        }
    }
}

_r.showHideInherited = function(show,type) {
if(type == undefined) return;
    selectorText = ".isInherited_"+type;
   // alert(selectorText)
    _r.setDetailCookie("showInherited_"+type, show);
    var theRules = new Array();
    if (document.styleSheets[0].cssRules) {
        theRules = document.styleSheets[0].cssRules;
    } else if (document.styleSheets[0].rules) {
        theRules = document.styleSheets[0].rules;
    }
    for (n in theRules) {
        if (theRules[n].selectorText == selectorText) {
            theRules[n].style.display = (show) ? 'table-row' : 'none';
        }
    }
    _r.changeToggleInheritedText(show, type);
  }


_r.changeToggleInheritedText = function(show,type) { 
	if(document.getElementById("btn_"+type)==undefined) return;
  document.getElementById("btn_"+type).innerHTML = (show) ? 'Hide inherited elements' : 'Show inherited elements';
  return;
  }


  return _r;
})();

$(document).ready(Ortelius.init);