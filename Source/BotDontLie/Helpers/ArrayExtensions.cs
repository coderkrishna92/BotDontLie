// <copyright file="ArrayExtensions.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Helpers
{
    using System;

    /// <summary>
    /// This class will be used for array extensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// A method that will generate a sub array given an index, and the length of selection.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="data">The array itself.</param>
        /// <param name="index">The starting point of sub array extraction.</param>
        /// <param name="length">The length of the sub array.</param>
        /// <returns>A sub array of type T.</returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}