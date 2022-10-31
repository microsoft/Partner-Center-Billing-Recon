﻿// -----------------------------------------------------------------------
// <copyright file="Fragment.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Fragment
    {
        /// <summary>
        /// Get all attrbiutes
        /// </summary>
        Full,

        /// <summary>
        /// limited set of attributes
        /// </summary>
        Basic
    }
}
