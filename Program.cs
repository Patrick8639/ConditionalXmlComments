/* Tool to post-process an XML comment file to process the conditional tags.
 *
 * Author:  OrdinaSoft
 *          Patrick Lanz
 *          Lausanne
 *          info@ordinasoft.ch
 *
 * First version: November 18, 2021
 */


using System.Xml.Linq;


// File name
if (args.Length == 0)
{
  Console.WriteLine ("You must supply the name of the XML file to process.");
  return -1;
}

var FileName = args [0];

// Defined symbols
var Symbols = new HashSet <String> ();

for (var i = 1; i < args.Length; i++)
  Symbols.Add (args [i]);

// Processes the file
var NbConditionals = 0;

try
{
  var XDoc = XDocument.Load (FileName, LoadOptions.PreserveWhitespace);

  foreach (var Elmt in XDoc.Descendants ("if").ToArray ())
  {
    NbConditionals++;

    var Parent = Elmt.Parent!;
    Elmt.Remove ();

    var Symbol = (String?) Elmt.Attribute ("Symbol");

    if (Symbol is null)
    {
      Console.WriteLine ("Found <if> without Symbol attribute");
      Symbol = String.Empty;
    }

    if (Symbols.Contains (Symbol))
      Parent.Add (Elmt.Nodes ());
  }

  XDoc.Save (FileName);
}
catch (Exception ex)
{
  Console.WriteLine ("Error during file processing");
  Console.WriteLine (ex.Message);
  return -1;
}

// Indicates successful run
Console.WriteLine ($"Successfully processed {NbConditionals} conditions.");
return 0;