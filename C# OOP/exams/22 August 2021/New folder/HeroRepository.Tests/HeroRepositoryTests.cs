using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    private HeroRepository heroRepository;

    [SetUp]
    public void SetUp()
    {
        this.heroRepository = new HeroRepository();
    }

    [Test]
    public void ConstructorShouldWorksCorrectly()
    {
        Assert.IsNotNull(this.heroRepository);
    }

    [Test]
    public void HeroesCollectionShouldCollectOnlyHeroes()
    {
        CollectionAssert.AllItemsAreInstancesOfType(this.heroRepository.Heroes, typeof(Hero));
    }

    [Test]
    public void CreateMethodShouldWorksCorrectly()
    {
        var hero = new Hero("Alexa", 20);

        this.heroRepository.Create(hero);

        var expectedCount = 1;
        var actualCount = this.heroRepository.Heroes.Count;

        Assert.AreEqual(expectedCount, actualCount);
        Assert.That(this.heroRepository.Heroes, Contains.Item(hero));
    }

    [Test]
    public void CreateMethodShouldReturnCorrectMessage()
    {
        var hero = new Hero("Alexa", 20);

        var expectedMessage = "Successfully added hero Alexa with level 20";
        var actualMessage = this.heroRepository.Create(hero);

        Assert.AreEqual(expectedMessage, actualMessage);
    }

    [Test]
    public void CreateMethodShouldThrowsArgumentNullExceptionIfHeroIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => this.heroRepository.Create(null));
    }

    [Test]
    public void CreateMethodShouldThrowsInvalidOperationExceptionIfHeroWithThatNameExists()
    {
        var hero = new Hero("Alexa", 5);

        this.heroRepository.Create(hero);

        Assert.Throws<InvalidOperationException>(() => this.heroRepository.Create(new Hero("Alexa", 20)));
    }

    [Test]
    public void RemoveMethodShouldWorksCorrectly()
    {
        var hero = new Hero("Alexa", 20);

        this.heroRepository.Create(hero);
        this.heroRepository.Remove(hero.Name);

        var expectedCount = 0;
        var actualCount = this.heroRepository.Heroes.Count;

        Assert.AreEqual(expectedCount, actualCount);
    }

    [Test]
    public void RemoveMethodShouldReturnTrueIfHeroIsRemoved()
    {
        var hero = new Hero("Alexa", 20);

        this.heroRepository.Create(hero);
        var actualResult = this.heroRepository.Remove(hero.Name);

        Assert.IsTrue(actualResult);
    }

    [Test]
    public void RemoveMethodShouldReturnFalseIfHeroIsNotFound()
    {
        var hero = new Hero("Alexa", 20);

        this.heroRepository.Create(hero);
        var actualResult = this.heroRepository.Remove("Ivan");

        Assert.IsFalse(actualResult);
    }

    [Test]
    public void RemoveMethodShouldThrowArgumentNullExceptionIfNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => this.heroRepository.Remove(null));
    }

    [Test]
    public void RemoveMethodShouldThrowArgumentNullExceptionIfNameIsWhiteSpace()
    {
        Assert.Throws<ArgumentNullException>(() => this.heroRepository.Remove("     "));
    }

    [Test]
    public void GetHeroWithHighestLevelMethodShouldWorksCorrectly()
    {
        var firstHero = new Hero("Alexa", 5);
        var secondHero = new Hero("Kiro", 12);
        var thirdHero = new Hero("Vanko", 1);

        this.heroRepository.Create(firstHero);
        this.heroRepository.Create(secondHero);
        this.heroRepository.Create(thirdHero);

        var actualResult = this.heroRepository.GetHeroWithHighestLevel();

        Assert.AreEqual(secondHero, actualResult);
    }

    [Test]
    public void GetHeroMethodShouldWorksCorrectly()
    {
        var firstHero = new Hero("Alexa", 5);
        var secondHero = new Hero("Kiro", 12);

        this.heroRepository.Create(firstHero);
        this.heroRepository.Create(secondHero);

        var actualResult = this.heroRepository.GetHero("Alexa");

        Assert.AreEqual(firstHero, actualResult);
    }
}