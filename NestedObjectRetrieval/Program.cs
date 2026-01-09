/*
Nested Object Retrieval

Build a function that obtains the first 'textId' and the value for that id obtained from the 'texts' object. 
For example texts['globantId'] is 'GLOBANT>'. 'texts' and 'textId' can be at any level within the object rawData.

Use recursion to solve this problem

Example:
const rawData = {
    info1: {
        infon: {
            textId: 'globantId', 
        },
        texts: {
            globantId2: 'GLOBANT>>',
            globantId: 'GLOBANT>', 
        },
    },
}
const result = yourFunction(rawData);  // Returns 'GLOBANT>'
*/

object rawData = new Dictionary<string, object>
{
    ["info1"] = new Dictionary<string, object>
    {
        ["infon"] = new Dictionary<string, object>
        {
            ["textId"] = "globantId"
        },
        ["texts"] = new Dictionary<string, object>
        {
            ["globantId2"] = "GLOBANT>>",
            ["globantId"] = "GLOBANT>"
        }
    }
};


Console.WriteLine(NestedObjectRetrieval(rawData));

/// <summary>
/// Recursively retrieves the value associated with the first 'textId' found in the nested object.
/// </summary>
/// <param name="rawData">The nested object to search.</param>
/// <param name="textIdKey">The key used to identify the textId (default is 'textId').</param>
/// <param name="textsKey">The key used to identify the texts dictionary (default is 'texts').</param>
/// <returns>Returns the value associated with the first 'textId' found, or null if not found.</returns>
static string? NestedObjectRetrieval(object rawData, string textIdKey = "textId", string textsKey = "texts")
{
    // First, find the first textId value anywhere in the structure
    var textId = FindTextId(rawData, textIdKey);
    if (textId == null) return null;

    // Then, find the texts dictionary and lookup the textId in it
    var textsDict = FindTexts(rawData, textsKey);
    if (textsDict == null) return null;

    // Return the value from texts using the textId key
    return textsDict.TryGetValue(textId, out var result) && result is string resultStr
        ? resultStr
        : null;
}

/// <summary>
/// Recursively finds the first 'textId' value in the nested object.
/// </summary>
/// <param name="rawData">The nested object to search.</param>
/// <param name="textIdKey">The key used to identify the textId (default is 'textId').</param>
/// <returns>The first 'textId' value found, or null if not found.</returns>
static string? FindTextId(object rawData, string textIdKey = "textId") =>
    rawData is not IDictionary<string, object> dictionaryObj ? null :
    dictionaryObj.TryGetValue(textIdKey, out var textIdValue) && textIdValue is string textId ? textId :
    dictionaryObj
    .Where(it => it.Value is IDictionary<string, object>)
    .Select(it => FindTextId(it.Value, textIdKey))
    .FirstOrDefault(it => it != null);

/// <summary>
/// Recursively finds the first 'texts' dictionary in the nested object.
/// </summary>
/// <param name="rawData">The nested object to search.</param>
/// <param name="textsKey">The key used to identify the texts dictionary (default is 'texts').</param>
/// <returns>The first 'texts' dictionary found, or null if not found.</returns>
static IDictionary<string, object>? FindTexts(object rawData, string textsKey = "texts") =>
    rawData is not IDictionary<string, object> dictionaryObj ? null :
    dictionaryObj.TryGetValue(textsKey, out var texts) && texts is IDictionary<string, object> textsDict ? textsDict :
    dictionaryObj
    .Where(it => it.Value is IDictionary<string, object>)
    .Select(it => FindTexts(it.Value))
    .FirstOrDefault(it => it != null);
