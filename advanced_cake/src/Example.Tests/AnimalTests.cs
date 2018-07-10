using Example.Animals;
using NUnit.Framework;

namespace Example.Tests
{
	[TestFixture]
    public sealed class AnimalTests
    {
        [Test]
        public void The_Cat_Should_Meow()
        {
            var cat = new Cat();

            var result = cat.Talk();

            Assert.AreEqual("Meow", result);
        }

        [Test]
        public void The_Dog_Should_Bark()
        {
            var dog = new Dog();

            var result = dog.Talk();

            Assert.AreEqual("Woof", result);
        }
    }
}
