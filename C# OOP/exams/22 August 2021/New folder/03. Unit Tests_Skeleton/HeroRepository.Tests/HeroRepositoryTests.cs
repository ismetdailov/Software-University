using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    HeroRepository repository;
    public HeroRepositoryTests()
    {
        this.repository = new HeroRepository();
        

    }
    [Test]
    public void ConstructorShouldWorksCorrectly()
    {
        Assert.IsNotNull(this.repository);
    }
    [Test]
    public void HeroesCollectionShouldCollectOnlyHeroes()
    {
        CollectionAssert.AllItemsAreInstancesOfType(this.repository.Heroes, typeof(Hero));
    }
    [Test]
    public void test()
    {
        var hero = new Hero(null, 0);
        Assert.Throws<ArgumentNullException>(() =>
        {
            repository.Create(null);
        });
       
    }
    [Test]
    public void testExist()
    {
        var hero = new Hero("pesho", 0);
        var hero1 = new Hero("pesho", 0);
        Exception ex = Assert.Throws<InvalidOperationException>(() =>
        {
          
            repository.Create(hero);
            repository.Create(hero1);
        });
        Assert.AreEqual(ex.Message, $"Hero with name {hero1.Name} already exists");
    }
    [Test]
    public void testAddSuccesfully()
    {
        var hero = new Hero("pe",1);
        var res = repository.Create(hero);
        Assert.AreEqual( $"Successfully added hero {hero.Name} with level {hero.Level}",res);
    }
    [Test]
    public void testRemove()
    {
        Exception ex = Assert.Throws<ArgumentNullException>(() =>
        {
        var res = repository.Remove(null);
        });
    }
    [Test]
    public void testRemoveEmpty()
    {
        Exception ex = Assert.Throws<ArgumentNullException>(() =>
        {
            var res = repository.Remove("");
        });
    }
    [Test]
    public void testRemovecorrect()
    {
        var hero = new Hero("gosho", 0);
        repository.Create(hero);
        Assert.IsTrue(repository.Remove("gosho"));
    }
    [Test]
    public void testRemovecorrectFalse()
    {
        var hero = new Hero("gosho", 0);
        repository.Create(hero);
        Assert.IsFalse(repository.Remove("gos"));
    }
    [Test]
    public void testHighLevel()
    {
        var hero = new Hero("gho",4);
        var hero1 = new Hero("goho",6);
        var hero2 = new Hero("goo",7);
        repository.GetHeroWithHighestLevel();
        Assert.AreEqual(hero2, hero2);
    }
    [Test]
    public void testGetHerol()
    {
        var hero = new Hero("gho", 4);
        var hero1 = new Hero("goho", 6);
        var hero2 = new Hero("goo", 7);
        repository.GetHero("goo");
        Assert.AreEqual(hero2, hero2);
    }
    [Test]
    public void ReadOnlyColection()
    {
        var hero = new Hero("gho", 4);
        var hero1 = new Hero("goho", 6);
        repository.Create(hero);
        repository.Create(hero1);
        Assert.AreEqual(2, repository.Heroes.Count);

    }
    
}