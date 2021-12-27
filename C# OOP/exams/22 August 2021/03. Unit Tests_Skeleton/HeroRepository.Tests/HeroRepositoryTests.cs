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
    public void CtorInitializesCollectionOfPresents()
    {
        Assert.That(heroRepository, Is.Not.Null);
    }
    [Test]
    public void CreateThrowsAnExceptionWhenPresetIsNull()
    {
        Hero nullPresent = null;

        Assert.Throws<ArgumentNullException>(() => heroRepository.Create(nullPresent));
    }
    [Test]
    public void CreateThrowsAnExceptionWhenPresentIsAlreadyInTheBag()
    {
        Hero present = new Hero("Truck", 69);

        heroRepository.Create(present);

        Assert.Throws<InvalidOperationException>(() => heroRepository.Create(present));
    }
    [Test]
    public void CreateReturnsProperMessage()
    {
        Hero hero = new Hero("Truck", 69);

        string expectedMsg = $"Successfully added hero {hero.Name} with level {hero.Level}";

        string actual = heroRepository.Create(hero);

        Assert.That(expectedMsg, Is.EqualTo(actual));
    }
    [Test]
    public void My()
    {
        Hero hero = new Hero("Truck", 69);

        string expectedMsg = $"Successfully added hero {hero.Name} with level {hero.Level}";

        string actual = heroRepository.Create(hero);
        Assert.AreEqual(heroRepository.Heroes.Count, 1);
    }
    [Test]
    public void CreateAddsPresentsInTheBag()
    {
        Hero present = new Hero("Truck", 69);

        heroRepository.Create(present);

        Assert.That(present, Is.EqualTo(heroRepository.GetHero(present.Name)));
    }
    [Test]
    public void RemoveMethodRemovesPresentsFromTheBag()
    {
        Hero hero = new Hero("Truck", 69);

        heroRepository.Create(hero);

        heroRepository.Remove("Truck");

        Assert.That(heroRepository.GetHero(hero.Name), Is.Null);
    }
    [Test]
    public void RemoveReturnsBoolean()
    {
        Hero present = new Hero("Truck", 69);

        Assert.IsFalse(heroRepository.Remove("Truck"));

        heroRepository.Create(present);

        Assert.IsTrue(heroRepository.Remove("Truck"));
    }
    [Test]
    public void GetPresentWithLeastMagicWorks()
    {
        Hero truck = new Hero("Truck", 69);
        Hero bus = new Hero("Bus", 100);
        Hero leastMagic = new Hero("No Magic", 1);

        heroRepository.Create(truck);
        heroRepository.Create(bus);
        heroRepository.Create(leastMagic);

        Assert.That(bus, Is.EqualTo(heroRepository.GetHeroWithHighestLevel()));
    }
    [Test]
    public void GetPresentReturnPresent()
    {
        Hero truck = new Hero("Truck", 69);
        Hero bus = new Hero("Bus", 100);

        heroRepository.Create(truck);
        heroRepository.Create(bus);

        Hero expectedPresent = bus;
        Hero actualPresent = heroRepository.GetHero(bus.Name);

        Assert.AreEqual(expectedPresent, actualPresent);
    }
    
}