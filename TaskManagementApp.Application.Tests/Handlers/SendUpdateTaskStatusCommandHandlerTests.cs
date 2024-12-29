using Moq;
using TaskManagementApp.Application.Actions;
using TaskManagementApp.Application.Events;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;
using TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Domain;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Tests.Handlers;

[TestClass]
public class SendUpdateTaskStatusCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IServiceBusSender> _serviceBusSenderMock = new();

    [TestMethod]
    public async Task SendUpdateTaskStatusCommandHandler_Should_UpdateEntity_And_Send_Message_On_Different_NewStatus()
    {
        //arrange
        var taskId = 1;
        var repositoryTask = new TaskEntity("name", "desc")
        {
            ID = taskId
        };
        _taskRepositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync(repositoryTask).Verifiable(Times.Once);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable(Times.Once);
        _serviceBusSenderMock.Setup(x => x.SendAsync(It.Is<UpdateTestStatusAction>(e => e.Id == taskId), It.IsAny<string>(), It.IsAny<CancellationToken>())).Verifiable(Times.Once);

        var command = new SendUpdateTaskStatusCommand(taskId, Domain.TaskStatus.Completed);

        //act
        var handler = new SendUpdateTaskStatusCommandHandler(_taskRepositoryMock.Object,
            _unitOfWorkMock.Object, _serviceBusSenderMock.Object);
        await handler.Handle(command, CancellationToken.None);

        //assert
        _taskRepositoryMock.Verify();
        _serviceBusSenderMock.Verify();
        _unitOfWorkMock.Verify();
    }

    [TestMethod]
    public async Task SendUpdateTaskStatusCommandHandler_Should_Throw_Exception_On_Missing_Task()
    {
        //arrange
        var taskId = 1;
        TaskEntity? repositoryTask = null;
        _taskRepositoryMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync(repositoryTask).Verifiable(Times.Once);
        _taskRepositoryMock.Setup(x => x.UpdateAsync(It.Is<TaskEntity>(e => e.ID == taskId), It.IsAny<CancellationToken>())).Verifiable(Times.Never);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable(Times.Never);
        _serviceBusSenderMock.Setup(x => x.SendAsync(It.Is<TaskStatusUpdatedEvent>(e => e.Id == taskId), It.IsAny<string>(), It.IsAny<CancellationToken>())).Verifiable(Times.Never);

        var command = new SendUpdateTaskStatusCommand(taskId, TaskStatus.NotStarted);

        //act //assert
        var handler = new SendUpdateTaskStatusCommandHandler(_taskRepositoryMock.Object,
            _unitOfWorkMock.Object, _serviceBusSenderMock.Object);

        await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });

        //assert
        _taskRepositoryMock.Verify();
        _serviceBusSenderMock.Verify();
        _unitOfWorkMock.Verify();
    }
}