using SchoolProvider.Test.Mocks;

namespace SchoolProvider.Test;

[TestFixture]
public class CreateStudentTests
{
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public async Task Should_Create_Student_Successfully()
    // {
    //     // Arrange
    //     var command = new CreateStudentCommand("John", DateTime.Now,["Math","Physic"] );
    //
    //     var handler = new CreateStudentCommandHandler(UnitOfWorkMock.Students);
    //
    //     // Act
    //     var result = await handler.Handle(command, CancellationToken.None);
    //
    //     // Assert
    //     Assert.Equals(Is.Not.Null, result);
    // }
}
