//Copyright © 2024 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Linq;

namespace MandalaConsulting.Objects.API
{
    public class JObjectSerializer : SerializerBase<JObject>
    {
        public override JObject Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            BsonType bsonType = context.Reader.GetCurrentBsonType();
            if (bsonType == BsonType.Null)
            {
                context.Reader.ReadNull();
                return null;
            }

            BsonDocument bsonDocument = BsonDocumentSerializer.Instance.Deserialize(context, args);
            return JObject.Parse(bsonDocument.ToJson());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, JObject value)
        {
            if (value == null)
            {
                context.Writer.WriteNull();
                return;
            }

            BsonDocument bsonDocument = BsonDocument.Parse(value.ToString());
            BsonDocumentSerializer.Instance.Serialize(context, args, bsonDocument);
        }
    }
}