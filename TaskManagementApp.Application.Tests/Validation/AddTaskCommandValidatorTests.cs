using TaskManagementApp.Application.Handlers.Commands.AddTask;

namespace TaskManagementApp.Application.Tests.Validation;

[TestClass]
public class AddTaskCommandValidatorTests
{
    [TestMethod]
    public void AddTaskCommandValidator_Should_Pass_On_Fully_Filled_Command()
    {
        //arrange
        var dto = new AddTaskCommand
        {
            Name = "Test",
            Description = "TestDesc",
            AssignedTo = null
        };

        //act
        var validator = new AddTaskCommandValidator();
        var validationResult = validator.Validate(dto);

        //assert
        Assert.IsTrue(validationResult.IsValid);
    }

    [TestMethod]
    public void AddTaskCommandValidator_Should_Fail_Validation_On_Missing_Required_Fields()
    {
        //arrange
        var dto = new AddTaskCommand
        {
            Description = ""
        };

        //act
        var validator = new AddTaskCommandValidator();
        var validationResult = validator.Validate(dto);

        //assert
        Assert.IsFalse(validationResult.IsValid);
        Assert.AreEqual(2, validationResult.Errors.Count);
        Assert.AreEqual("Name cannot be empty", validationResult.Errors[0].ErrorMessage);
        Assert.AreEqual("Description cannot be empty", validationResult.Errors[1].ErrorMessage);
    }
}