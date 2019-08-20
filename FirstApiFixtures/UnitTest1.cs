using FirstApi.Controllers;
using System;
using Xunit;

namespace FirstApiFixtures
{
    public class UnitTest1
    {
        [Fact]
        public void TestHelloReply()
        {
            ValuesController greet = new ValuesController();
            string actual = greet.Get("Hello").Value;
            string expected = "Hii";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestHiiReply()
        {
            ValuesController greet = new ValuesController();
            string actual = greet.Get("Hii").Value;
            string expected = "Hello";
            Assert.Equal(expected, actual);

        }
    }
}
