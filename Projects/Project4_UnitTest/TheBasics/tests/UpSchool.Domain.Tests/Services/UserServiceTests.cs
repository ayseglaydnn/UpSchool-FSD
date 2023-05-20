using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;
using UpSchool.Domain.Utilities;

namespace UpSchool.Domain.Tests.Services
{
	public class UserServiceTests
	{
		[Fact]
		public async void GetUser_ShouldGetUserWithCorrectId()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

			var cancellationSource = new CancellationTokenSource();

			var expectedUser = new User()
			{
				Id = userId
			};

			A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
				.Returns(Task.FromResult(expectedUser));

			IUserService userService = new UserManager(userRepositoryMock);

			var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

			Assert.Equal(expectedUser, user);

		}

		[Fact]
		public async void AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			var cancellationSource = new CancellationTokenSource();

			IUserService userService = new UserManager(userRepositoryMock);

			var emptyException = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.AddAsync("ALPER", "TUNGA", 25, String.Empty, cancellationSource.Token));

			var nullException = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.AddAsync("ALPER", "TUNGA", 25, null, cancellationSource.Token));

			Assert.Equal("Email cannot be null or empty.", emptyException.Message);

			Assert.Equal("Email cannot be null or empty.", nullException.Message);
		}

		[Fact]
		public async void AddAsync_ShouldReturn_CorrectUserId()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			var cancellationSource = new CancellationTokenSource();

			Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

			var addedUser = new User()
			{
				Id = userId
			};

			A.CallTo(() => userRepositoryMock.AddAsync(addedUser, cancellationSource.Token))
				.Returns(Task.FromResult(1));

			IUserService userService = new UserManager(userRepositoryMock);

			var addedUserId = await userService.AddAsync("ALPER", "TUNGA", 25, "alper@gmail.com", cancellationSource.Token);

			Assert.NotNull(addedUserId.ToString());

			Assert.NotEmpty(addedUserId.ToString());
			
		}

		[Fact]
		public async void DeleteAsync_ShouldReturnTrue_WhenUserExists()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			var cancellationSource = new CancellationTokenSource();

			Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

			var user = new User()
			{
				Id = userId
			};

			A.CallTo(() => userRepositoryMock.DeleteAsync(A<Expression<Func<User, bool>>>.Ignored, cancellationSource.Token))
				.Returns(Task.FromResult(1));

			IUserService userService = new UserManager(userRepositoryMock);

			var userStatus = await userService.DeleteAsync(userId, cancellationSource.Token);

			Assert.True(userStatus);

		}

		[Fact]
		public async void DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			var cancellationSource = new CancellationTokenSource();

			A.CallTo(() => userRepositoryMock.DeleteAsync(A<Expression<Func<User,bool>>>.Ignored, cancellationSource.Token))
				.Returns(Task.FromResult(1));

			IUserService userService = new UserManager(userRepositoryMock);

			var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.DeleteAsync(Guid.Empty, cancellationSource.Token));

			Assert.Equal("id cannot be empty.", exception.Message);
		}

		[Fact]
		public async void UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			var cancellationSource = new CancellationTokenSource();

			var expectedUser = new User()
			{
				Id = Guid.Empty,
				Email = "upSchool@dotnet.gg"
			};

			A.CallTo(() => userRepositoryMock.UpdateAsync(expectedUser, cancellationSource.Token))
				.Returns(Task.FromResult(1));

			IUserService userService = new UserManager(userRepositoryMock);

			var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.UpdateAsync(expectedUser, cancellationSource.Token));

			Assert.Equal("User Id cannot be null or empty.", exception.Message);
		}

		[Fact]
		public async void UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();
			var cancellationSource = new CancellationTokenSource();

			Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

			var nullCaseUser = new User()
			{
				Email = null,
				Id = userId
			};
			
			var emptyCaseUser = new User()
			{
				Email = string.Empty,
				Id = userId
			};

			IUserService userService = new UserManager(userRepositoryMock);

			var nullException = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.UpdateAsync(nullCaseUser, cancellationSource.Token));
			
			var emptyException = await Assert.ThrowsAnyAsync<ArgumentException>(() => userService.UpdateAsync(emptyCaseUser, cancellationSource.Token));
			
			Assert.Equal("Email cannot be null or empty.", nullException.Message);

			Assert.Equal("Email cannot be null or empty.", emptyException.Message);
		}

		[Fact]
		public async void GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
		{
			var userRepositoryMock = A.Fake<IUserRepository>();

			Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

			var cancellationSource = new CancellationTokenSource();

			List<User> users = new List<User>();
			users.Add(new User());
			users.Add(new User());

			A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token))
				.Returns(Task.FromResult(users));

			IUserService userService = new UserManager(userRepositoryMock);

			var userList = await userService.GetAllAsync(cancellationSource.Token);

			Assert.True(userList.Count() >= 2);
		}

	}
}
