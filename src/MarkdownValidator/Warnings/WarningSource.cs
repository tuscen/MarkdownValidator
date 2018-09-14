﻿/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the Markdown Validator project
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/MarkdownValidator/blob/master/LICENSE
*/
using System;

namespace MihaZupan.MarkdownValidator.Warnings
{
    [Flags]
    public enum WarningSource
    {
        InternalParser = 1,
        ExternalParser = 2,
        CodeBlockParser = 4,

        ParserFinalize = InternalParser | 8,
        Validator = InternalParser | 16,

        InternalCodeBlockParser = InternalParser | CodeBlockParser,
        ExternalCodeBlockParser = ExternalParser | CodeBlockParser,
    }
}