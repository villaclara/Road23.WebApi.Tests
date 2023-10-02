using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Road23.WebApi.Tests.Mocks;
using Road23.WebAPI.Controllers;
using Road23.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road23.WebApi.Tests.ControllerTests
{
	public class CategoryControllerTests
	{
		[Fact]
		public void GetAllCategories_ReturnsAllCategories()
		{
			// Arrange
			var mockCategoryRepository = MockRepositoryWrapper.GetCategoryMock();
			var mockCandleRepository = MockRepositoryWrapper.GetCandleMock();

			// // creating controller
			// // Object = gives the instance of Mock, otherwise it is not working
			var categoryController = new CandleCategoryController(mockCategoryRepository.Object, mockCandleRepository.Object);

			// Act
			var result = categoryController.GetCategories() as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.NotEmpty(result.Value as IEnumerable<CandleCategoryFullVM>);
		}


		[Fact]
		public void GetCategoryByID_ReturnsCategory()
		{
			// Arrange
			var mockCategoryRepository = MockRepositoryWrapper.GetCategoryMock();
			var mockCandleRepository = MockRepositoryWrapper.GetCandleMock();

			var controller = new CandleCategoryController(mockCategoryRepository.Object, mockCandleRepository.Object);

			// Act
			var correctId = 1;
			var result = controller.GetCategoryById(correctId) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsAssignableFrom<CandleCategoryFullVM>(result.Value);
			Assert.NotNull(result.Value as CandleCategoryFullVM);

		}

		[Fact]
		public void GetCategoryWithWrongID_ReturnsNotFound404Code()
		{
			// Arrange
			var mockCategoryRepository = MockRepositoryWrapper.GetCategoryMock();
			var mockCandleRepository = MockRepositoryWrapper.GetCandleMock();

			var controller = new CandleCategoryController(mockCategoryRepository.Object, mockCandleRepository.Object);

			// Act
			int wrongID = 10;
			var result = controller.GetCategoryById(wrongID) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
		}

		
		// create, update, delete next
	}
}
