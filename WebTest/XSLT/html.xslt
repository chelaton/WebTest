<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" version="2.0" encoding="windows-1250" indent="yes" omit-xml-declaration="yes"/>
  <xsl:template match="/">
    <link rel="stylesheet" type="text/css" href="css/WebTest.css"/>
    <html>
      <div class="content">
      <table>
        <xsl:for-each select ="Items/Item">
          <tr>
            <td>
              <div class="imageAndText">
                <img src="Images/{Properties/Parameters/Parameter}"/>
                <div class="imgId circle">
                  <xsl:value-of select="@ItemID"/>
                </div>
                <div class="imageName">
                  <xsl:value-of select="@Title"/>
                </div>
              </div>
            </td>

          </tr>
        </xsl:for-each>
      </table>
        </div>
    </html>
  </xsl:template>
</xsl:stylesheet>
