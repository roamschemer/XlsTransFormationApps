using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

var oldDocument = XDocument.Load("old.xml");

var xslt = "";
using (var sr = new StreamReader("trans.xslt")) {
    xslt = sr.ReadToEnd();
}

var newDocument = new XDocument();

using (var stringReader = new StringReader(xslt)) {
    using var xsltReader = XmlReader.Create(stringReader);
    var transformer = new XslCompiledTransform();
    transformer.Load(xsltReader);
    using var oldDocumentReader = oldDocument.CreateReader();
    using var newDocumentWriter = newDocument.CreateWriter();
    transformer.Transform(oldDocumentReader, newDocumentWriter);
}

string result = newDocument.ToString();
Console.WriteLine(result);
newDocument.Save("new.xml");

