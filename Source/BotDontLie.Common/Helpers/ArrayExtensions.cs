// <copyright file="ArrayExtensions.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Helpers
{
    using System;

    /// <summary>
    /// This class is being used for an extension method on the Array class called SubArray.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// A method that will generate a sub array given an index, and the length of the selection.
        /// </summary>
        /// <typeparam name="T">The generic type, which would be the type of the array.</typeparam>
        /// <param name="data">The array itself.</param>
        /// <param name="index">The starting point of the sub array extraction.</param>
        /// <param name="length">The length of the sub array.</param>
        /// <returns>An array which is the subarray of a generic type.</returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}