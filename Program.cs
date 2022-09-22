/* Tool to post-process an XML comment file to process the conditional tags.
 *
 * Author:  OrdinaSoft
 *          Patrick Lanz
 *          Lausanne
 *          info@ordinasoft.ch
 *
 * First version: November 18, 2021
 */


using System.Diagnostics;
using System.Xml.Linq;


// File name
if (args.Length == 0)
{
  Console.WriteLine ("You must supply the name of the XML file to process.");
  return -1;
}

var fileName = args [0];

// Defined symbols
var symbols = new HashSet <String> ();

for (var i = 1; i < args.Length; i ++)
  symbols.Add (args [i]);


// Processes the file
var stopwatch = new Stopwatch ();
stopwatch.Start ();

var nbConditionals = 0;

try
{
  var doc = XDocument.Load (fileName, LoadOptions.PreserveWhitespace);

  foreach (var elmt in doc.Descendants ("if").ToArray ())
  {
    nbConditionals ++;

    var symbol = (String?) elmt.Attribute ("symbol");

    if (symbol is null)
    {
      Console.WriteLine ("Found <if> without Symbol attribute.");
      symbol = String.Empty;
    }

    if (symbols.Contains (symbol))
      elmt.AddAfterSelf (new XElement (elmt).Nodes ());

    elmt.Remove ();
  }

  doc.Save (fileName);
}

catch (Exception ex)
{
  Console.WriteLine ("Error during file processing.");
  Console.WriteLine (ex.Message);
  return -1;
}

stopwatch.Stop ();


// Indicates successful run
Console.WriteLine ($"Successfully processed {nbConditionals} conditions in {stopwatch.Elapsed}.");
return 0;