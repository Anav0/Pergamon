﻿
using System;
using System.Linq;

namespace Pergamon
{
    public static class StringExtention
    {
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
