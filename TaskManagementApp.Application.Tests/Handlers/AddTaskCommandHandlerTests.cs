using Moq;
using TaskManagementApp.Application.Actions;
using TaskManagementApp.Application.Handlers.Commands.AddTask;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Domain;

namespace TaskManagementApp.Application.Tests.Handlers;

[TestClass]
public class AddTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [TestMethod]
    public async Task AddTaskCommandHandler_Should_Update_Repository_On_Valid_Data()
    {
        //arrange
        var taskName = "test1";
        _taskRepositoryMock.Setup(x => x.AddAsync(It.Is<TaskEntity>(e => e.Name == taskName), It.IsAny<CancellationToken>())).Verifiable(Times.Once);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable(Times.Once);

        var command = new AddTaskCommand
        {
            Name = taskName,
            Description = "Desc",
            AssignedTo = "test"
        };

        //act
        var handler = new AddTaskCommandHandler(_taskRepositoryMock.Object,
            _unitOfWorkMock.Object);
        await handler.Handle(command, CancellationToken.None);

        //assert
        _taskRepositoryMock.Verify();
        _unitOfWorkMock.Verify();
    }
}