﻿/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the Markdown Validator project
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/MarkdownValidator/blob/master/LICENSE
*/
using MihaZupan.MarkdownValidator.Configuration;
using System;

namespace MihaZupan.MarkdownValidator
{
    /// <summary>
    /// Represents a context of markdown files and other referenceable files and directories.
    /// <para>It can be updated in real time, allowing for use in IDEs</para>
    /// <para>All method calls on an instance of this class are thread-safe</para>
    /// <para>It is up to you to make sure you call all adds/updates before calling Validate</para>
    /// </summary>
    public class MarkdownValidator
    {
        private readonly Config Configuration;
        private readonly ValidationContext Context;

        /// <summary>
        /// Constructs a new validation context with the specified <see cref="Config"/>
        /// </summary>
        /// <param name="configuration">The configuration to use when constructing, parsing and validating the context</param>
        public MarkdownValidator(Config configuration, IPipelineProvider pipelineProvider = null)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Context = new ValidationContext(Configuration);
        }

        public bool UpdateMarkdownFile(string filePath, string markdownSource)
        {
            EnsureValidPath(filePath, out string relativePath);
            return Context.UpdateMarkdownFile(relativePath, markdownSource);
        }

        public bool AddMarkdownFile(string filePath, string markdownSource)
        {
            EnsureValidPath(filePath, out string fullPath, out string relativePath);
            return Context.AddMarkdownFile(fullPath, relativePath, markdownSource);
        }
        public bool RemoveMarkdownFile(string filePath)
        {
            EnsureValidPath(filePath, out string relativePath);
            return Context.RemoveEntityFromIndex(relativePath, true);
        }

        public bool AddEntity(string path)
        {
            EnsureValidPath(path, out string relativePath);
            return Context.IndexEntity(relativePath);
        }
        public bool RemoveEntity(string path)
        {
            EnsureValidPath(path, out string relativePath);
            return Context.RemoveEntityFromIndex(relativePath, false);
        }

        /// <summary>
        /// This method will return very quickly, but does not guarantee a complete report if any async operations are still in progress internally
        /// </summary>
        /// <returns></returns>
        public ValidationReport Validate() => Context.Validate(false);
        /// <summary>
        /// This method may block if any async operations are still in progress internally
        /// </summary>
        /// <returns></returns>
        public ValidationReport ValidateFully() => Context.Validate(true);

        public void Clear() => Context.Clear();

        private void EnsureValidPath(string path, out string relativePath)
            => EnsureValidPath(path, out _, out relativePath);
        private void EnsureValidPath(string path, out string fullPath, out string relativePath)
            => Configuration.EnsurePathIsValidForContext(path, out fullPath, out relativePath);
    }
}