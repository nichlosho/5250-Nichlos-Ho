using Mine.Models;
using NUnit.Framework;
namespace UnitTests.Models
{
    [TestFixture]
    public class ItemModelTests
    {
        [Test]
        public void ItemModel_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new ItemModel();

            // Reset


            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemModel_Set_Get_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new ItemModel();
            result.Text = "Text";
            result.Description = "Description";
            result.Id = "id";
            result.Value = 1;

            // Reset


            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual("Text", result.Text);
            Assert.AreEqual("Description", result.Description);
            Assert.AreEqual("id", result.Id);
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void ItemModel_Get_Valid_Default_Should_Pass()
        {
            // Arrange


            // Act
            var result = new ItemModel();

            // Reset


            // Assert 
            Assert.AreEqual(0, result.Value);
        }
    }
}