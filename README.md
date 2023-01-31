# SerializationExample
Serialization (known as pickling in python) is an easy way to convert an object to a binary representation that can then be e.g. written to disk or sent over a wire.
It’s useful e.g. for easy saving of settings to a file.
You can serialize your own classes if you mark them with [Serializable] attribute. This serializes all members of a class, except those marked as [NonSerialized].
.NET offers 2 serializers: binary, SOAP, XML. The difference between binary and SOAP is:
binary is more efficient (time and memory used)
binary is not human-readable. SOAP isn’t much better.
XML is slightly different:
it lives in System.Xml.Serialization
it uses [XmlIgnore] instead of [NonSerialized] and ignores [Serializable]
it doesn’t serialize private class members

Credits : https://blog.kowalczyk.info/article/8n/Serialization-in-C.html
        : https://stackoverflow.com/questions/5877808/what-is-serializable-and-when-should-i-use-it
