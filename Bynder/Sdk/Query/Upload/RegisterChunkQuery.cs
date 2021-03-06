﻿// Copyright (c) Bynder. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using Bynder.Sdk.Query.Decoder;

namespace Bynder.Sdk.Query.Upload
{
    /// <summary>
    /// Class used to represent RegisterChunk response. This
    /// class should only be used by UploadFile
    /// </summary>
    internal class RegisterChunkQuery
    {
        /// <summary>
        /// Target id
        /// </summary>
        [ApiField("targetid")]
        public string TargetId { get; set; }

        /// <summary>
        /// S3 filename
        /// </summary>
        [ApiField("filename")]
        public string S3Filename { get; set; }

        /// <summary>
        /// Chunk number
        /// </summary>
        [ApiField("chunkNumber")]
        public string ChunkNumber { get; set; }

        /// <summary>
        /// Upload id
        /// </summary>
        public string UploadId { get; set; }
    }
}
