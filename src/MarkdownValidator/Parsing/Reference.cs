﻿/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the Markdown Validator project
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/MarkdownValidator/blob/master/LICENSE
*/
using Markdig.Syntax;
using MihaZupan.MarkdownValidator.Helpers;
using System;

namespace MihaZupan.MarkdownValidator.Parsing
{
    internal class Reference : IEquatable<Reference>
    {
        public readonly string RawReference;
        public readonly string GlobalReference;
        public readonly SourceSpan SourceSpan;
        public readonly bool IsImage;
        public readonly bool IsUrl;
        public readonly bool IsCleanUrl;
        public readonly int Line;

        public Reference(string reference, string globalReference, SourceSpan span, int line, bool isImage = false, bool isUrl = false, bool isCleanUrl = false)
        {
            RawReference = reference;
            GlobalReference = globalReference;
            SourceSpan = span;
            Line = line;
            IsImage = isImage;
            IsUrl = isUrl;
            IsCleanUrl = isCleanUrl;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SourceSpan.GetHashCode() * 397) ^ Line;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is Reference other)
            {
                return Equals(other);
            }
            return false;
        }
        public bool Equals(Reference other)
        {
            if (other is null) return false;
            return other.Line == Line &&
                other.SourceSpan.Equals(SourceSpan) &&
                other.IsImage == IsImage &&
                other.IsUrl == IsUrl &&
                other.IsCleanUrl == IsCleanUrl &&
                other.GlobalReference.OrdinalEquals(GlobalReference) &&
                other.RawReference.OrdinalEquals(RawReference);
        }
        public static bool operator ==(Reference a, Reference b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Reference a, Reference b)
            => !(a == b);
    }
}
