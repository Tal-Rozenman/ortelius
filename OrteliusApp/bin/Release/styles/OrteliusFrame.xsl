<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="html" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" encoding="UTF-8" indent="yes"/>
<xsl:output method="html" name="text" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" encoding="UTF-8" indent="yes"/>
<xsl:variable name="basePath" select="docElements/basePath"/>


<xsl:template match="docElements">
		
<html>
<head>
<title><xsl:value-of disable-output-escaping="yes" select="introHeader"/></title>
<link rel="stylesheet" href="OrteliusFrame/style.css" type="text/css" media="screen"/>
</head>
<body>



<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/introText.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<html>
<head/>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
<body>
<xsl:comment>Intro text</xsl:comment>
<div class="classDetails" ID="introText">
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="introHeader"/></div>
<xsl:value-of disable-output-escaping="yes" select="introText"/>    
</div>
</body>
</html>
</xsl:result-document>

<div id="topBar"><div id="createdText"><a target="contentFrame"><xsl:attribute name="href">ortfiles/introText.html</xsl:attribute><b><xsl:value-of disable-output-escaping="yes" select="introHeader"/></b></a> was created with Ortelius - <a href="http://ortelius.marten.dk" target="_blank">ortelius.marten.dk</a></div></div>

<xsl:apply-templates select="allpackages"/>

<xsl:comment>Details of each class - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoClasses"/>

<xsl:comment>Details of each Method - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoMethod"/>

<xsl:comment>Details of each Properties - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoProperties"/>

<xsl:comment>Details of each Events - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoEvents"/>

<div id="content">
<iframe frameborder="0" src="ortfiles/introText.html" id="contentIframe" name="contentFrame"/>
</div>

</body>
</html>

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
<div class="modifierExplanation"><img src="../OrteliusFrame/staticmodifier.gif"/>=static | <img src="../OrteliusFrame/overridemodifier.gif"/>=overridden | <img src="../OrteliusFrame/dynamicmodifier.gif"/>=dynamic</div>
</xsl:template>
	
   

	
	
<xsl:template match="class" mode="DoClasses">

<xsl:for-each select=".">
<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<html>
<head/>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
<body>
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



<div class="detailHeader">Details</div>
<div id="div|details" class="detailsVisible">
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
<b>→</b><a target="contentFrame"><xsl:attribute name="href"><xsl:value-of select="@fullPath" />.html</xsl:attribute><xsl:value-of select="."/></a>
</xsl:for-each>
</div>
</xsl:if>
</div>

<xsl:if test="example"><div class="detailHeader">Example code</div><div id="div|example" class="detailsVisible"><div class="detailElement"><code><xsl:value-of disable-output-escaping="yes" select="example"/></code></div></div></xsl:if>

<xsl:if test="import">
<div class="detailHeader">Imported packages</div>
<div id="div|import" class="detailsVisible">
<xsl:for-each select="import">
<xsl:sort select="packageName"/>
<div class="detailElement"><xsl:value-of disable-output-escaping="yes" select="packageName"/></div>
</xsl:for-each>
</div>
</xsl:if>
<br />
<br />

<xsl:if test="method[@access = 'public']">

<div class="detailHeader">Public methods</div>

<div id="div|publicmethod" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:if test="method[name = $className]">
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> <a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="method[name = $className]/name"/></a>
</td><td class="methodPropertyModifiers">
<img border="0"><xsl:attribute name="src">../OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/></xsl:attribute></img>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="method[name = $className]/summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:if>



<xsl:for-each select="method[name != $className and @access = 'public']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/><a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">../OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each></td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>

<xsl:if test="method[@access = 'protected']">
<div class="detailHeader">Protected methods</div>

<div id="div|protectedmethod" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="method[name != $className and @access = 'protected']">

<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> <a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each></td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>

<xsl:if test="property[@access = 'public']">

<div class="detailHeader"><a onclick="toggleDetails('div|publicproperties')"><img src="../OrteliusFrame/foldind.gif" ID="img|publicproperties" border="0" height="9" width="15"/>Public properties</a></div>

<div id="div|publicproperties" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="property[@access = 'public']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> <a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each></td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:for-each>
</table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>


<xsl:if test="property[@access = 'protected']">
<div class="detailHeader"><a onclick="toggleDetails('div|protectedproperty')"><img src="../OrteliusFrame/foldud.gif" ID="img|protectedproperty" border="0" height="9" width="15"/>Protected properties</a></div>

<div id="div|protectedproperty" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:for-each select="property[@access = 'protected']">
<xsl:sort select="name"/>
<tr class="methodPropertyLine"><td class="methodPropertyTitle">
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> <a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/> </a>
</td><td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each> </td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:for-each></table>
<xsl:call-template name="Explanation"/>
</div>
</xsl:if>


<xsl:if test="event">
<div class="elementTitle">Events:</div>
<xsl:for-each select="event">
<xsl:sort select="name"/>
<img src="../OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> <a target="contentFrame">
<xsl:attribute name="href"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>.html</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>
</a><br/>
</xsl:for-each>
</xsl:if>
</div>

</body>
</html>
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
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<html>
<head/>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
<body>

<div class="classDetails" >


<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of select="codeLine"/></div>
<br />





<div class="detailHeader">Details</div>

<div id="div|details" class="detailsVisible">

<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b> <a target="contentFrame"><xsl:attribute name="href"><xsl:value-of select="@inheritedFrom"/>.html</xsl:attribute><xsl:value-of select="@inheritedFrom"/></a></div>
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
<div class="detailHeader">Parameters</div>
<div id="div|parameters" class="detailsVisible">

<xsl:for-each select="param">
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="name"/> : <xsl:value-of disable-output-escaping="yes" select="type"/></div>
<br />
</xsl:for-each>

</div>
</xsl:if>



<xsl:if test="returns">
<div class="detailHeader">Return value</div>
<div id="div|parameters" class="detailsVisible">
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="returns/type"/></div>
<div class="summary"><xsl:value-of disable-output-escaping="yes" select="returns/summary"/></div>
</div>
</xsl:if>



</div>

</body>
</html>
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

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>
<xsl:result-document href="{$filename}" format="text">
<html>
<head/>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
<body>

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="codeLine"/></div><br />



<div class="detailHeader">Details</div>

<div id="div|details" class="detailsVisible">
<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b> <a target="contentFrame"><xsl:attribute name="href"><xsl:value-of select="@inheritedFrom"/>.html</xsl:attribute><xsl:value-of select="@inheritedFrom"/></a></div>
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

<div class="elementTitle">Type:</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="type"/></div>
<div class="elementTitle">Default value:</div>
<xsl:value-of disable-output-escaping="yes" select="defaultValue"/>


</div>



</body>
</html>
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
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
<html>
<head/>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
<body>

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>


</div>
</body>
</html>
</xsl:result-document>
</xsl:for-each>
</xsl:for-each>
</xsl:template>


<xsl:template match="allpackages">
<div id="classList" >

<xsl:apply-templates select="packagelevel">
<xsl:sort select="@name"/>
</xsl:apply-templates>

</div>
</xsl:template>


<xsl:template match="packagelevel">



<div class="treeBranch">
<xsl:value-of disable-output-escaping="yes" select="@name"/></div>
<div class="packageTreeVisible">
<xsl:attribute name="ID">div|<xsl:value-of select="@fullname"/></xsl:attribute>
<xsl:for-each select="packageClass">
<xsl:sort select="@package"/>
<xsl:sort select="@class"/>

<a target="contentFrame">
<xsl:attribute name="href">ortfiles/<xsl:value-of select="@package"/>.<xsl:value-of disable-output-escaping="yes" select="@class"/>.html</xsl:attribute>
<span class="nonChoosen">
<xsl:attribute name="ID"><xsl:value-of select="@package"/>.<xsl:value-of disable-output-escaping="yes" select="@class"/>Button</xsl:attribute>
<img src="OrteliusFrame/arrowright.gif" border="0" height="9" width="15"/> 
<xsl:value-of disable-output-escaping="yes" select="@class"/>
</span>
</a><br/>
</xsl:for-each>
<xsl:apply-templates select="packagelevel">
<xsl:sort select="@name"/>
</xsl:apply-templates>
</div>
</xsl:template>



</xsl:stylesheet>