﻿/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the Markdown Validator project
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/MarkdownValidator/blob/master/LICENSE
*/
using MihaZupan.MarkdownValidator.Tests.Framework;
using MihaZupan.MarkdownValidator.Warnings;
using Xunit;

namespace MihaZupan.MarkdownValidator.Tests.ExternalUrlsTests
{
    public class WebRequestTimedOut
    {
        [Fact]
        public void NoTimeout()
        {
            string source = $"[Foo]({Constants.TEST_HOST})";
            SingleFileTest.AssertNotPresent(source, WarningIDs.WebRequestTimedOut);
        }

        [Fact]
        public void Timeout()
        {
            string url = $"{Constants.TEST_HOST}/delay/7";
            string source = $"[Foo]({url})";
            SingleFileTest.AssertWarning(source,
                WarningIDs.WebRequestTimedOut, 6, 6 + url.Length - 1, url);
        }

        [Fact]
        public void TimeoutAfterRedirect()
        {
            string url = $"{Constants.TEST_HOST}/redirect-to?url={Constants.TEST_HOST_ENCODED}%2Fdelay%2F7";
            string source = $"[Foo]({url})";
            SingleFileTest.AssertWarning(source,
                WarningIDs.WebRequestTimedOut, 6, 6 + url.Length - 1, url);
        }
    }
}