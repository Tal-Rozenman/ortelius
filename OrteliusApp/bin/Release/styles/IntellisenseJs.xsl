<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">
<xsl:output method="text" name="text" encoding="UTF-8" indent="no" media-type="text/javascript"/>
<xsl:variable name="basePath" select="docElements/basePath"/>
<xsl:variable name="language" select="docElements/language"/>


<xsl:template match="docElements">			
<xsl:apply-templates select="class" mode="DoClasses"/>
</xsl:template>


<!-- CLASS -->
<xsl:template match="class" mode="DoClasses">

<xsl:for-each select=".">

<xsl:if test="name">
<xsl:variable name="className" select="name"/>
<xsl:variable name="packageName"><xsl:if test="package"><xsl:if test="string-length(package)!=0"><xsl:value-of select="translate(package,'.','/')"/>/</xsl:if></xsl:if></xsl:variable>
<xsl:variable name="filename"><xsl:value-of select="$basePath" />/intellisense/<xsl:value-of select="$packageName" /><xsl:value-of select="$className" />.intellisense.js</xsl:variable>  
  <xsl:result-document href="{$filename}" format="text">intellisense.annotate(window, {
//<xsl:call-template name="RemoveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template><xsl:text>
</xsl:text>
<xsl:value-of disable-output-escaping="yes" select="name"/>: undefined
});
intellisense.annotate(<xsl:value-of disable-output-escaping="yes" select="name"/>, {<xsl:text>
</xsl:text>

<xsl:if test="method[@access = 'public']">
<xsl:apply-templates select="method[name != $className and @access = 'public']" mode="DoMethodDetails">
				<xsl:sort select="name"/>
            </xsl:apply-templates>
</xsl:if>
<!--	PROPERTIES-->
<xsl:if test="property[@access = 'public']"> 
			<xsl:variable name="type">publicproperties</xsl:variable>	
			<xsl:apply-templates select="property[@access = 'public']" mode="DoPropDetails">
				<xsl:sort select="name"/>
            </xsl:apply-templates> 
</xsl:if>toString: function () {
///&lt;signature&gt;
  ///&lt;summary&gt;Test beskrivelse&lt;/summary&gt;
  ///&lt;returns type="string" /&gt;
///&lt;/signature&gt;
}
});	
</xsl:result-document>
</xsl:if>
</xsl:for-each>
</xsl:template>


<!-- property list content-->
<xsl:template match="property" mode="DoPropDetails">//<xsl:call-template name="RemoveLineBreaks">
<xsl:with-param name="text" select="summary"/>
</xsl:call-template><xsl:text>
</xsl:text>
<xsl:value-of disable-output-escaping="yes" select="name"/>:undefined,
</xsl:template>
  
  
<!-- method list content-->
<xsl:template match="method" mode="DoMethodDetails"><xsl:value-of disable-output-escaping="yes" select="name"/> : function () {
///&lt;signature&gt;
  ///&lt;summary&gt;<xsl:call-template name="RemoveLineBreaks">
  <xsl:with-param name="text" select="summary"/>
</xsl:call-template>&lt;/summary&gt;<xsl:for-each select="param">
  ///&lt;param name="<xsl:value-of disable-output-escaping="yes" select="name"/>" type="<xsl:value-of disable-output-escaping="yes" select="type"/>"&gt;
    ///<xsl:call-template name="RemoveLineBreaks">
                        <xsl:with-param name="text" select="summary"/>
                    </xsl:call-template>
<xsl:text>
  ///</xsl:text>&lt;/param&gt;</xsl:for-each><xsl:if test="returns">
    ///&lt;returns type="<xsl:value-of disable-output-escaping="yes" select="returns/type"/>"/&gt;</xsl:if>
///&lt;/signature&gt;
},
</xsl:template>

  
  
<!-- REMOVE A LINE BREAK -->
<xsl:template name="RemoveLineBreaks">
    <xsl:param name="text"/>
    <xsl:choose>
        <xsl:when test="contains($text,'&lt;br/&gt;')">
            <xsl:value-of disable-output-escaping="yes" select="substring-before($text,'&lt;br/&gt;')"/>
            
            <xsl:call-template name="RemoveLineBreaks">
                <xsl:with-param name="text">
                    <xsl:value-of disable-output-escaping="yes" select="substring-after($text,'&lt;br/&gt;')"/>
                </xsl:with-param>
            </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of disable-output-escaping="yes" select="$text"/>
        </xsl:otherwise>
    </xsl:choose>
</xsl:template>
	
</xsl:stylesheet> 