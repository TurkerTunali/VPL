﻿namespace CaptiveAire.VPL.Metadata
{
    public class BlockMetadata : ElementMetadataBase
    {
        public string Id { get; set; }

        public ElementMetadata[] Elements { get; set; }
    }
}