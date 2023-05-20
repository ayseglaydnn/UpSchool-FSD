using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Dtos;
using UpSchool.Domain.Utilities;

namespace UpSchool.Domain.Tests.Utilities
{
	public class PasswordGeneratorTests
	{
		[Fact]
		public void Generate_ShouldReturnEmptyString_WhenNoCharactersAreIncluded()
		{
			var ipHelper = A.Fake<IIpHelper>();

			A.CallTo(() => ipHelper.GetCurrentIPAddress()).Returns("195.142.70.227");

			var localDbMock = A.Fake<ILocalDB>();

			A.CallTo(() => localDbMock.IPs).Returns(new List<string>()
			{
				"195.142.70.227",
				"180.22.22.22",
				"155.166.177.188"
			});

			// Arrange
			var passwordGenerator = new PasswordGenerator(ipHelper,localDbMock);

			var generatePasswordDto = new GeneratePasswordDto()
			{
				Length = 20,
				IncludeLowercaseCharacters=false,
				IncludeNumbers=false,
				IncludeSpecialCharacters=false,
				IncludeUppercaseCharacters=false,
			};

			// Act
			var password = passwordGenerator.Generate(generatePasswordDto);

			//Assert
			Assert.Equal(string.Empty, password);
			
		}

		[Fact]
		public void Generate_ShouldReturnPasswordWithGivenLength()
		{
			// Arrange
			var ipHelper = A.Fake<IIpHelper>();

			A.CallTo(() => ipHelper.GetCurrentIPAddress()).Returns("195.142.70.227");

			var localDbMock =A.Fake<ILocalDB>();

			A.CallTo(() => localDbMock.IPs).Returns(new List<string>()
			{
				"195.142.70.227",
				"180.22.22.22",
				"155.166.177.188"
			});

			//var localDb = new LocalDB();

			//IIpHelper ipHelper = new IpHelper();

			var passwordGenerator = new PasswordGenerator(ipHelper,localDbMock);

			var generatePasswordDto = new GeneratePasswordDto()
			{
				Length = 20,
				IncludeLowercaseCharacters = true,
				IncludeNumbers = true,
				IncludeSpecialCharacters = true,
				IncludeUppercaseCharacters = true,
			};

			// Act
			var password = passwordGenerator.Generate(generatePasswordDto);

			//Assert
			Assert.Equal(generatePasswordDto.Length, password.Length);

		}

		[Fact]
		public void Generate_ShouldIncludeNumbers_WhenIncludeNumbersIsTrue()
		{
			var ipHelper = A.Fake<IIpHelper>();

			A.CallTo(() => ipHelper.GetCurrentIPAddress()).Returns("195.142.70.227");

			var localDbMock = A.Fake<ILocalDB>();

			A.CallTo(() => localDbMock.IPs).Returns(new List<string>()
			{
				"195.142.70.227",
				"180.22.22.22",
				"155.166.177.188"
			});

			// Arrange
			var passwordGenerator = new PasswordGenerator(ipHelper, localDbMock);

			var generatePasswordDto = new GeneratePasswordDto()
			{
				Length = 20,
				IncludeLowercaseCharacters = false,
				IncludeNumbers = true,
				IncludeSpecialCharacters = false,
				IncludeUppercaseCharacters = false,
			};

			// Act
			var password = passwordGenerator.Generate(generatePasswordDto);

			//Assert
			Assert.Contains(password, x => char.IsDigit(x));

			Assert.True(password.Any(Char.IsDigit), password);
		}

		[Fact]
		public void Generate_ShouldIncludeSpecialCharacters_WhenIncludeSpecialCharactersIsTrue()
		{
			var ipHelper = A.Fake<IIpHelper>();

			A.CallTo(() => ipHelper.GetCurrentIPAddress()).Returns("195.142.70.227");

			var localDbMock = A.Fake<ILocalDB>();

			A.CallTo(() => localDbMock.IPs).Returns(new List<string>()
			{
				"195.142.70.227",
				"180.22.22.22",
				"155.166.177.188"
			});

			// Arrange
			var passwordGenerator = new PasswordGenerator(ipHelper, localDbMock);

			var generatePasswordDto = new GeneratePasswordDto()
			{
				Length = 20,
				IncludeLowercaseCharacters = false,
				IncludeNumbers = false,
				IncludeSpecialCharacters = true,
				IncludeUppercaseCharacters = false,
			};

			var specialCharacters = "!@#$%^&*()";

			// Act
			var password = passwordGenerator.Generate(generatePasswordDto);

			//Assert
			Assert.Contains(password, x => specialCharacters.Contains(x));
		}

		
	}
}
