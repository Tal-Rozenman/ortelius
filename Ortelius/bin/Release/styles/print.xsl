<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="html" indent="yes" name="html"/>
<xsl:variable name="basePath" select="docElements/basePath"/>

<xsl:template match="docElements">
		
<html>
<head>
<title>test</title>
</head>
<body>
<xsl:comment>Test</xsl:comment>


<xsl:apply-templates select="class" mode="DoProperties"/>


</body>
</html>

</xsl:template>



<xsl:template match="class" mode="DoProperties">

<xsl:for-each select=".">


<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="string-length(package)!=0"><xsl:value-of disable-output-escaping="yes" select="package"/>.</xsl:if></xsl:variable>

<xsl:variable name="filename"><xsl:value-of select="$basePath" />/ortfiles/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.html</xsl:variable>
<xsl:result-document href="{$filename}" format="html">
<html><body>
<xsl:comment>Details of each property</xsl:comment>
<xsl:for-each select="property">

<div class="classDetails" >
<xsl:attribute name="ID"><xsl:value-of disable-output-escaping="yes" select="$packageName" /><xsl:value-of disable-output-escaping="yes" select="$className" />.<xsl:value-of disable-output-escaping="yes" select="name"/></xsl:attribute>

<div class="classTitle"><xsl:value-of disable-output-escaping="yes" select="name"/></div>
<div class="summary">
<xsl:call-template name="PreserveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template>
</div>
<div class="codeLine"><xsl:value-of disable-output-escaping="yes" select="codeLine"/></div>

<br />

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
<div class="backButton"><img src="basic/arrowleft.gif" border="0" height="8" width="18"/> <a onclick="goBack()">Back</a></div>

</div>

</xsl:for-each>
 </body></html>
</xsl:result-document>

</xsl:for-each>
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

</xsl:stylesheet>