Imports System.Text
Imports System.Xml
Imports System.Xml.Linq

Public Module PrettyXML_
    Public Function PrettyXml(ByVal xml As String) As String
        ' https://stackoverflow.com/a/14449850/338101
        Dim stringBuilder As New StringBuilder()
        Dim element As XElement = XElement.Parse(xml)
        Dim settings As New XmlWriterSettings With {
            .OmitXmlDeclaration = True,
            .Indent = True,
            .NewLineOnAttributes = True
        }

        Using xmlwriter As XmlWriter = XmlWriter.Create(stringBuilder, settings)
            element.Save(xmlwriter)
        End Using

        Return stringBuilder.ToString()

    End Function
End Module
