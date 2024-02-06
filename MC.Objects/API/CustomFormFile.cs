//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MC.API.Objects
{
    public class CustomFormFile : IFormFile
    {
        public CustomFormFile()
        {
        }
   
        public CustomFormFile(string fileName, byte[] content)
        {
            FileName = fileName ?? throw new System.ArgumentNullException(nameof(fileName));
            Content = content ?? new byte[0];
        }

        public string ContentType => "application/octet-stream";

        public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{FileName}\"";

        public IHeaderDictionary Headers => new HeaderDictionary();

        public long Length {
            get => Content.Length;
        }

        public string Name => "file";

        [BsonId]
        public string FileName {
            get;set;
        }
        public byte[] Content {
            get;set;
        }

        public void CopyTo(Stream target)
        {
            target.Write(Content, 0, Content.Length);
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            return target.WriteAsync(Content, 0, Content.Length, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(Content);
        }
    }

}
