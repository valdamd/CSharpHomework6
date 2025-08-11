using ManagmentEmploee;
using NUnit.Framework;
namespace TestEmploee;

public class Tests
{
    [Test]
    public void Employee_CreationAndBasicProperties_WorkCorrectly()
    {
        var employee = new Employee(101)
        {
            FirstName = "Иван",
            LastName = "Петров",
            BirthDate = new DateTime(1990, 5, 15)
        };

        Assert.AreEqual(101, employee.EmploeeId);
        Assert.AreEqual("Иван", employee.FirstName);
        Assert.AreEqual("Петров", employee.LastName);
        Assert.AreEqual("Иван Петров", employee.FullName);
    }

    [Test]
    public void Employee_Age_CalculatedCorrectly()
    {
        var employee = new Employee(102)
        {
            BirthDate = DateTime.Now.AddYears(-30)
        };

        Assert.AreEqual(30, employee.Age);
    }

    [Test]
    public void Employee_Status_DeterminedByAge()
    {
        var junior = new Employee(103) { BirthDate = DateTime.Now.AddYears(-23) };
        var middle = new Employee(104) { BirthDate = DateTime.Now.AddYears(-35) };
        var senior = new Employee(105) { BirthDate = DateTime.Now.AddYears(-45) };

        Assert.AreEqual("Junior", junior.Status);
        Assert.AreEqual("Middle", middle.Status);
        Assert.AreEqual("Senior", senior.Status);
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

        Assert.AreEqual("+7-123-456-78-90", employee["телефон"]);
        Assert.AreEqual("Москва, ул. Примерная, 123", employee["адрес"]);
        Assert.IsNull(employee["email"]); // Несуществующий ключ
    }

    [Test]
    public void Employee_Salary_CannotBeSetDirectly()
    {
        var employee = new Employee(108);

        // Этот код НЕ должен компилироваться:
        // employee.Salary = 50000; // Приватный setter

        // Salary можно только читать
        Assert.AreEqual(0, employee.Salary); // Начальное значение
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
    }
}