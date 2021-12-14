# JSON
Contains snippets of code to handle JSON (C#)

Most of these code snippets assume the use of Newtonsoft Nuget Package.

1) `FormatDecimalsAsTextConverter.cs` - A custom converter that will convert/treat all decimal properties as text when **serializing**
2) `FormatNumberAsTextConverter.cs` - A custom converter that will convert/treat all number/int properties as text when **serializing**
3) `FormatDateAsTextConverter.cs` - A custom converter that will convert/treat all Date properties as text with specific formatting when **serializing** (e.g. yyyyMMdd - 20211412)


4) `StringTruncatingPropertyResolver.cs` - A contract resolver which will automatically truncate any string property based on the [MaxLength] attribute specified for the string property. This snippet is adoted from https://stackoverflow.com/questions/54119426/string-truncation-during-json-deserialization-and-ado-persistance 
We can customize the serialization (and deserialization ability) of the engine using a custom Contract Resolver. For more details, please see https://www.newtonsoft.com/json/help/html/contractresolver.htm
Remember, if using a custom ValueProvider(which implements `IValueProvider` interface, the `GetValue` method needs to be updated in order to customize **serialization** of a property, whereas `SetValue` gets called when **deserialization** happens from a JSON string to an object.


