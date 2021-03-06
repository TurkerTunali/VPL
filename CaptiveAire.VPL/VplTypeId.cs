﻿using System;

namespace CaptiveAire.VPL
{
    /// <summary>
    /// The 'System' VplType Ids.
    /// </summary>
    public static class VplTypeId
    {
        public static readonly Guid Boolean = new Guid("F0D1BC68-1DB8-46EE-8E03-7861239AC763");

        /// <summary>
        /// double
        /// </summary>
        public static readonly Guid Float = new Guid("26D85D13-F9E9-4DFB-A093-E5650F396DE3");

        /// <summary>
        /// Sort of like 'object' in the .NET framework.
        /// </summary>
        public static readonly Guid Any = new Guid("17CA2480-3594-4885-BD58-BA5BD8D35493");

        public static readonly Guid String = new Guid("857D766D-6090-41AC-BEDF-65AAC184B726");

        public static readonly Guid Int = new Guid("BF29002F-8B73-423E-8B80-E606C940A7B6");

        public static readonly Guid Byte = new Guid("43DF7E97-5A5F-443F-97E9-17CD7EF123BF");

        public static readonly Guid SByte = new Guid("24B61776-EF80-4942-8E5D-419A2CD95DF5");

        public static readonly Guid Int16 = new Guid("DBE0E452-0EE2-4EEC-8402-1AE160DC5B41");

        public static readonly Guid UInt16 = new Guid("F4CBC063-A563-404A-86B4-DFE5ECED0C63");

        public static readonly Guid UInt32 = new Guid("BDC2D194-9F7D-4791-8DF7-0176D5CBF04A");

        public static readonly Guid Single = new Guid("565F75D8-F654-4B28-86FE-440A62B21CA2");

        public static readonly Guid DateTime = new Guid("42679804-015E-4F04-AC2B-8A97CF548E04");

        /// <summary>
        /// ulong
        /// </summary>
        public static readonly Guid UInt64 = new Guid("03789DB3-6F4F-4549-ACCE-E8AFF1048910");

        /// <summary>
        /// long
        /// </summary>
        public static readonly Guid Int64 = new Guid("F368712F-08B6-4265-9137-6E2E5C4C6545");

        /// <summary>
        /// decimal
        /// </summary>
        public static readonly Guid Decimal = new Guid("B4E7081E-D9F6-4F76-9F49-18B0C68A5FF8");
    }
}