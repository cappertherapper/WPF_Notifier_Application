using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDemo;
using System;
using Moq;

namespace WPFDemo.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void IsValidTimeInterval_ValidNumber_ReturnsTrue()
        {
            var window = new MainWindow();

            bool result = window.IsValidTimeInterval("10");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidTimeInterval_InvalidInput_ReturnsFalse()
        {
            var window = new MainWindow();

            bool result = window.IsValidTimeInterval("not a number");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestAddingMessage()
        {
            var messages = new ListOfMessages();
            int initialCount = messages.Count;

            messages.Add("Test message", 10);
            Assert.AreEqual(initialCount + 1, messages.Count);
        }

    }
}
