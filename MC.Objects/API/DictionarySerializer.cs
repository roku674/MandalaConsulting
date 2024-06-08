//Copyright © 2024 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MC.Objects
{
    public class DictionarySerializer : SerializerBase<Dictionary<string, object>>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Dictionary<string, object> value)
    {
        if (value == null)
        {
            context.Writer.WriteNull();
            return;
        }

        context.Writer.WriteStartDocument();
        foreach (KeyValuePair<string,object> kvp in value)
        {
            context.Writer.WriteName(kvp.Key);
            BsonSerializer.Serialize(context.Writer, kvp.Value?.GetType() ?? typeof(object), kvp.Value);
        }
        context.Writer.WriteEndDocument();
    }

    public override Dictionary<string, object> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        BsonType bsonType = context.Reader.GetCurrentBsonType();
        if (bsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        context.Reader.ReadStartDocument();
        while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
        {
            string key = context.Reader.ReadName();
            object value = BsonSerializer.Deserialize<object>(context.Reader);
            dictionary[key] = value;
        }
        context.Reader.ReadEndDocument();
        return dictionary;
    }
}
}
