using Mine.Models;
using NUnit.Framework;
namespace UnitTests.Models
{
    [TestFixture]
    public class HomeMenuItemTests
    {
        [Test]
        public void HomeMenuItem_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new HomeMenuItem();

            // Reset


            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void HomeMenuItem_Set_Get_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new HomeMenuItem();
            result.Title = "Text";
            result.Id = MenuItemType.Items;

            // Reset


            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual("Text", result.Title);
            Assert.AreEqual(MenuItemType.Items, result.Id);
        }

        [Test]
        public void HomeMenuItem_Get_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new HomeMenuItem();

            // Reset


            // Assert 
            Assert.AreEqual(MenuItemType.Items, result.Id);
        }
    }
}