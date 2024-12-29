using Moq;
using System.Globalization;
using TaskManagementApp.Application.Events;
using TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Application.Providers;
using TaskManagementApp.Domain;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Tests.Handlers;

[TestClass]
public class UpdateTaskStatusCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock = new ();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new ();
    private readonly Mock<IServiceBusSender> _serviceBusSenderMock = new ();
    private readonly Mock<IDateTimeProvider> _dataTimeProviderMock = new ();

    public UpdateTaskStatusCommandHandlerTests()
    {
        _dataTimeProviderMock.Setup(x => x.GetTodayDateTimeUtc())
            .Returns(DateTime.ParseExact("1999-12-07 12:15", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
    }

    [TestMethod]
    public async Task UpdateTaskStatusCommandHandler_Should_Pass_On_Different_NewStatus()
    {
        //arrange
        var taskId = 1;
        var repositoryTask = new TaskEntity("name", "desc")
        {
            ID = taskId
        };
        _taskRepositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync(repositoryTask).Verifiable(Times.Once);
        _taskRepositoryMock.Setup(x=>x.UpdateAsync(It.Is<TaskEntity>(e => e.ID == taskId), It.IsAny<CancellationToken>())).Verifiable(Times.Once);
        _unitOfWorkMock.Setup(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable(Times.Once);
        _serviceBusSenderMock.Setup(x=>x.SendAsync(It.Is<TaskStatusUpdatedEvent>(e => e.Id == taskId),It.IsAny<string>(), It.IsAny<CancellationToken>())).Verifiable(Times.Once);
        
        var command = new UpdateTaskStatusCommand(taskId, TaskStatus.Completed);

        //act
        var handler = new UpdateTaskStatusCommandHandler(_taskRepositoryMock.Object,
            _unitOfWorkMock.Object, _serviceBusSenderMock.Object, _dataTimeProviderMock.Object);
        await handler.Handle(command, CancellationToken.None);

        //assert
        _taskRepositoryMock.Verify();
        _serviceBusSenderMock.Verify();
        _unitOfWorkMock.Verify();
    }

    [TestMethod]
    public async Task UpdateTaskStatusCommandHandler_Should_Throw_Exception_On_Same_NewStatus()
    {
        //arrange
        var taskId = 1;
        var repositoryTask = new TaskEntity("name", "desc")
        {
            ID = taskId
        };
        _taskRepositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync(repositoryTask).Verifiable(Times.Once);
        _taskRepositoryMock.Setup(x => x.UpdateAsync(It.Is<TaskEntity>(e => e.ID == taskId), It.IsAny<CancellationToken>())).Verifiable(Times.Never);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable(Times.Never);
        _serviceBusSenderMock.Setup(x => x.SendAsync(It.Is<TaskStatusUpdatedEvent>(e => e.Id == taskId), It.IsAny<string>(), It.IsAny<CancellationToken>())).Verifiable(Times.Never);

        var command = new UpdateTaskStatusCommand(taskId, TaskStatus.NotStarted);

        //act //assert
        var handler = new UpdateTaskStatusCommandHandler(_taskRepositoryMock.Object,
            _unitOfWorkMock.Object, _serviceBusSenderMock.Object, _dataTimeProviderMock.Object);

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
        
        //assert
        _taskRepositoryMock.Verify();
        _serviceBusSenderMock.Verify();
        _unitOfWorkMock.Verify();
    }
}