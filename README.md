# ConditionalXmlComments
A tool to post-process the XML comments, in order to have conditions on them.

## Conditional XML comments
In the XML comments, you can add conditional statements as following:
```xml
  /// <summary>
  ///   Does something.
  /// </summary>
  /// <remarks>
  ///   Common info.
  ///   <if symbol="client">
  ///   Additional info for client only.
  ///   </if>
  /// </remarks>
```
If the `client` symbol is defined, the XML comment inside it is kept, otherwise, it is removed from the XML comments. If the content is kept, the `<if>` tag is removed.

## Running the tool
You can run the tool either:
- In the post build events, so it is run automatically each time the solution is run.
- In a command-line (batch file if multiple), so you can run it only when you need to generate the documentation.

The first argument of the program is the name of the XML comment file to process.

The following arguments are the names of the defined symbol.

The tool replaces the XML file with the processed one.
