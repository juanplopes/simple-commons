﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;
using System.Globalization;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class DictionaryHelperFixture
    {
        [Test]
        public void CreateSimpleObjectFromAnonymousTypes()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = 123, bbb = "asd" });

            value.Count.Should().Be(2);
            value["aaa"].Should().Be(123);
            value["bbb"].Should().Be("asd");
        }

        [Test]
        public void CreateSimpleObjectFromExpressions()
        {
            var value = DictionaryHelper.FromExpressions(aaa => 123, bbb => "asd");

            value.Count.Should().Be(2);
            value["aaa"].Should().Be(123);
            value["bbb"].Should().Be("asd");
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesWithNullableValues()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = (int?)null, bbb = (string)null });

            value.Count.Should().Be(2);
            value["aaa"].Should().Be(null);
            value["bbb"].Should().Be(null);
        }

        [Test]
        public void CreateSimpleObjectFromExpressionsWithNullableValues()
        {
            var value = DictionaryHelper.FromExpressions(aaa => null, bbb => null);

            value.Count.Should().Be(2);
            value["aaa"].Should().Be(null);
            value["bbb"].Should().Be(null);
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesCaseSensitive()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = 123, bbb = "asd" });

            value.Count.Should().Be(2);

            value.ContainsKey("aaa").Should().Be.True();
            value.ContainsKey("Aaa").Should().Be.False();

            value.ContainsKey("bbb").Should().Be.True();
            value.ContainsKey("Bbb").Should().Be.False();
        }

        [Test]
        public void CreateSimpleObjectFromExpressionsCaseSensitive()
        {
            Expression<Func<object, object>>[] array = new Expression<Func<object, object>>[] { 
                aaa => 123, bbb => "asd"};

            var value = DictionaryHelper.FromExpressions(array);

            value.Count.Should().Be(2);

            value.ContainsKey("aaa").Should().Be.True();
            value.ContainsKey("Aaa").Should().Be.False();

            value.ContainsKey("bbb").Should().Be.True();
            value.ContainsKey("Bbb").Should().Be.False();
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesCaseInsensitive()
        {
            var value = DictionaryHelper.FromAnonymous(StringComparer.InvariantCultureIgnoreCase, new { aaa = 123, bbb = "asd" });

            value.Count.Should().Be(2);

            value.ContainsKey("aaa").Should().Be.True();
            value.ContainsKey("Aaa").Should().Be.True();

            value.ContainsKey("bbb").Should().Be.True();
            value.ContainsKey("Bbb").Should().Be.True();
        }

        [Test]
        public void CreateSimpleObjectFromExpressionsCaseInsensitive()
        {
            var value = DictionaryHelper.FromExpressions(StringComparer.InvariantCultureIgnoreCase, aaa => 123, bbb => "asd");

            value.Count.Should().Be(2);

            value.ContainsKey("aaa").Should().Be.True();
            value.ContainsKey("Aaa").Should().Be.True();

            value.ContainsKey("bbb").Should().Be.True();
            value.ContainsKey("Bbb").Should().Be.True();
        }

        class Sample1
        {
            public DateTime AAA { get; set; }
            public string BBB { get; set; }
        }
        [Test]
        public void CreateSimpleObjectFromExistingClass()
        {
            var now = DateTime.Now;
            var obj = new Sample1() { AAA = now, BBB = "ASD" };
            var value = DictionaryHelper.FromAnonymous(obj);

            value.Count.Should().Be(2);
            value["AAA"].Should().Be(now);
            value["BBB"].Should().Be("ASD");
        }


        [Test]
        public void CreateSimpleObjectFromNullObject()
        {
            var value = DictionaryHelper.FromAnonymous(null);

            value.Count.Should().Be(0);
        }


    }
}
