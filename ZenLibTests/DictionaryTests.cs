﻿// <copyright file="DictionaryTests.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ZenLib.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ZenLib;
    using static ZenLib.Language;
    using static ZenLib.Tests.TestHelper;

    /// <summary>
    /// Tests for the Zen dictionary type.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DictionaryTests
    {
        /// <summary>
        /// Test that adding and then getting an element returns that element.
        /// </summary>
        [TestMethod]
        public void TestAddThenGetIsEqual()
        {
            CheckValid<IDictionary<int, int>>(d => d.Add(1, 1).Get(1).Value() == 1);
        }

        /// <summary>
        /// Check that adding to a dictionary evaluates correctly.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEvaluation()
        {
            var f = Function<IDictionary<int, int>, IDictionary<int, int>>(d => d.Add(1, 1).Add(2, 2));
            var result = f.Evaluate(new Dictionary<int, int>());
            Assert.AreEqual(result[1], 1);
            Assert.AreEqual(result[2], 2);
        }

        /// <summary>
        /// Test that the empty dictionary does not return anything for get.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEmpty()
        {
            RandomBytes(x => CheckAgreement<IDictionary<int, int>>(d => Not(EmptyDict<int, int>().Get(x).HasValue())));
        }

        /// <summary>
        /// Test that the empty dictionary does not return anything for get.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEvaluateOutput()
        {
            var f = Function<int, int, IDictionary<int, int>>((x, y) => EmptyDict<int, int>().Add(x, y));
            var d = f.Evaluate(1, 2);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(2, d[1]);

            f.Compile();
            d = f.Evaluate(1, 2);
            Assert.AreEqual(1, d.Count);
            Assert.AreEqual(2, d[1]);
        }

        /// <summary>
        /// Test that the empty dictionary does not return anything for get.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEvaluateInput()
        {
            CheckAgreement<IDictionary<int, int>>(d => d.ContainsKey(1));

            var f = Function<IDictionary<int, int>, bool>(d => d.ContainsKey(1));
            Assert.AreEqual(true, f.Evaluate(new Dictionary<int, int> { { 1, 2 } }));
            Assert.AreEqual(false, f.Evaluate(new Dictionary<int, int> { { 2, 1 } }));
        }

        /// <summary>
        /// Test that the dictionary works with strings.
        /// </summary>
        [TestMethod]
        public void TestDictionaryStrings()
        {
            var f = Function<IDictionary<string, string>, bool>(d => true);
            var sat = f.Find((d, allowed) =>
            {
                return And(
                    d.Get("k1").Value() == "v1",
                    d.Get("k2").Value() == "v2",
                    d.Get("k3").Value() == "v3");
            });

            Assert.IsTrue(sat.HasValue);
            Assert.AreEqual("v1", sat.Value["k1"]);
            Assert.AreEqual("v2", sat.Value["k2"]);
            Assert.AreEqual("v3", sat.Value["k3"]);
        }
    }
}
