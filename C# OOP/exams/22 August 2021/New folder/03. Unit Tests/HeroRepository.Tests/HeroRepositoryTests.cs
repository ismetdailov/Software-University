using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    [Test]
    public void CreateHero()
    {
        Hero hero = new Hero("str", 10);
        Assert.AreEqual(hero.Name, "str");
        Assert.AreEqual(hero.Level, 10);



    }
    [Test]
    public void CreateHeroRepository()
    {
        HeroRepository hero = new HeroRepository();
        Hero her = new Hero(null, 10);
        Hero hero1 = new Hero("gosho", 3);
        Hero hero2 = new Hero("gosho", 3);
        Assert.AreEqual(hero.Create(hero2), $"Successfully added hero {hero1.Name} with level {hero1.Level}");
        Assert.Throws<InvalidOperationException>(() => hero.Create(hero2));
        Assert.Throws<ArgumentNullException>(() => hero.Create(null));


    }
    public void RemoveHeroRepository()
    {
        HeroRepository hero = new HeroRepository();

        Assert.Throws<ArgumentNullException>(() => hero.Remove(null));

    }
    public void RemoveHeroRepos()
    {
        HeroRepository hero = new HeroRepository();
        Hero hero1 = new Hero("gosho", 3);
        Hero hero2 = new Hero("pesho", 3);

        string name = "gosho";
        hero.Remove(name);
        Assert.AreEqual(name, true);
    }
    public void RemoveHeroRepo()
    {
        HeroRepository hero = new HeroRepository();
        Hero hero1 = new Hero("gosho", 3);
        Hero hero2 = new Hero("pesho", 3);

        string name = "gosho";

        Assert.AreEqual(hero.Remove(name), true);
        Assert.AreEqual(hero.Remove("ho"), false);
        Assert.AreEqual(hero.Remove(" "), false);
    }
    public void RemoveHeroepo()
    {
        HeroRepository hero = new HeroRepository();
        Hero hero1 = new Hero("gosho", 3);
        Hero hero2 = new Hero("pesho", 3);

        string name = "gosho";

        Assert.AreEqual(hero.Remove(name), hero.Heroes.Count == 1);
    }
    public void RemoveHeroReo()
    {
        HeroRepository hero = new HeroRepository();
        Assert.AreEqual(hero.Remove(" "), false);
    }
    public void CreateHeroRepositoryHeroWithHighestLevel()
    {
        HeroRepository hero = new HeroRepository();
        Hero her = new Hero(null, 10);
        Hero hero1 = new Hero("gosho", 3);
        Hero hero2 = new Hero("osho", 4);
        Hero hero3 = new Hero("goho", 20);
        hero.GetHeroWithHighestLevel();
        Assert.AreEqual(hero3.Name, "goho");

    }
    public void CreateHeroRepositoryGetHero()
    {
        HeroRepository hero = new HeroRepository();
        string name = "gosho";
        int level = 5;
        Hero hero1 = new Hero(name, level);
       
        hero.GetHero(name);
        Assert.AreEqual(hero1.Name,name);

    }
}


//public bool Remove(string name)
//{
//    if (String.IsNullOrWhiteSpace(name))
//    {
//        throw new ArgumentNullException(nameof(name), "Name cannot be null");
//    }

//    Hero hero = this.data.FirstOrDefault(h => h.Name == name);
//    bool isRemoved = this.data.Remove(hero);
//    return isRemoved;
//}

//public Hero GetHeroWithHighestLevel()
//{
//    Hero hero = this.data.OrderByDescending(h => h.Level).ToArray()[0];
//    return hero;
//}

//public Hero GetHero(string name)
//{
//    Hero hero = this.data.FirstOrDefault(h => h.Name == name);
//    return hero;
//}

//public IReadOnlyCollection<Hero> Heroes => this.data.AsReadOnly();