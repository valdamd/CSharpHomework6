// <copyright file="UnitTest1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TestEmploee;

using FluentAssertions;
using ManagmentEmploee;
using NUnit.Framework;

public sealed class UnitTest1
{
    [Test]
    public void Employee_CreationAndBasicProperties_WorkCorrectly()
    {
        var employee = new Employee(101)
        {
            FirstName = "Иван",
            LastName = "Петров",
            BirthDate = new DateTime(1990, 5, 15),
        };

        Assert.That(employee.EmploeeId, Is.EqualTo(101));
        Assert.That(employee.FirstName, Is.EqualTo("Иван"));
        Assert.That(employee.LastName, Is.EqualTo("Петров"));
        Assert.That(employee.FullName, Is.EqualTo("Иван Петров"));
    }

    [Test]
    public void Employee_Age_CalculatedCorrectly()
    {
        var employee = new Employee(102)
        {
            BirthDate = DateTime.Now.AddYears(-30),
        };

        Assert.That(employee.Age, Is.EqualTo(30));
    }

    [Test]
    public void Employee_Status_DeterminedByAge()
    {
        var junior = new Employee(103) { BirthDate = DateTime.Now.AddYears(-23) };
        var middle = new Employee(104) { BirthDate = DateTime.Now.AddYears(-35) };
        var senior = new Employee(105) { BirthDate = DateTime.Now.AddYears(-45) };

        Assert.That(junior.Status, Is.EqualTo("Junior"));
        Assert.That(middle.Status, Is.EqualTo("Middle"));
        Assert.That(senior.Status, Is.EqualTo("Senior"));
    }

    [Test]
    public void Employee_BirthDate_ValidatesInput()
    {
        var employee = new Employee(106);

        Assert.Throws<ArgumentException>(() =>
            employee.BirthDate = DateTime.Now.AddDays(1)); // Будущая дата

        Assert.Throws<ArgumentException>(() =>
            employee.BirthDate = new DateTime(1850, 1, 1)); // Слишком старая дата
    }

    [Test]
    public void Employee_Indexer_WorksWithAdditionalInfo()
    {
        var employee = new Employee(107);

        employee["телефон"] = "+7-123-456-78-90";
        employee["адрес"] = "Москва, ул. Примерная, 123";

        Assert.That(employee["телефон"], Is.EqualTo("+7-123-456-78-90"));
        Assert.That(employee["адрес"], Is.EqualTo("Москва, ул. Примерная, 123"));
        Assert.IsNull(employee["email"]); // Несуществующий ключ
    }

    [Test]
    public void Employee_Salary_CannotBeSetDirectly()
    {
        var employee = new Employee(108);

        // Этот код НЕ должен компилироваться:
        // employee.Salary = 50000; // Приватный setter

        // Salary можно только читать
        Assert.That(employee.Salary, Is.EqualTo(0)); // Начальное значение
    }

    [Test]
    public void Employee_ImmutableProperties_CannotBeChanged()
    {
        var employee = new Employee(109) { LastName = "Иванов" };

        // Этот код НЕ должен компилироваться после создания:
        // employee.EmployeeId = 999;  // Только для чтения
        // employee.LastName = "Петров"; // Init-only
        // employee.Age = 25;          // Вычисляемое свойство
        // employee.FullName = "Test"; // Вычисляемое свойство
        employee.EmploeeId.Should().Be(109);
        employee.LastName.Should().Be("Иванов");
    }
}