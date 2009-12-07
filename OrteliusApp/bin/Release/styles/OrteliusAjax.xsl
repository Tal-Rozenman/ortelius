<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="html" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" encoding="UTF-8"  indent="yes"/>
<xsl:output method="html" name="text"  encoding="UTF-8"  indent="yes"/>
<xsl:variable name="basePath" select="docElements/basePath"/>


<xsl:template match="docElements">
		
<html>
<head>
<title><xsl:value-of disable-output-escaping="yes" select="introHeader"/></title>
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

<div id="topBar"><div id="topNavigation"><a><xsl:attribute name="onmousedown">goBack();</xsl:attribute><xsl:value-of select="@inheritedFrom"/><img src="OrteliusAjax/arrowleft.gif" border="0" height="8" width="18"/>  Back</a></div><div id="createdText"><a><xsl:attribute name="onmousedown">showElement('introText');</xsl:attribute><b><xsl:value-of disable-output-escaping="yes" select="introHeader"/></b></a> was created with Ortelius - <a href="http://ortelius.marten.dk" target="_blank">ortelius.marten.dk</a></div></div>

<xsl:apply-templates select="allpackages"/>

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

	
	
	
<xsl:template match="class" mode="DoClasses">

<xsl:for-each select=".">

<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>
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
</xsl:call-template><br />
<xsl:if test="see"><b>See: </b><xsl:value-of disable-output-escaping="yes" select="see"/><br /></xsl:if>
<br />
    
<xsl:if test="author"><b>Author: </b> <xsl:value-of disable-output-escaping="yes" select="author"/><br /></xsl:if>
<xsl:if test="copyright"><b>Copyright: </b> <xsl:value-of disable-output-escaping="yes" select="copyright"/><br /></xsl:if>
<xsl:if test="version"><b>Version: </b> <xsl:value-of disable-output-escaping="yes" select="version"/><br /></xsl:if>
<xsl:if test="playerversion"><b>Playerversion: </b> <xsl:value-of disable-output-escaping="yes" select="playerversion"/><br /></xsl:if>
<xsl:if test="langversion"><b>Langversion: </b> <xsl:value-of disable-output-escaping="yes" select="langversion"/><br /></xsl:if>
<xsl:if test="todo"><br />
<b>To be done: </b><xsl:for-each select="todo">
<xsl:value-of disable-output-escaping="yes" select="."/><br />
</xsl:for-each>
</xsl:if>


<xsl:if test="example"><b>Example code:</b><br /><code><xsl:value-of disable-output-escaping="yes" select="example"/></code><br /></xsl:if>

</div>
<br />

<xsl:if test="inheritanceHierarchy/*">
<br />
<b>Inheritance: </b> <xsl:value-of select="$className"/>
<xsl:for-each select="inheritanceHierarchy/inheritanceClass">
→<a><xsl:attribute name="onmousedown">showElement('<xsl:value-of select="@fullPath" />');return false;</xsl:attribute><xsl:value-of select="."/></a>
</xsl:for-each>

</xsl:if>

<xsl:if test="import">
<div class="elementTitle">Imported packages:</div>

<xsl:for-each select="import">
<xsl:sort select="packageName"/>
<xsl:value-of disable-output-escaping="yes" select="packageName"/><br/>
</xsl:for-each>
</xsl:if>

<xsl:if test="method[@access = 'public']">

<div class="elementTitle">Public methods:</div>


<xsl:if test="method[name = $className]">
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="method[name = $className]/name"/>
<img border="0"><xsl:attribute name="src">basic/<xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/></xsl:attribute></img>
</a><br/>
</xsl:if>


<xsl:for-each select="method[name != $className and @access = 'public']">
<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>


<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">basic/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>

</a><br/>
</xsl:for-each>
</xsl:if>


<xsl:if test="method[@access = 'protected']">
<div class="elementTitle">Protected methods:</div>

<xsl:for-each select="method[name != $className and @access = 'protected']">

<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>

<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">basic/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>

</a><br/>
</xsl:for-each>
</xsl:if>


<xsl:if test="property[@access = 'public']">
<div class="elementTitle">Public properties:</div>
<xsl:for-each select="property[@access = 'public']">
<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>

<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">basic/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>

</a><br/>
</xsl:for-each>
</xsl:if>


<xsl:if test="property[@access = 'protected']">
<div class="elementTitle">Protected properties:</div>
<xsl:for-each select="property[@access = 'protected']">
<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>

<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">basic/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>

</a><br/>
</xsl:for-each>
</xsl:if>

<xsl:if test="event">
<div class="elementTitle">Events:</div>
<xsl:for-each select="event">
<xsl:sort select="name"/>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> <a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/>');return false;</xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="name"/>
</a><br/>
</xsl:for-each>
</xsl:if>
</div>

</xsl:result-document>
</xsl:for-each>


</xsl:template>






<xsl:template match="class" mode="DoMethod">

<xsl:for-each select=".">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="string-length(package)!=0"><xsl:value-of select="package"/>.</xsl:if></xsl:variable>

<xsl:for-each select="method">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">

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

<xsl:if test="@inheritedFrom">
<b>Inherited from: </b> <a><xsl:attribute name="onmousedown">showElement('<xsl:value-of select="@inheritedFrom"/>');return false;</xsl:attribute><xsl:value-of select="@inheritedFrom"/></a>
<br />
</xsl:if>

<xsl:if test="param">
<div class="elementTitle">Parameters:</div>
<xsl:for-each select="param">
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="name"/> : <xsl:value-of disable-output-escaping="yes" select="type"/></div>

<br />
</xsl:for-each>
</xsl:if>

<xsl:if test="see">
<div class="elementTitle"><b>See: </b></div>
<xsl:for-each select="see">
<div class="summary"><xsl:copy-of select="."/></div>
</xsl:for-each>
</xsl:if>

<xsl:if test="returns">
<div class="elementTitle">Return value:</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="returns/type"/></div>
<div class="summary"><xsl:value-of disable-output-escaping="yes" select="returns/summary"/></div>
</xsl:if>

<xsl:if test="todo">
<div class="elementTitle">To be done:</div>
<xsl:for-each select="todo">
<div class="todo"><xsl:value-of disable-output-escaping="yes" select="."/></div>
</xsl:for-each>
</xsl:if>


<div class="backButton"><img src="OrteliusAjax/arrowleft.gif" border="0" height="8" width="18"/> <a onmousedown="goBack()">Back</a></div>

</div>

</xsl:result-document>
</xsl:for-each>
</xsl:for-each>
</xsl:template>



<xsl:template match="class" mode="DoProperties">

<xsl:for-each select=".">

<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>

<xsl:for-each select="property">

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>
<xsl:result-document href="{$filename}" format="text">

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="codeLine"/></div><br />

<xsl:if test="@inheritedFrom">
<b>Inherited from: </b> <xsl:value-of select="@inheritedFrom"/><br />
</xsl:if>

<xsl:if test="@readWrite!='ReadWrite'">
<div class="summary"><xsl:value-of select="@readWrite"/> only</div>
</xsl:if>

<div class="elementTitle">Type:</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="type"/></div>
<div class="elementTitle">Default value:</div>
<xsl:value-of disable-output-escaping="yes" select="defaultValue"/>
<div class="backButton"><img src="OrteliusAjax/arrowleft.gif" border="0" height="8" width="18"/> <a onmousedown="goBack()">Back</a></div>

</div>



</xsl:result-document>
</xsl:for-each>


</xsl:for-each>
</xsl:template>









<xsl:template match="class" mode="DoEvents">

<xsl:for-each select=".">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>


<xsl:for-each select="event">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="backButton"><img src="OrteliusAjax/arrowleft.gif" border="0" height="8" width="18"/> <a onmousedown="goBack()">Back</a></div>

</div>
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



<div class="treeBranch"><a><xsl:attribute name="onmousedown">toggleTreeElement(<xsl:value-of select="@fullname"/>);</xsl:attribute><xsl:value-of disable-output-escaping="yes" select="@name"/></a>
</div>
<div class="packageTreeVisible">
<xsl:attribute name="ID"><xsl:value-of select="@fullname"/></xsl:attribute>
<xsl:for-each select="packageClass">
<xsl:sort select="@package"/>
<xsl:sort select="@class"/>
<script language="JavaScript">allClasses.push('<xsl:value-of disable-output-escaping="yes" select="@package"/>.<xsl:value-of disable-output-escaping="yes" select="@class"/>')</script>
<a>
<xsl:attribute name="onmousedown">showElement('<xsl:value-of select="@package"/>.<xsl:value-of disable-output-escaping="yes" select="@class"/>');return false;</xsl:attribute>
<span class="nonChoosen">
<xsl:attribute name="ID"><xsl:value-of select="@package"/>.<xsl:value-of disable-output-escaping="yes" select="@class"/>Button</xsl:attribute>
<img src="OrteliusAjax/arrowright.gif" border="0" height="8" width="18"/> 
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