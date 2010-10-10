﻿<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="html" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" encoding="UTF-8" indent="yes"/>
<xsl:output method="html" name="text"  encoding="UTF-8"  indent="yes"/>
<xsl:variable name="basePath" select="docElements/basePath"/>


<xsl:template match="docElements">
		
<html>
<head>
<title><xsl:value-of disable-output-escaping="yes" select="introHeader"/> | Actionscript documentation</title>
<script language="JavaScript" type="text/javascript" src="OrteliusAjax/script.js"></script>
<link rel="stylesheet" href="OrteliusAjax/style.css" type="text/css" media="screen"/>
</head>
<body>

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/introText.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<xsl:comment>Intro text</xsl:comment>
<div class="classDetails" ID="introText">
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="introHeader"/></div>
<xsl:value-of disable-output-escaping="yes" select="introText"/>    
</div>
</xsl:result-document>

<div id="topBar"><div id="topNavigation"><a href="#"><xsl:attribute name="onclick">goBack();</xsl:attribute><xsl:value-of select="@inheritedFrom"/><img src="OrteliusAjax/arrowleft.gif" border="0" height="9" width="15"/>Back</a></div><div id="createdText"><a href="#"><xsl:attribute name="onclick">showElement('introText');</xsl:attribute><b><xsl:value-of disable-output-escaping="yes" select="introHeader"/></b></a> was created <xsl:value-of disable-output-escaping="yes" select="created"/> with Ortelius - <a href="http://ortelius.marten.dk" target="_blank">ortelius.marten.dk</a></div></div>

<xsl:apply-templates select="allpackages"/>

<xsl:comment>Index 1</xsl:comment>
<xsl:apply-templates select="classNameIndex" mode="indexByName"/>
<xsl:apply-templates select="classNameIndex" mode="indexByModifiedTime"/>

<xsl:call-template name="indexMenu"/>




<xsl:comment>Details of each class - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoClasses"/>

<xsl:comment>Details of each Method - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoMethod"/>

<xsl:comment>Details of each Properties - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoProperties"/>

<xsl:comment>Details of each Events - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoEvents"/>

<div id="content"/>

</body>
</html>

</xsl:template>


	
<xsl:template match="class" mode="DoClasses">

<xsl:for-each select=".">

<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of select="package"/>.</xsl:if></xsl:if></xsl:variable>
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<div class="classDetails" >
<xsl:attribute name="ID">
<xsl:value-of select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="name"/>
</xsl:attribute>
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template></div>

<xsl:if test="see"><div class="detailElement"><b>See: </b><xsl:value-of disable-output-escaping="yes" select="see"/></div></xsl:if>



<div class="detailHeader"><a href="#" onclick="toggleDetails('div|details')"><img src="OrteliusAjax/foldud.gif" ID="img|details" border="0" height="9" width="15"/>Details</a></div>
<div id="div|details" class="hiddenElement">
<div class="detailElement"><b>Package: </b> <xsl:value-of select="package "/></div>
<div class="detailElement"><b>File modified: </b> <xsl:value-of select="modified "/></div>
<xsl:if test="author"><div class="detailElement"><b>Author: </b> <xsl:value-of disable-output-escaping="yes" select="author"/></div></xsl:if>
<xsl:if test="copyright"><div class="detailElement"><b>Copyright: </b> <xsl:value-of disable-output-escaping="yes" select="copyright"/></div></xsl:if>
<xsl:if test="version"><div class="detailElement"><b>Version: </b> <xsl:value-of disable-output-escaping="yes" select="version"/></div></xsl:if>
<xsl:if test="playerversion"><div class="detailElement"><b>Playerversion: </b> <xsl:value-of disable-output-escaping="yes" select="playerversion"/></div></xsl:if>
<xsl:if test="langversion"><div class="detailElement"><b>Langversion: </b> <xsl:value-of disable-output-escaping="yes" select="langversion"/></div></xsl:if>
<xsl:if test="todo"><div class="detailElement">
<b>To be done: </b><xsl:for-each select="todo">
<xsl:value-of disable-output-escaping="yes" select="."/><br />
</xsl:for-each></div>
</xsl:if>

<xsl:if test="inheritanceHierarchy/*">
<div class="detailElement">
<b>Inheritance: </b> <xsl:value-of select="$className"/>
<xsl:for-each select="inheritanceHierarchy/inheritanceClass">
<b>→</b><a href="#"><xsl:attribute name="onclick">showElement('<xsl:value-of select="@fullPath" />');return false;</xsl:attribute><xsl:value-of select="."/></a>
</xsl:for-each>
</div>
</xsl:if>
</div>

<xsl:if test="example"><div class="detailHeader"><a href="#" onclick="toggleDetails('div|example')"><img src="OrteliusAjax/foldud.gif" ID="img|example" border="0" height="9" width="15"/>Example code</a></div><div id="div|example" class="hiddenElement"><div class="detailElement"><code><xsl:value-of disable-output-escaping="yes" select="example"/></code></div></div></xsl:if>

<xsl:if test="import">
<div class="detailHeader"><a href="#" onclick="toggleDetails('div|import')"><img src="OrteliusAjax/foldud.gif" ID="img|import" border="0" height="9" width="15"/>Imported packages</a></div>
<div id="div|import" class="hiddenElement">
<xsl:for-each select="import">
<xsl:sort select="packageName"/>   
<div class="detailElement">
  <a href="#">
    <xsl:attribute name="onclick">
      showElement('<xsl:value-of select="packageName/@fullPath"/>');return false;
    </xsl:attribute>
    <xsl:value-of disable-output-escaping="yes" select="packageName"/>
  </a></div>
</xsl:for-each>
</div>
</xsl:if>

<xsl:if test="method[@access = 'public']">

<div class="detailHeader"><a href="#" onclick="toggleDetails('div|publicmethod')"><img src="OrteliusAjax/foldind.gif" ID="img|publicmethod" border="0" height="9" width="15"/>Public methods</a></div>

<div id="div|publicmethod" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:if test="method[name = $className]">
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> <a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" />
<xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="method[name = $className]/fid" />');return false;</xsl:attribute>
<xsl:attribute name="title"><xsl:value-of select="method[name = $className]/codeLine"/></xsl:attribute>
<xsl:value-of select="method[name = $className]/name"/></a>
</td><td class="methodPropertyModifiers">
<img border="0"><xsl:attribute name="src">OrteliusAjax/<xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/></xsl:attribute></img>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="method[name = $className]/summary"/><img border="0" src="OrteliusAjax/modifier.gif"/></td></tr>
</xsl:if>



<xsl:for-each select="method[name != $className and @access = 'public']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/><a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/><xsl:value-of select="fid"/>');return false;</xsl:attribute>
<xsl:attribute name="title"><xsl:value-of select="codeLine"/></xsl:attribute>
<xsl:value-of select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="OrteliusAjax/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusAjax/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
   <img border="0" src="OrteliusAjax/inheritedmodifier.gif" title="inherited"/>
  </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="OrteliusAjax/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>

<xsl:if test="method[@access = 'protected']">
<div class="detailHeader"><a href="#" onclick="toggleDetails('div|protectedmethod')"><img src="OrteliusAjax/foldud.gif" ID="img|protectedmethod" border="0" height="9" width="15"/>Protected methods</a></div>

<div id="div|protectedmethod" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="method[name != $className and @access = 'protected']">

<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> <a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/><xsl:value-of select="fid"/>');return false;</xsl:attribute>
<xsl:attribute name="title"><xsl:value-of select="codeLine"/></xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="OrteliusAjax/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusAjax/<xsl:value-of select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
    <img border="0" src="OrteliusAjax/inheritedmodifier.gif" title="inherited"/>
  </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="OrteliusAjax/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>


<xsl:if test="property[@access = 'public']">

<div class="detailHeader"><a href="#" onclick="toggleDetails('div|publicproperties')"><img src="OrteliusAjax/foldind.gif" ID="img|publicproperties" border="0" height="9" width="15"/>Public properties</a></div>

<div id="div|publicproperties" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="property[@access = 'public']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> <a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="fid"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="OrteliusAjax/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusAjax/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
    <img border="0" src="OrteliusAjax/inheritedmodifier.gif" title="inherited"/>
  </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="OrteliusAjax/modifier.gif"/></td></tr>
</xsl:for-each>
</table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>


<xsl:if test="property[@access = 'protected']">
<div class="detailHeader"><a href="#" onclick="toggleDetails('div|protectedproperty')"><img src="OrteliusAjax/foldud.gif" ID="img|protectedproperty" border="0" height="9" width="15"/>Protected properties</a></div>

<div id="div|protectedproperty" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="property[@access = 'protected']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> <a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="fid"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="OrteliusAjax/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusAjax/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
    <img border="0" src="OrteliusAjax/inheritedmodifier.gif" title="inherited"/>
  </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="OrteliusAjax/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>


<xsl:if test="event">
<div class="elementTitle">Events:</div>
<xsl:for-each select="event">
<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> <a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="fid"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>
</a><br/>
</xsl:for-each>
</xsl:if>
</div>

</xsl:result-document>

</xsl:if>
</xsl:for-each>


</xsl:template>






<xsl:template match="class" mode="DoMethod">

<xsl:for-each select=".">
		
<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="string-length(package)!=0"><xsl:value-of select="package"/>.</xsl:if></xsl:variable>

<xsl:for-each select="method">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>


<xsl:result-document href="{$filename}" format="text">

<div class="classDetails" >

<!-- remove ?-->
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>

<br />





<div class="detailHeader"><a href="#" onclick="toggleDetails('div|methoddetails')"><img src="OrteliusAjax/foldud.gif" ID="img|methoddetails" border="0" height="9" width="15"/>Details</a></div>

<div id="div|methoddetails" class="hiddenElement">
<div class="codeLine"><xsl:value-of select="codeLine"/></div>
<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b> <a href="#"><xsl:attribute name="onclick">showElement('<xsl:value-of select="@inheritedFrom"/>');return false;</xsl:attribute><xsl:value-of select="@inheritedFrom"/></a></div>
</xsl:if>

<xsl:if test="see">
<div class="detailElement"><b>See: </b>
<xsl:for-each select="see">
<xsl:copy-of select="."/>
</xsl:for-each>
</div>
</xsl:if>

<xsl:if test="todo">
<div class="detailElement"><b>To be done: </b><br />
<xsl:for-each select="todo">
<xsl:value-of disable-output-escaping="yes" select="."/>
</xsl:for-each>
</div>
</xsl:if>


<xsl:if test="since">
<div class="detailElement"><b>Since: </b><xsl:value-of select="since"/></div>
</xsl:if>
</div>




<xsl:if test="param">
<div class="detailHeader"><a href="#" onclick="toggleDetails('div|parameters')"><img src="OrteliusAjax/foldud.gif" ID="img|parameters" border="0" height="9" width="15"/>Parameters</a></div>

<div id="div|parameters" class="detailsVisible">

<xsl:for-each select="param">
<div class="detailElement"><b><xsl:value-of disable-output-escaping="yes" select="name"/></b> : 
  <a href="#">
  <xsl:attribute name="onclick">
    showElement('<xsl:value-of select="type/@fullPath"/>');return false;
  </xsl:attribute>
    <span class="codeLine">
      <xsl:value-of disable-output-escaping="yes" select="type"/>
    </span>
</a>  
</div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<br />
</xsl:for-each>

</div>
</xsl:if>



<xsl:if test="returns">
<div class="detailHeader">Return value</div>
<div id="div|parameters" class="detailsVisible">
  <a href="#">
    <xsl:attribute name="onclick">
      showElement('<xsl:value-of select="returns/type/@fullPath"/>');return false;
    </xsl:attribute>
    <div class="codeLine">
      <xsl:value-of disable-output-escaping="yes" select="returns/type"/>
    </div>
  </a>
<div class="summary"><xsl:value-of disable-output-escaping="yes" select="returns/summary"/></div>
</div>
</xsl:if>


  <xsl:if test="example">
    <div class="detailHeader">
      <a href="#" onclick="toggleDetails('div|example')">
        <img src="OrteliusAjax/foldud.gif" ID="img|example" border="0" height="9" width="15"/>Example code
      </a>
    </div>
    <div id="div|example" class="hiddenElement">
      <div class="detailElement">
        <code>
          <xsl:value-of disable-output-escaping="yes" select="example"/>
        </code>
      </div>
    </div>
  </xsl:if>

<xsl:call-template name="backButton"/>

</div>

</xsl:result-document>
</xsl:for-each>

</xsl:if>
</xsl:for-each>
</xsl:template>










<xsl:template match="class" mode="DoProperties">

<xsl:for-each select=".">

<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>

<xsl:for-each select="property">

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<br />



<div class="detailHeader"><a href="#" onclick="toggleDetails('div|propdetails')"><img src="OrteliusAjax/foldud.gif" ID="img|propdetails" border="0" height="9" width="15"/>Details</a></div>

<div id="div|propdetails" class="hiddenElement">
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="codeLine"/></div>
<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b> <a href="#"><xsl:attribute name="onclick">showElement('<xsl:value-of select="@inheritedFrom"/>');return false;</xsl:attribute><xsl:value-of select="@inheritedFrom"/></a></div>
</xsl:if>

<xsl:if test="see">
<div class="detailElement"><b>See: </b>
<xsl:for-each select="see">
<xsl:copy-of select="."/>
</xsl:for-each>
</div>
</xsl:if>

<xsl:if test="todo">
<div class="detailElement"><b>To be done: </b><br />
<xsl:for-each select="todo">
<xsl:value-of disable-output-escaping="yes" select="."/>
</xsl:for-each>
</div>
</xsl:if>


<xsl:if test="since">
<div class="detailElement"><b>Since: </b><xsl:value-of select="since"/></div>
</xsl:if>
</div>




<xsl:if test="@readWrite!='ReadWrite'">
<div class="summary"><xsl:value-of select="@readWrite"/> only</div>
</xsl:if>

<div class="detailHeader">Type</div>
  <a href="#">
    <xsl:attribute name="onclick">
      showElement('<xsl:value-of select="type/@fullPath"/>');return false;
    </xsl:attribute>
    <div class="codeLine">
      <xsl:value-of disable-output-escaping="yes" select="type"/>
    </div>
  </a>
  
<div class="detailHeader">Default value</div>
<xsl:value-of disable-output-escaping="yes" select="defaultValue"/>
<xsl:call-template name="backButton"/>

</div>



</xsl:result-document>
</xsl:for-each>


</xsl:if>
</xsl:for-each>
</xsl:template>











<xsl:template match="class" mode="DoEvents">

<xsl:for-each select=".">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>


<xsl:for-each select="event">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<xsl:call-template name="backButton"/>

</div>
</xsl:result-document>
</xsl:for-each>
</xsl:for-each>
</xsl:template>


<xsl:template match="allpackages">
<div id="classList" >

<xsl:apply-templates select="packageClass">
<xsl:with-param name="extraTitle" select="'Button'"/>
<xsl:sort select="@package"/>
<xsl:sort select="@class"/>
</xsl:apply-templates>

<xsl:apply-templates select="packagelevel">
<xsl:sort select="@name"/>
</xsl:apply-templates>

</div>
</xsl:template>


<xsl:template match="packagelevel">



<div class="treeBranch">
<a href="#" class="nonChoosen"><xsl:attribute name="onclick">toggleTreeElement('div|<xsl:value-of select="@fullname"/>');</xsl:attribute>
<xsl:attribute name="ID">a|<xsl:value-of select="@fullname"/></xsl:attribute>
<img src="OrteliusAjax/foldind.gif" border="0" height="9" width="15"><xsl:attribute name="ID">img|<xsl:value-of select="@fullname"/></xsl:attribute></img>
<xsl:value-of disable-output-escaping="yes" select="@name"/></a>
</div>
<div class="packageTreeVisible">
<xsl:attribute name="ID">div|<xsl:value-of select="@fullname"/></xsl:attribute>

<xsl:apply-templates select="packageClass">
<xsl:sort select="@package"/>
<xsl:sort select="@class"/>
</xsl:apply-templates>

<xsl:apply-templates select="packagelevel">
<xsl:sort select="@name"/>
</xsl:apply-templates>
</div>
</xsl:template>




   
<xsl:template match="classNameIndex" mode="indexByName">
<div id="indexByName">

<xsl:apply-templates select="packageClass">
<xsl:with-param name="extraTitle" select="'ByName'"/>
<xsl:sort select="@class"/>
<xsl:sort select="@package"/>
</xsl:apply-templates>

</div>
</xsl:template>


    
<xsl:template match="classNameIndex" mode="indexByModifiedTime">
<div id="indexByModifiedTime">

<xsl:apply-templates select="packageClass">
<xsl:with-param name="extraTitle" select="'ByTime'"/>
<xsl:sort select="@modified" order="descending"/>
<xsl:sort select="@class"/>
<xsl:sort select="@package"/>
</xsl:apply-templates>

</div>
</xsl:template>


<xsl:template match="packageClass">
<xsl:param name="extraTitle" select="'Button'"/>

<xsl:variable name="packageNameVar"><xsl:if test="@package"><xsl:if test="string-length(@package)!=0"><xsl:value-of disable-output-escaping="yes" select="@package"/>.</xsl:if></xsl:if></xsl:variable>

<script language="JavaScript">allClasses.push('<xsl:value-of disable-output-escaping="yes" select="$packageNameVar"/><xsl:value-of disable-output-escaping="yes" select="@class"/>')</script>
<a href="#">
<xsl:attribute name="onclick">showElement('<xsl:value-of select="$packageNameVar"/><xsl:value-of disable-output-escaping="yes" select="@class"/>');return false;</xsl:attribute>
<xsl:attribute name="title"><xsl:value-of select="$packageNameVar"/><xsl:value-of disable-output-escaping="yes" select="@class"/></xsl:attribute>
<span class="nonChoosen">
<xsl:attribute name="ID"><xsl:value-of select="$packageNameVar"/><xsl:value-of select="@class"/><xsl:value-of select="$extraTitle"/></xsl:attribute>
<img src="OrteliusAjax/arrowright.gif" border="0" height="9" width="15"/> 
<xsl:value-of disable-output-escaping="yes" select="@class"/>
</span>
</a><br/>
</xsl:template>

    
<xsl:template name="indexMenu">
<div id="indexMenu"><a href="#" id="classListButton" onclick="changeIndex('classList')" class="choosen">Package tree</a> | <a href="#" id="indexByNameButton" onclick="changeIndex('indexByName')" class="nonChoosen">Sort by name</a> | <a href="#" id="indexByModifiedTimeButton" onclick="changeIndex('indexByModifiedTime')" class="nonChoosen">Sort by date</a></div>
</xsl:template>


<xsl:template name="backButton">
<div class="backButton"><a href="#" onclick="goBack()"><img src="OrteliusAjax/arrowleft.gif" border="0" height="9" width="15"/> Back</a></div>
</xsl:template>


<xsl:template name="PreserveLineBreaks">
    <xsl:param name="text"/>
    <xsl:choose>
        <xsl:when test="contains($text,'&#xA;')">
            <xsl:value-of disable-output-escaping="yes" select="substring-before($text,'&#xA;')"/>
            <br/>
            <xsl:call-template name="PreserveLineBreaks">
                <xsl:with-param name="text">
                    <xsl:value-of disable-output-escaping="yes" select="substring-after($text,'&#xA;')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of disable-output-escaping="yes" select="$text"/>
        </xsl:otherwise>
    </xsl:choose>
</xsl:template>

<xsl:template name="Explanation">
<div class="modifierExplanation"><img src="OrteliusAjax/staticmodifier.gif"/>=static | <img src="OrteliusAjax/overridenmodifier.gif"/>=overridden | <img src="OrteliusAjax/dynamicmodifier.gif"/>=dynamic | <img border="0" src="OrteliusAjax/inheritedmodifier.gif" title="inherited"/>=inherited</div>
</xsl:template>

</xsl:stylesheet>




 