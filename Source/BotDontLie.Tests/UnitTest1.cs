// <copyright file="UnitTest1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BotDontLie.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// This is the necessary test class.
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// This is the setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// This is the test method.
        /// </summary>
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        /// <summary>
        /// This is a simple test method to have the unit testing work.
        /// </summary>
        [Test]
        public void MyTestMethod()
        {
            var testString = "hello";
            var expected = "hello";
            Assert.AreEqual(expected, testString);
            Assert.Pass("This unit test: MyTestMethod() passed!");
        }
    }
}