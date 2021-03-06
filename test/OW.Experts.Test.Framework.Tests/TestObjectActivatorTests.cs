﻿using FluentAssertions;
using NUnit.Framework;

namespace OW.Experts.Test.Infrastructure.Tests
{
    [TestFixture]
    public class TestObjectActivatorTests
    {
        [Test]
        public void Create_CreateWithPrivateCtor()
        {
            var @object = TestObjectActivator.Create<ClassWithNonPublicCtor>();

            @object.Should().NotBeNull().And.BeOfType<ClassWithNonPublicCtor>();
        }

        private class ClassWithNonPublicCtor
        {
            private ClassWithNonPublicCtor()
            {
            }
        }
    }
}