<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="html" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" encoding="UTF-8" indent="yes"/>
<xsl:output method="html" name="text"  encoding="UTF-8"  indent="yes"/>
<xsl:variable name="basePath" select="docElements/basePath"/>


<xsl:template match="docElements">
		
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/introText.html</xsl:variable>
<html>
<head>
<title><xsl:value-of disable-output-escaping="yes" select="introHeader"/> | Actionscript documentation</title>
<link rel="stylesheet" href="OrteliusFrame/style.css" type="text/css" media="screen"/>
</head>
<body>


<div id="topBar"><div id="topNavigation">
 
</div>
    <div id="createdText">
    	    <xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href">introText</xsl:with-param>
                <xsl:with-param name="title">Intro</xsl:with-param>
                <xsl:with-param name="text">
					<b><xsl:value-of disable-output-escaping="yes" select="introHeader"/></b>
 		     	</xsl:with-param>
				<xsl:with-param name="topLevel">true</xsl:with-param>
            </xsl:call-template> 
    	
    
        was created <xsl:value-of disable-output-escaping="yes" select="created"/> with Ortelius - <a href="http://ortelius.marten.dk" target="_blank">ortelius.marten.dk</a></div></div>

<xsl:apply-templates select="allpackages"/>


<xsl:call-template name="indexMenu"/>





<div id="content">
<iframe frameborder="0" src="ortfiles/introText.html" id="contentIframe" name="contentFrame"/>
</div>

</body>
</html>

<xsl:result-document href="{$filename}" format="text">
	<html><xsl:call-template name="ElementStart"/><body>
<xsl:comment>Intro text</xsl:comment>
<div class="classDetails" ID="introText">
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="introHeader"/></div>
<xsl:value-of disable-output-escaping="yes" select="introText"/>    
</div>
<xsl:call-template name="ElementSlut"/></body></html>
</xsl:result-document>
	
<xsl:comment>Details of each class - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoClasses"/>

<xsl:comment>Details of each Method - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoMethod"/>

<xsl:comment>Details of each Properties - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoProperties"/>

<xsl:comment>Details of each Events - start</xsl:comment>
<xsl:apply-templates select="class" mode="DoEvents"/>
	
</xsl:template>


<!-- CLASS -->
<xsl:template match="class" mode="DoClasses">

<xsl:for-each select=".">

<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of select="package"/>.</xsl:if></xsl:if></xsl:variable>
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
	<html><xsl:call-template name="ElementStart"/><body>
<div class="classDetails" >
<xsl:attribute name="ID">
<xsl:value-of select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="name"/>
</xsl:attribute>
<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template></div>

<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">details</xsl:with-param>
<xsl:with-param name="text">Details</xsl:with-param>
</xsl:call-template>

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
<xsl:text>  </xsl:text>
	    	<xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href"><xsl:value-of select="@fullPath" /></xsl:with-param>
                <xsl:with-param name="title"><xsl:value-of select="."/></xsl:with-param>
                <xsl:with-param name="text">
					<xsl:value-of select="."/>
 		     	</xsl:with-param>
            </xsl:call-template> 
	
</xsl:for-each>
</div>
</xsl:if>
</div>

    <xsl:if test="import">
<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">import</xsl:with-param>
<xsl:with-param name="text">Imported packages</xsl:with-param>
</xsl:call-template>

        <div id="div|import" class="hiddenElement">
            <xsl:for-each select="import">
                <xsl:sort select="packageName"/>
                <div class="detailElement">
             <xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href"><xsl:value-of select="packageName/@fullPath" /></xsl:with-param>
                <xsl:with-param name="title"><xsl:value-of disable-output-escaping="yes" select="packageName"/></xsl:with-param>
                <xsl:with-param name="text"><xsl:value-of disable-output-escaping="yes" select="packageName"/></xsl:with-param>
            </xsl:call-template> 
         
                </div>
            </xsl:for-each>
        </div>
    </xsl:if>
    
<xsl:if test="example">
	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">example</xsl:with-param>
<xsl:with-param name="text">Example code</xsl:with-param>
</xsl:call-template>
	
	<div id="div|example" class="hiddenElement"><div class="detailElement"><code><xsl:value-of disable-output-escaping="yes" select="example"/></code></div></div>
	</xsl:if>

    <xsl:if test="see">
    	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">see</xsl:with-param>
<xsl:with-param name="text">See</xsl:with-param>
</xsl:call-template>


        <div id="div|see" class="hiddenElement">
            <xsl:for-each select="see"> 
 <xsl:if test="not(contains(.,'&lt;a '))">■ </xsl:if>           	
               <xsl:value-of disable-output-escaping="yes" select="replace(., '&lt;a ', '&lt;img src=../OrteliusFrame/arrowright.gif height=9 width=15/&gt; &lt;a ')"/>
                <br />
            </xsl:for-each>
        </div>
    </xsl:if>
	
	<xsl:variable name="inhLength" select="string-length(inheritanceHierarchy)"/>
	
<xsl:if test="method[@access = 'public']">
	
	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">publicmethod</xsl:with-param>
<xsl:with-param name="text">Public methods</xsl:with-param>
</xsl:call-template>
	
<!--	CONSTRUCTOR-->
<div id="div|publicmethod" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
<xsl:if test="method[name = $className]">
    <tr>
        <xsl:choose>
            <xsl:when test="@inherited='true'">
                <xsl:attribute name="class">methodPropertyLine isInherited</xsl:attribute>
            </xsl:when>
            <xsl:otherwise>
                <xsl:attribute name="class">methodPropertyLine</xsl:attribute>
            </xsl:otherwise>
        </xsl:choose>
        <td class="methodPropertyTitle">        	
        	 <xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href">
                    <xsl:value-of disable-output-escaping="yes" select="$packageName" />
						<xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="method[name = $className]/fid" />
                </xsl:with-param>
                <xsl:with-param name="title" select="method[name = $className]/codeLine"/>
                <xsl:with-param name="text" select="method[name = $className]/name"/>
            </xsl:call-template>  
        	

    	
</td><td class="methodPropertyModifiers">
<img border="0"><xsl:attribute name="src">../OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="modifiers/modifier"/></xsl:attribute></img>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="method[name = $className]/summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>
</xsl:if>


<!--	 NON CONSTRUCTOR METHODS-->
		
			<xsl:apply-templates select="method[name != $className and @access = 'public']" mode="DoMethodDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>
	</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">publicMehod</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>
	
	
<xsl:if test="method[@access = 'protected']">
	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">protectedmethod</xsl:with-param>
<xsl:with-param name="text">Protected methods</xsl:with-param>
</xsl:call-template>

<div id="div|protectedmethod" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
	
			<xsl:apply-templates select="method[name != $className and @access = 'protected']" mode="DoMethodDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>

</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">protectedMehod</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>

	
<xsl:if test="method[@access = 'internal']">
<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">internalmethod</xsl:with-param>
<xsl:with-param name="text">Internal methods</xsl:with-param>
</xsl:call-template>
	
<div id="div|internalmethod" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">

			<xsl:apply-templates select="method[name != $className and @access = 'internal']" mode="DoMethodDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>

</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">internalMethod</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>

	
	
	
<!--	PROPERTIES-->
<xsl:if test="property[@access = 'public']">
<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">publicproperties</xsl:with-param>
<xsl:with-param name="text">Public properties</xsl:with-param>
</xsl:call-template>

<div id="div|publicproperties" class="detailsVisible">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
			<xsl:apply-templates select="property[@access = 'public']" mode="DoPropDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>
</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">publicProperties</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>

	
<xsl:if test="property[@access = 'protected']">
	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">protectedproperty</xsl:with-param>
<xsl:with-param name="text">Protected properties</xsl:with-param>
</xsl:call-template>
	
<div id="div|protectedproperty" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
	
			<xsl:apply-templates select="property[@access = 'protected']" mode="DoPropDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>
	
	</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">protectedProperties</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>

	
	
	
<xsl:if test="property[@access = 'internal']">
	
	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">internalproperty</xsl:with-param>
<xsl:with-param name="text">Internal properties</xsl:with-param>
</xsl:call-template>

<div id="div|internalproperty" class="hiddenElement">
<table border="0" cellpadding="0" cellspacing="0" class="methodPropertyTabel">
	
			<xsl:apply-templates select="property[@access = 'internal']" mode="DoPropDetails">
				<xsl:sort select="name"/>
                <xsl:with-param name="packageName">
                    <xsl:value-of select="$packageName"/>
                </xsl:with-param>
                <xsl:with-param name="className">
                    <xsl:value-of select="$className"/>
                </xsl:with-param>
            </xsl:apply-templates>
		
	</table>
<xsl:call-template name="Explanation">
	<xsl:with-param name="type">internalProperties</xsl:with-param>
	<xsl:with-param name="inhLength"><xsl:value-of select="$inhLength"/></xsl:with-param>
</xsl:call-template>
</div>
</xsl:if>


	
	
	

<xsl:if test="event">
<div class="elementTitle">Events:</div>
<xsl:for-each select="event">
<xsl:sort select="name"/>
	
	<xsl:call-template name="arrowPageLink">
        <xsl:with-param name="href">
           <xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name"/><xsl:value-of select="fid"/>
        </xsl:with-param>
        <xsl:with-param name="title" select="name"/>
        <xsl:with-param name="text" select="name"/>
    </xsl:call-template>  
	<br/>
</xsl:for-each>
</xsl:if>
</div>

<xsl:call-template name="ElementSlut"/></body></html>
</xsl:result-document>

</xsl:if>
</xsl:for-each>


</xsl:template>


	
	



<!-- METHOD DETAIL PAGES -->
<xsl:template match="class" mode="DoMethod">

<xsl:for-each select=".">
		
<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="string-length(package)!=0"><xsl:value-of select="package"/>.</xsl:if></xsl:variable>

<xsl:for-each select="method">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>


<xsl:result-document href="{$filename}" format="text">
	<html><xsl:call-template name="ElementStart"/><body>

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

<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">methoddetails</xsl:with-param>
<xsl:with-param name="text">Details</xsl:with-param>
</xsl:call-template>

<div id="div|methoddetails" class="hiddenElement">
<div class="codeLine"><xsl:value-of select="codeLine"/></div>
<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b>
	<xsl:call-template name="simplePageLink">
        <xsl:with-param name="href" select="@inheritedFrom"/>
        <xsl:with-param name="text"><xsl:value-of select="@inheritedFrom"/></xsl:with-param>
    </xsl:call-template>
	</div>
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
	
<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">parameters</xsl:with-param>
<xsl:with-param name="text">Parameters</xsl:with-param>
</xsl:call-template>

<div id="div|parameters" class="detailsVisible">

<xsl:for-each select="param">
<div class="detailElement"><b><xsl:value-of disable-output-escaping="yes" select="name"/></b> : 
		<xsl:call-template name="simplePageLink">
        <xsl:with-param name="href" select="type/@fullPath"/>
        <xsl:with-param name="text">
        	<span class="codeLine">
  	   			<xsl:value-of disable-output-escaping="yes" select="type"/>
 		   </span>
		</xsl:with-param>
    </xsl:call-template>
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
	<xsl:call-template name="simplePageLink">
        <xsl:with-param name="href" select="returns/type/@fullPath"/>
        <xsl:with-param name="text">
		    <div class="codeLine">
      <xsl:value-of disable-output-escaping="yes" select="returns/type"/>
    </div>
		</xsl:with-param>
    </xsl:call-template>

<div class="summary"><xsl:value-of disable-output-escaping="yes" select="returns/summary"/></div>
</div>
</xsl:if>


  <xsl:if test="example">
  	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">example</xsl:with-param>
<xsl:with-param name="text">Example code</xsl:with-param>
</xsl:call-template>

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

<xsl:call-template name="ElementSlut"/></body></html>
</xsl:result-document>
</xsl:for-each>

</xsl:if>
</xsl:for-each>
</xsl:template>




<!-- PROPERTIIES DETAIL PAGES -->
<xsl:template match="class" mode="DoProperties">

<xsl:for-each select=".">

<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>

<xsl:for-each select="property">

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
	<html><xsl:call-template name="ElementStart"/><body>

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<br />

	<xsl:call-template name="toggleDetailsHeader">
<xsl:with-param name="name">propdetails</xsl:with-param>
<xsl:with-param name="text">Details</xsl:with-param>
</xsl:call-template>

<div id="div|propdetails" class="hiddenElement">
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="codeLine"/></div>
<xsl:if test="@inheritedFrom">
<div class="detailElement"><b>Inherited from: </b>
	<xsl:call-template name="simplePageLink">
        <xsl:with-param name="href" select="@inheritedFrom"/>
        <xsl:with-param name="text"><xsl:value-of select="@inheritedFrom"/></xsl:with-param>
    </xsl:call-template>
	</div>
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
	
	<xsl:call-template name="simplePageLink">
        <xsl:with-param name="href" select="type/@fullPath"/>
        <xsl:with-param name="text">
        	<div class="codeLine">
  	   			<xsl:value-of disable-output-escaping="yes" select="type"/>
 		   </div>
		</xsl:with-param>
    </xsl:call-template>

  
<div class="detailHeader">Default value</div>
<xsl:value-of disable-output-escaping="yes" select="defaultValue"/>
<xsl:call-template name="backButton"/>

</div>



<xsl:call-template name="ElementSlut"/></body></html>
</xsl:result-document>
</xsl:for-each>


</xsl:if>
</xsl:for-each>
</xsl:template>


<!-- EVENT DETAIL PAGES -->
<xsl:template match="class" mode="DoEvents">

<xsl:for-each select=".">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:if></xsl:variable>


<xsl:for-each select="event">
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.<xsl:value-of select="name" /><xsl:value-of select="fid"/>.html</xsl:variable>

<xsl:result-document href="{$filename}" format="text">
	<html><xsl:call-template name="ElementStart"/><body>

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
<xsl:call-template name="ElementSlut"/></body></html>
</xsl:result-document>
</xsl:for-each>
</xsl:for-each>
</xsl:template>

	

	
<!-- property list content-->
<xsl:template match="property" mode="DoPropDetails">
    <xsl:param name="packageName"/>	
    <xsl:param name="className"/>
	
	<tr>
    <xsl:choose>
        <xsl:when test="@inherited='true'">
            <xsl:attribute name="class">methodPropertyLine isInherited</xsl:attribute>
        </xsl:when>
        <xsl:otherwise>
            <xsl:attribute name="class">methodPropertyLine</xsl:attribute>
        </xsl:otherwise>
    </xsl:choose>
<td class="methodPropertyTitle">
	           <xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href">
                    <xsl:value-of select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/><xsl:value-of select="fid"/>
                </xsl:with-param>
                <xsl:with-param name="title" select="codeLine"/>
                <xsl:with-param name="text" select="name"/>
            </xsl:call-template>  
</td>
		<td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">../OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
    <img border="0" src="../OrteliusFrame/inheritedmodifier.gif" title="inherited"/>
  </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td>
	</tr>
</xsl:template>

	
<!-- method list content-->
<xsl:template match="method" mode="DoMethodDetails">
    <xsl:param name="packageName"/>	
    <xsl:param name="className"/>
	
  <tr>
        <xsl:choose>
            <xsl:when test="@inherited='true'">
                <xsl:attribute name="class">methodPropertyLine isInherited</xsl:attribute>
            </xsl:when>
            <xsl:otherwise>
                <xsl:attribute name="class">methodPropertyLine</xsl:attribute>
            </xsl:otherwise>
        </xsl:choose>
        <td class="methodPropertyTitle">
            <xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href">
                    <xsl:value-of select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/><xsl:value-of select="fid"/>
                </xsl:with-param>
                <xsl:with-param name="title" select="codeLine"/>
                <xsl:with-param name="text" select="name"/>
            </xsl:call-template>                   
</td>
  	<td class="methodPropertyModifiers"><img border="0" src="../OrteliusFrame/modifier.gif"/>
<xsl:for-each select="modifiers/modifier">
<xsl:text> </xsl:text>
<img border="0"><xsl:attribute name="src">../OrteliusFrame/<xsl:value-of disable-output-escaping="yes" select="."/>modifier.gif</xsl:attribute><xsl:attribute name="title"><xsl:value-of disable-output-escaping="yes" select="."/></xsl:attribute></img>
</xsl:for-each>
  <xsl:if test="@inherited='true'">
   <img border="0" src="../OrteliusFrame/inheritedmodifier.gif" title="inherited"/>
 </xsl:if>
</td><td class="methodPropertySummary"><xsl:value-of disable-output-escaping="yes" select="summary"/><img border="0" src="../OrteliusFrame/modifier.gif"/></td></tr>

</xsl:template>


	
<!-- REMOVE A LINE BREAK WITH A BR HTML TAG -->
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
	

<!--  -->
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

	
	
	
	
	
<!-- THE PACKAGE TREE - JAVASCRIPT -->
<xsl:template match="packagelevel">
<div class="treeBranch">
<xsl:attribute name="ID">a|<xsl:value-of select="@fullname"/></xsl:attribute>
<xsl:value-of disable-output-escaping="yes" select="@name"/>
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



<!-- THE PACKAGE TREE, CLASS LINKS - JAVASCRIPT -->
<xsl:template match="packageClass">
<xsl:param name="extraTitle" select="'Button'"/>

<xsl:variable name="packageNameVar"><xsl:if test="@package"><xsl:if test="string-length(@package)!=0"><xsl:value-of disable-output-escaping="yes" select="@package"/>.</xsl:if></xsl:if></xsl:variable>

				<xsl:call-template name="arrowPageLink">
                <xsl:with-param name="href"><xsl:value-of select="$packageNameVar"/><xsl:value-of disable-output-escaping="yes" select="@class"/></xsl:with-param>
                <xsl:with-param name="title"><xsl:value-of select="$packageNameVar"/><xsl:value-of disable-output-escaping="yes" select="@class"/></xsl:with-param>
                <xsl:with-param name="text">
				<span class="nonChoosen">
					<xsl:attribute name="ID"><xsl:value-of select="$packageNameVar"/><xsl:value-of select="@class"/><xsl:value-of select="$extraTitle"/></xsl:attribute>
					<xsl:value-of disable-output-escaping="yes" select="@class"/>
				</span>
 		     	</xsl:with-param>
				<xsl:with-param name="topLevel">true</xsl:with-param>
            </xsl:call-template> 
	
	
	<br/>
</xsl:template>


<!-- THE APHABETIC LIST OF ALL CLASSES - JAVASCRIPT -->
<xsl:template name="indexMenu">
	<div id="indexMenu"><b>Packages and classes</b></div>
</xsl:template>



<!-- THE BACK BUTTON - JAVASCRIPT -->
<xsl:template name="backButton">
<div class="backButton"></div>
</xsl:template>


<!-- THE HEADER THAT TOGGLES DIFFERENT TYPES OF DETAILS - JAVASCRIPT -->
<xsl:template name="toggleDetailsHeader">
    <xsl:param name="name"/>
    <xsl:param name="text"/>
	<div class="detailHeader">		
			<xsl:value-of select="$text"/>
</div>
</xsl:template>
	
	
<!-- A LINK TO A METHOD/PROPERTY/CLASS WITH AN RED ARROW IN FRONT - JAVASCRIPT -->
    <xsl:template name="arrowPageLink">
        <xsl:param name="href"/>
        <xsl:param name="title"/>
        <xsl:param name="text"/>
        <xsl:param name="topLevel"/>
        <img border="0" height="9" width="15">
    		<xsl:attribute name="src"><xsl:if test="not($topLevel='true')">../</xsl:if>OrteliusFrame/arrowright.gif</xsl:attribute>
    	</img>
        <a target="contentFrame">
            <xsl:attribute name="href"><xsl:if test="$topLevel='true'">ortfiles/</xsl:if><xsl:value-of select="$href" />.html</xsl:attribute>
            <xsl:attribute name="title"><xsl:value-of select="$title"/></xsl:attribute>
            <xsl:copy-of select="$text"/>
        </a>
    </xsl:template>


<!-- A LINK TO A METHOD/PROPERTY/CLASS  - JAVASCRIPT -->
	    <xsl:template name="simplePageLink">
        <xsl:param name="href"/>
        <xsl:param name="text"/>
        <a target="contentFrame">
            <xsl:attribute name="href"><xsl:value-of select="$href" />.html</xsl:attribute>
            <xsl:copy-of select="$text"/>
        </a>
    </xsl:template>
	

<!-- THE BOTTOM EXPLANITION UNDER THE EACH LIST AF METHOD/PROPERTY - JAVASCRIPT -->
<xsl:template name="Explanation">	
    <xsl:param name="type"/>	
    <xsl:param name="inhLength"/>
	
    <div class="toggleInherited">
    	
	</div>

	<div class="modifierExplanation">
     <img src="../OrteliusFrame/staticmodifier.gif"/>=static | <img src="../OrteliusFrame/overridemodifier.gif"/>=overridden | <img src="../OrteliusFrame/dynamicmodifier.gif"/>=dynamic | <img src="../OrteliusFrame/finalmodifier.gif"/>=final | <img border="0" src="../OrteliusFrame/inheritedmodifier.gif" title="inherited"/>=inherited
	</div>
</xsl:template>


	
<xsl:template name="ElementStart">
<head>
<title>Actionscript documentation</title>
<link rel="stylesheet" href="../OrteliusFrame/style.css" type="text/css" media="screen"/>
</head>
</xsl:template>
	
<xsl:template name="ElementSlut">	
</xsl:template>
	
</xsl:stylesheet> 